// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 HomeController.cs
// Type de fichier                Contrôleur MVC
// Classe                         HomeController
// Emplacement                    Controllers
// Entités concernées             HomeVM, ApplicationUser
// Créé le                        2025-12-09
//
// Description
//     Contrôleur principal du site DRD. Gère l’affichage de la page d’accueil
//     pour les utilisateurs authentifiés ou non. Charge le nom complet de
//     l’utilisateur connecté via ASP.NET Identity.
//
// Fonctionnalité
//     - GET /Home/Index : affiche la page d’accueil et injecte le nom complet
//       de l’utilisateur lorsque disponible.
//     - Utilise UserManager<ApplicationUser> pour récupérer l’identité DRD.
//     - Journalisation légère via Serilog (diagnostic uniquement).
//
// Modifications
//     2025-12-09    Standardisation DRD v10 (en-tête, régions, XML, logging).
// ============================================================================

using DRD.Infrastructure.Identity;
using DRD.Web.Models.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DRD.Web.Controllers
{
    /// <summary>
    /// Contrôleur principal affichant la page d’accueil du système DRD.
    /// </summary>
    public class HomeController : Controller
    {
        #region DRD – Services
        /// <summary>
        /// Gestionnaire d'utilisateurs Identity.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Logger Serilog (diagnostic uniquement, messages toujours en anglais).
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Constructeur du contrôleur Home.
        /// </summary>
        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = Log.ForContext<HomeController>();
        }
        #endregion

        #region DRD – Actions GET
        /// <summary>
        /// Action principale de la page d’accueil.
        /// Si l’utilisateur est authentifié, on affiche son nom complet.
        /// </summary>
        /// <returns>Vue Home/Index avec HomeVM.</returns>
        public async Task<IActionResult> Index()
        {
            _logger.Information("HomeController - Index accessed.");

            var vm = new HomeVM();

            if (User?.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);

                vm.UserFullName = $"{user?.FirstName} {user?.LastName}".Trim();

                _logger.Information("Home - User authenticated: {Name}", vm.UserFullName);
            }
            else
            {
                _logger.Information("Home - Anonymous visitor.");
            }

            return View(vm);
        }
        #endregion
    }
}
