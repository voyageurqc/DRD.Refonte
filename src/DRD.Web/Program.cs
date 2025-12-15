// ============================================================================
// Projet:      DRD.Web
// Fichier:     Program.cs
// Type:        Point d'entrée
// Classe:      (Top-level statements)
// Emplacement: /
// Entité(s):   ApplicationUser, ApplicationDbContext
// Créé le:     2025-12-02
//
// Description
//     Point d’entrée principal de l’application web DRD v10. Configure Serilog,
//     SQL Server, Identity, la localisation bilingue fr-CA/en-CA, MVC, Razor,
//     les filtres d’autorisation, ainsi que le pipeline HTTP complet.
//
// Fonctionnalité
//     - Configurer Serilog (console + fichiers journaliers).
//     - Enregistrer ApplicationDbContext (SQL Server).
//     - Configurer ASP.NET Identity avec ApplicationUser et roles.
//     - Définir la culture par défaut (fr-CA).
//     - Ajouter MVC + Razor Pages + localisation.
//     - Activer AuthorizeFilter global (sécurité).
//     - Configurer le pipeline HTTP DRD.
//     - Démarrer et journaliser l’application.
//
// Modifications
//     2025-12-11    Version DRD v10 complète (SQL Server + Identity + Serilog).
//     2025-12-10    Correction DI Identity (ApplicationDbContext manquant).
//     2025-12-03    Régions DRD + résumés.
//     2025-12-02    Création initiale DRD v10.
// ============================================================================

using DRD.Application;              
using DRD.Infrastructure;           
using DRD.Infrastructure.Data;
using DRD.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// REGION : Serilog
// ============================================================================
#region Serilog
/// <summary>
/// Configure Serilog avec logs journaliers conservés 7 jours.
/// </summary>
Log.Logger = new LoggerConfiguration()
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.WriteTo.File(
		path: $"Logs/log-{DateTime.UtcNow:yyyy-MM-dd}.txt",
		rollingInterval: RollingInterval.Day,
		retainedFileCountLimit: 7)
	.CreateLogger();

builder.Host.UseSerilog();
#endregion

// ============================================================================
// REGION : Services (DI)
// ============================================================================
#region Services

/// <summary>
/// Configure ApplicationDbContext (SQL Server).
/// </summary>
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection"),
		sql => sql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
	);
});


/// <summary>
/// Configure Identity (ApplicationUser + Roles).
/// </summary>
builder.Services
	.AddIdentity<ApplicationUser, IdentityRole>(options =>
	{
		options.SignIn.RequireConfirmedAccount = false;
	})
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();
builder.Services.AddScoped<
	Microsoft.AspNetCore.Identity.IUserClaimsPrincipalFactory<ApplicationUser>,
	DRD.Infrastructure.Identity.ApplicationUserClaimsPrincipalFactory>();


/// <summary>
/// MVC, Razor Pages et Localisation.
/// </summary>
builder.Services.AddControllersWithViews()
	.AddViewLocalization()
	.AddDataAnnotationsLocalization();

builder.Services.AddRazorPages();

/// <summary>
/// Enregistre les services Application et Infrastructure DRD v10.
/// IMPORTANT : Ces lignes doivent être AVANT builder.Build() !!!
/// </summary>
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

#endregion

// ============================================================================
// REGION : Localisation
// ============================================================================
#region Localisation

/// <summary>
/// Configure la culture par défaut fr-CA et support en-CA.
/// </summary>
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

// ============================================================================
// REGION : Pipeline HTTP
// ============================================================================
#region Pipeline

var app = builder.Build();   // ← À partir d’ici, les services deviennent READ-ONLY
							 // ← Donc plus aucun builder.Services.AddXxx() possible

/// <summary>
/// Active la localisation.
/// </summary>
app.UseRequestLocalization();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

/// <summary>
/// Route par défaut DRD.
/// </summary>
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

#endregion

// ============================================================================
// REGION : Run
// ============================================================================
#region Run

/// <summary>
/// Démarre l’application.
/// </summary>
Log.Information("DRD v10 — Application démarrée à {UtcTime}", DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture));
app.Run();

#endregion
