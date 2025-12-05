// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 LocalizationController.cs
// Type de fichier                Contrôleur MVC
// Classe                         LocalizationController
// Emplacement                    Controllers
// Entités concernées             n/a
// Créé le                        2025-12-05
//
// Description
//     Contrôleur MVC responsable de la gestion du changement de culture
//     (langue) de l'application via un cookie. Logique migrée de la v9.
//
// Fonctionnalité
//     - ToggleLanguage : Permet de basculer la langue et de rediriger
//       l'utilisateur vers la page précédente via un champ returnUrl (V10 standard).
//     - Journalisation Serilog (migrée de la v9).
//
// Modifications
//     2025-12-05    Création initiale et migration de la logique v9. Remplacement de la
//                   redirection par Request.Headers["Referer"] par la méthode
//                   sécurisée LocalRedirect(returnUrl).
// ============================================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Globalization;
using System.Linq; // Nécessaire pour FirstOrDefault() si vous l'utilisez, mais nous le retirons ici.

namespace DRD.Web.Controllers
{
	[AllowAnonymous]

	/// <summary>
	/// Controller responsible for handling language and localization operations.
	/// </summary>
	public class LocalizationController : Controller
	{
		/// <summary>
		/// Toggles the current language between French and English.
		/// </summary>
		/// <param name="returnUrl">URL de la page à laquelle retourner après le changement de culture.</param>
		/// <returns>Redirects to the previous page after toggling the language.</returns>
		[HttpPost]
		public IActionResult ToggleLanguage(string returnUrl) // AJOUTÉ returnUrl en paramètre
		{
			Log.Information("🌐 Language toggle requested.");

			// Votre logique v9 pour récupérer la culture
			var currentCulture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
			Log.Debug("🌍 Current culture detected: {Culture}", currentCulture);

			// Toggle entre "fr-CA" et "en-CA"
			var newCulture = currentCulture == "fr" ? "en-CA" : "fr-CA";
			Log.Debug("🔄 Switching culture to: {NewCulture}", newCulture);

			// Stocker la nouvelle culture dans un cookie
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(newCulture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
			);

			Log.Information("✅ Language toggled to {NewCulture}", newCulture);

			// Redirection V10 sécurisée
			// returnUrl contient la valeur de @Context.Request.Path que nous avons passée via le champ caché.
			return LocalRedirect(returnUrl ?? "/");
		}
	}
}