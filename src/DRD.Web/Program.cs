// =============================================================================
// Projet                         DRD.Web
// Nom du fichier                 Program.cs
// Type de fichier                Point d'entrée
// Nature C#                      Top-level statements
// Emplacement                    /
// Auteur                         Michel Gariépy
// Créé le                        2025-12-02
//
// Description
//     Point d'entrée principal de l'application Web DRD. Configure Serilog,
//     le DbContext, Identity, la localisation, MVC et le pipeline HTTP.
//     Intègre également le seeding Identity (rôles, access type,
//     administrateur système).
//
// Fonctionnalité
//     - Initialiser la journalisation Serilog (console + fichier).
//     - Configurer ApplicationDbContext (BD DRDv10).
//     - Configurer ASP.NET Identity avec ApplicationUser.
//     - Activer la localisation fr-CA / en-CA.
//     - Exécuter le seeding Identity au démarrage.
//     - Enregistrer MVC (controllers + views).
//     - Configurer le pipeline HTTP.
//
// Modifications
//     2025-12-02    Clean start .NET 10 + intégration Serilog DEV + PROD.
//     2025-12-02    Ajout du seeding Identity DRD (rôles + access type + admin).
// ============================================================================

using System.Globalization;
using DRD.Infrastructure.Data;
using DRD.Infrastructure.Identity;
using DRD.Infrastructure.Identity.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Serilog;

// ---------------------------------------------------------
// DRD – Bootstrap du logger (Serilog DEV + PROD)
// ---------------------------------------------------------

var logDirectory = @"C:\DRD.Refonte\Logs";

Directory.CreateDirectory(logDirectory);

// DEV → fichier par lancement
var devLogFile = Path.Combine(
	logDirectory,
	$"app-log-{DateTime.Now:yyyyMMdd-HHmmss}.txt");

// PROD → fichier par jour
var prodLogPattern = Path.Combine(
	logDirectory,
	"app-.log");

// Bootstrap logging
Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File(devLogFile)
	.CreateBootstrapLogger();

try
{
	Log.Information("Démarrage de DRD.Web…");

	var builder = WebApplication.CreateBuilder(args);

	#region DRD – Configuration Serilog
	/// <summary>
	/// Active la configuration Serilog complète (DEV/PROD).
	/// </summary>
	builder.Host.UseSerilog((context, services, configuration) =>
	{
		var env = context.HostingEnvironment;

		configuration
			.ReadFrom.Configuration(context.Configuration)
			.ReadFrom.Services(services)
			.Enrich.FromLogContext()
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
	#endregion


	#region DRD – DbContext
	/// <summary>
	/// Connexion SQL Server DRDv10.
	/// </summary>
	var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
		?? throw new InvalidOperationException(
			"La chaîne de connexion 'DefaultConnection' est manquante dans appsettings.json.");

	builder.Services.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer(connectionString));
	#endregion


	#region DRD – Identity
	/// <summary>
	/// ASP.NET Identity avec ApplicationUser.
	/// </summary>
	builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
	{
		options.SignIn.RequireConfirmedAccount = false;
	})
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();
	#endregion


	#region DRD – Localization
	/// <summary>
	/// Localisation fr-CA / en-CA.
	/// </summary>
	builder.Services.AddLocalization(options => options.ResourcesPath = "");

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
	});
	#endregion


	#region DRD – MVC
	/// <summary>
	/// MVC (controllers + views).
	/// </summary>
	builder.Services.AddControllersWithViews()
		.AddViewLocalization()
		.AddDataAnnotationsLocalization();
	#endregion


	var app = builder.Build();


	#region DRD – Seeding Identity
	/// <summary>
	/// Exécution du seeding Identity au démarrage.
	/// </summary>
	try
	{
		using var scope = app.Services.CreateScope();
		var services = scope.ServiceProvider;

		var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
		var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
		var context = services.GetRequiredService<ApplicationDbContext>();

		Log.Information("Exécution du seeding Identity DRD…");

		await IdentitySeeder.SeedAsync(roleManager, userManager, context);

		Log.Information("Seeding Identity complété avec succès.");
	}
	catch (Exception seedEx)
	{
		Log.Error(seedEx, "Erreur lors de l'exécution du seeding Identity DRD.");
		throw;
	}
	#endregion


	#region DRD – Pipeline HTTP
	/// <summary>
	/// Pipeline MVC + Auth + Localisation + Erreurs.
	/// </summary>
	var localizationOptions = app.Services.GetRequiredService<
		Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>().Value;

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
	#endregion

	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "L'application s'est arrêtée de manière inattendue.");
}
finally
{
	Log.CloseAndFlush();
}

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
