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
//     Contrôleur MVC responsable du changement de langue (culture) dans
//     l'application DRD. La bascule s'effectue via cookie, selon la norme
//     fr-CA / en-CA, et redirige l'utilisateur vers l'URL d'origine.
//
// Fonctionnalité
//     - ToggleLanguage : modifie la culture utilisateur via cookie sécurisé.
//     - Journalisation Serilog (anglais seulement) avec logger typé.
//     - Redirection sécurisée via LocalRedirect(returnUrl).
//
// Modifications
//     2025-12-09    Migration vers règles Serilog DRD v10 (logger typé).
//     2025-12-05    Création initiale DRD v10.
// ============================================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Globalization;

namespace DRD.Web.Controllers
{
    /// <summary>
    /// Controller responsible for managing language switch (culture)
    /// in the DRD system using cookie-based localization.
    /// </summary>
    [AllowAnonymous]
    public class LocalizationController : Controller
    {
        #region DRD – Services
        /// <summary>
        /// Serilog logger (typed for this controller).
        /// </summary>
        private readonly Serilog.ILogger _logger;

        /// <summary>
        /// Initializes a new instance of LocalizationController.
        /// </summary>
        public LocalizationController()
        {
            _logger = Log.ForContext<LocalizationController>();
        }
        #endregion

        #region DRD – Actions

        /// <summary>
        /// Toggles the current UI culture between French (fr-CA) and English (en-CA).
        /// </summary>
        /// <param name="returnUrl">
        /// URL to return to after the language switch. Must be local for security.
        /// </param>
        /// <returns>Redirects the user back to the provided returnUrl.</returns>
        [HttpPost]
        public IActionResult ToggleLanguage(string? returnUrl)
        {
            _logger.Information("Language switch requested.");

            var currentCulture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            _logger.Debug("Current culture detected: {Culture}", currentCulture);

            // Toggle rule : fr → en-CA, en → fr-CA
            var newCulture = currentCulture == "fr" ? "en-CA" : "fr-CA";
            _logger.Debug("Switching culture to: {NewCulture}", newCulture);

            // Writes the culture in the cookie used by ASP.NET Core localization
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(newCulture, newCulture)
                ),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true
                }
            );

            _logger.Information("Language changed to {NewCulture}", newCulture);

            // Secure redirection according to DRD v10 rules
            return LocalRedirect(returnUrl ?? "/");
        }

        #endregion
    }
}
