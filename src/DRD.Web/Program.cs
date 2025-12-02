// ============================================================================
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
//
// Fonctionnalité
//     - Initialiser la journalisation Serilog (console + fichier).
//     - Configurer le DbContext ApplicationDbContext avec la BD DRDv10.
//     - Configurer ASP.NET Identity avec ApplicationUser.
//     - Activer la localisation fr-CA / en-CA.
//     - Enregistrer MVC (controllers + views).
//     - Configurer le pipeline HTTP (erreurs, HTTPS, routage, auth).
//
// Modifications
//     2025-12-02    Clean start .NET 10 + intégration Serilog DEV + PROD.
// ============================================================================

using System.Globalization;
using DRD.Infrastructure.Data;
using DRD.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Serilog;

// ---------------------------------------------------------
// DRD – Bootstrap du logger (Serilog DEV + PROD)
// ---------------------------------------------------------

var logDirectory = Path.GetFullPath(
	Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Logs"));

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
	/// Active la configuration Serilog complète pour l'application.
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
			// DEV : fichier unique par lancement
			configuration.WriteTo.File(devLogFile);
		}
		else
		{
			// PROD : rolling par jour
			configuration.WriteTo.File(
				path: prodLogPattern,
				rollingInterval: RollingInterval.Day);
		}
	});
	#endregion


	#region DRD – DbContext
	/// <summary>
	/// Configuration de la base de données DRDv10.
	/// </summary>
	var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

	if (string.IsNullOrWhiteSpace(connectionString))
	{
		throw new InvalidOperationException(
			"La chaîne de connexion 'DefaultConnection' est manquante dans appsettings.json.");
	}

	builder.Services.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer(connectionString));
	#endregion


	#region DRD – Identity
	/// <summary>
	/// Active ASP.NET Identity avec ApplicationUser et rôles.
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
	/// Active la localisation FR-CA / EN-CA pour l'application.
	/// </summary>
	builder.Services.AddLocalization(options => options.ResourcesPath = "");

	builder.Services.AddControllersWithViews()
		.AddViewLocalization()
		.AddDataAnnotationsLocalization();

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
	/// Active les contrôleurs + vues.
	/// </summary>
	builder.Services.AddControllersWithViews();
	#endregion


	var app = builder.Build();


	#region DRD – Pipeline HTTP
	/// <summary>
	/// Pipeline HTTP de l'application (erreurs, HTTPS, static, auth, routes).
	/// </summary>

	// Localisation
	var localizationOptions = app.Services.GetRequiredService<
		Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>().Value;
	app.UseRequestLocalization(localizationOptions);

	// Gestion des erreurs
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

	// Routes MVC
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
