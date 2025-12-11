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
//     2025-12-11    Correction avertissement CA1305 (analyse : localisation / IFormatProvider).
//     2025-12-04    Serilog ajusté (messages en anglais, unilingue).
//     2025-12-03    Ajout obligatoire AddLogging() pour injection ILogger<T>.
//     2025-12-03    Régions DRD + résumés.
//     2025-12-03    Ajout AuthorizeFilter global (Option B).
//     2025-12-03    Nettoyage Identity (retrait DefaultIdentity).
//     2025-12-02    Création initiale DRD v10.
// ============================================================================


using DRD.Infrastructure;
using DRD.Application;
using Microsoft.AspNetCore.Localization;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// REGION : Configuration Serilog
// ============================================================================
#region Serilog
/// <summary>
/// Configuration du logger Serilog avec séparation DEV/PROD.
/// </summary>
Log.Logger = new LoggerConfiguration()
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.WriteTo.File(
		path: $"Logs/log-{DateTime.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}.txt",
		rollingInterval: RollingInterval.Day,
		retainedFileCountLimit: 7)
	.CreateLogger();

builder.Host.UseSerilog();
#endregion

// ============================================================================
// REGION : Configuration Services (DI)
// ============================================================================
#region Services

/// <summary>
/// Configuration MVC + localisation.
/// </summary>
builder.Services.AddControllersWithViews()
	.AddViewLocalization()
	.AddDataAnnotationsLocalization();

builder.Services.AddRazorPages();

#endregion

// ============================================================================
// REGION : Configuration Localisation
// ============================================================================
#region Localisation

/// <summary>
/// Cultures supportées par DRD v10.
/// </summary>
var supportedCultures = new[]
{
	new CultureInfo("fr-CA"),
	new CultureInfo("en-CA")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	options.DefaultRequestCulture = new RequestCulture("fr-CA", "fr-CA");
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;
});

#endregion

// ============================================================================
// REGION : Build + Pipeline HTTP
// ============================================================================
#region Pipeline

var app = builder.Build();

/// <summary>
/// Activation localisation.
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

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

#endregion

// ============================================================================
// REGION : Lancement
// ============================================================================
#region Run

/// <summary>
/// Lancement de l’application.
/// </summary>
Log.Information("DRD v10 - Application démarrée à {UtcTime}",
	DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture));

app.Run();

#endregion
