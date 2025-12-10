// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 Program.cs
// Type de fichier                Point d'entrée
// Classe                         (Top-level statements)
// Emplacement                    /
// Entités concernées             ApplicationUser, ApplicationDbContext
// Créé le                        2025-12-02
//
// Description
//     Point d'entrée principal de l'application Web DRD. Configure Serilog,
//     la base de données, Identity, la localisation bilingue, MVC, la
//     politique d'autorisation globale et le pipeline HTTP. Exécute aussi
//     le seeding Identity (rôles, types d’accès, administrateur système).
//
// Fonctionnalité
//     - Initialiser la journalisation Serilog (console + fichiers).
//     - Configurer ApplicationDbContext.
//     - Configurer ASP.NET Identity (ApplicationUser).
//     - Activer fr-CA et en-CA; culture par défaut : fr-CA.
//     - Ajouter MVC avec localisation et AuthorizeFilter global.
//     - Exécuter le seeding Identity au démarrage.
//     - Configurer le pipeline HTTP complet.
//
// Modifications
//     2025-12-02    Création initiale DRD v10.
//     2025-12-03    Nettoyage Identity (retrait DefaultIdentity).
//     2025-12-03    Ajout AuthorizeFilter global (Option B).
//     2025-12-03    Régions DRD + résumés.
//     2025-12-03    Ajout obligatoire AddLogging() pour injection ILogger<T>.
//     2025-12-04    Serilog ajusté (messages en anglais, unilingue).
// ============================================================================

using System.Globalization;
using DRD.Infrastructure.Data;
using DRD.Infrastructure.Identity;
using DRD.Infrastructure.Identity.Seeding;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

// ---------------------------------------------------------
// DRD – Bootstrap Serilog minimal (before DI)
// ---------------------------------------------------------
var logDirectory = @"C:\DRD.Refonte\Logs";
Directory.CreateDirectory(logDirectory);

var devLogFile = Path.Combine(logDirectory, $"app-log-{DateTime.Now:yyyyMMdd-HHmmss}.txt");
var prodLogPattern = Path.Combine(logDirectory, "app-.log");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(devLogFile)
    .CreateBootstrapLogger();

try
{
    Log.Information("Program - Startup - DRD.Web initializing...");

    var builder = WebApplication.CreateBuilder(args);

    // ============================================================================
    // Région DRD – Configuration Serilog
    // ============================================================================
    /// <summary>
    /// Configure Serilog logging system for development and production environments.
    /// English-only logs (debugging purpose).
    /// </summary>
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        var env = context.HostingEnvironment;

        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "DRD.Web")
            .Enrich.WithProperty("Environment", env.EnvironmentName)
            .WriteTo.Console();

        if (env.IsDevelopment())
        {
            configuration.WriteTo.File(devLogFile);
        }
        else
        {
            configuration.WriteTo.File(
                path: prodLogPattern,
                rollingInterval: RollingInterval.Day);
        }
    });

    // ============================================================================
    // Région DRD – Base de données
    // ============================================================================
    /// <summary>
    /// Configure the SQL Server database context used by DRD.
    /// </summary>
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException(
            "The 'DefaultConnection' connection string is missing in appsettings.json.");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

    // ============================================================================
    // Région DRD – Identity
    // ============================================================================
    /// <summary>
    /// Configures ASP.NET Identity for ApplicationUser and IdentityRole.
    /// </summary>
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

    // ============================================================================
    // Région DRD – Localisation
    // ============================================================================
    /// <summary>
    /// Enables French/English localization (default: fr-CA).
    /// </summary>
    builder.Services.AddLocalization();

    var supportedCultures = new[]
    {
        new CultureInfo("fr-CA"),
        new CultureInfo("en-CA")
    };

    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        options.DefaultRequestCulture = new RequestCulture("fr-CA");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
        options.ApplyCurrentCultureToResponseHeaders = true;
    });

    // ============================================================================
    // Région DRD – MVC + Global Authorization
    // ============================================================================
    /// <summary>
    /// Activates MVC, localization and enforces global authentication requirement.
    /// </summary>
    builder.Services.AddControllersWithViews(options =>
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

        options.Filters.Add(new AuthorizeFilter(policy));
    })
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

    // ============================================================================
    // Région DRD – Logging Dependency Injection
    // ============================================================================
    /// <summary>
    /// Enables dependency injection for ILogger&lt;T&gt; across the DRD solution.
    /// </summary>
    builder.Services.AddLogging();

    // ============================================================================
    // Région DRD – Build Application
    // ============================================================================
    var app = builder.Build();

    // ============================================================================
    // Région DRD – Identity Seeding
    // ============================================================================
    /// <summary>
    /// Seeds roles, access types, and system administrator account during startup.
    /// </summary>
    try
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var context = services.GetRequiredService<ApplicationDbContext>();

        Log.Information("Identity - Seeding - Start");

        await IdentitySeeder.SeedAsync(roleManager, userManager, context);

        Log.Information("Identity - Seeding - Completed");
    }
    catch (Exception seedingEx)
    {
        Log.Error(seedingEx, "Identity - Seeding - Fatal error");
        throw;
    }

    // ============================================================================
    // Région DRD – HTTP Pipeline
    // ============================================================================
    var localizationOptions =
        app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;

    app.UseRequestLocalization(localizationOptions);

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }
    else
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program - Startup - Fatal error");
}
finally
{
    Log.CloseAndFlush();
}
