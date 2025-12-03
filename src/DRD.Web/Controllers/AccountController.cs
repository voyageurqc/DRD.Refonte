// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 AccountController.cs
// Type de fichier                Contrôleur MVC
// Classe                         AccountController
// Emplacement                    Controllers
// Entités concernées             LoginVM, RegisterVM, ApplicationUser, AccessType
// Créé le                        2025-12-03
//
// Description
//     Contrôleur Identity du projet DRD gérant la connexion, l'inscription,
//     la déconnexion et la page d'accès refusé. Remplace l'utilisation des
//     Areas Identity afin d'assurer une intégration totale avec le layout DRD,
//     les messages Toastr, les ressources bilingues et la journalisation.
//
// Fonctionnalité
//     - Connexion utilisateur (GET/POST).
//     - Création d'un nouvel utilisateur ApplicationUser.
//     - Validation modèle DRD (LoginVM, RegisterVM).
//     - Déconnexion sécurisée.
//     - Gestion AccessDenied.
//     - Messages Toastr basés sur DRD.Resources.
//     - Journalisation complète via ILogger<T> (Serilog en backend).
//
// Modifications
//     2025-12-03    Version initiale DRD v10.
//     2025-12-03    Injection logger corrigée (ILogger<T>).
// ============================================================================

using DRD.Infrastructure.Identity;
using DRD.Resources.Common;
using DRD.Resources.Toastr;
using DRD.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DRD.Web.Controllers
{
	/// <summary>
	/// Contrôleur responsable de l'authentification, l'inscription,
	/// la déconnexion et la gestion des accès refusés.
	/// </summary>
	public class AccountController : Controller
	{
		#region Services
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ILogger<AccountController> _logger;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			ILogger<AccountController> logger)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
		}
		#endregion


		// ============================================================================
		// GET : /Account/Login
		// ============================================================================
		#region Login GET
		[HttpGet]
		public IActionResult Login(string? returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View(new LoginVM());
		}
		#endregion


		// ============================================================================
		// POST : /Account/Login
		// ============================================================================
		#region Login POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginVM model, string? returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;

			if (!ModelState.IsValid)
			{
				TempData["ToastrError"] = Toastr.LoginInvalid;
				return View(model);
			}

			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user is null)
			{
				TempData["ToastrError"] = Toastr.UserNotFound;
				_logger.LogWarning("Tentative de connexion avec un compte inexistant : {Email}", model.Email);
				return View(model);
			}

			var result = await _signInManager.PasswordSignInAsync(
				user,
				model.Password,
				model.RememberMe,
				lockoutOnFailure: false);

			if (result.Succeeded)
			{
				TempData["ToastrSuccess"] = Toastr.LoginSuccess;
				_logger.LogInformation("Utilisateur connecté : {Email}", model.Email);

				if (!string.IsNullOrEmpty(returnUrl))
					return Redirect(returnUrl);

				return RedirectToAction("Index", "Home");
			}

			TempData["ToastrError"] = Toastr.LoginInvalid;
			_logger.LogWarning("Connexion échouée pour : {Email}", model.Email);

			return View(model);
		}
		#endregion


		// ============================================================================
		// GET : /Account/Register
		// ============================================================================
		#region Register GET
		[HttpGet]
		public IActionResult Register()
		{
			return View(new RegisterVM());
		}
		#endregion


		// ============================================================================
		// POST : /Account/Register
		// ============================================================================
		#region Register POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterVM model)
		{
			if (!ModelState.IsValid)
			{
				TempData["ToastrError"] = Toastr.RegisterInvalid;
				return View(model);
			}

			var user = new ApplicationUser
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				SectorCode = model.SectorCode,
				DefaultPrinter = model.DefaultPrinter ?? string.Empty,
				LaserPrinter = model.LaserPrinter ?? string.Empty,
				CreationDate = DateTime.UtcNow,
				ModificationDate = DateTime.UtcNow,
				IsActive = true
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);

				TempData["ToastrError"] = Toastr.RegisterInvalid;
				_logger.LogWarning("Échec lors de la création du compte : {Email}", model.Email);

				return View(model);
			}

			TempData["ToastrSuccess"] = Toastr.RegisterSuccess;
			_logger.LogInformation("Nouvel utilisateur créé : {Email}", model.Email);

			await _signInManager.SignInAsync(user, isPersistent: false);

			return RedirectToAction("Index", "Home");
		}
		#endregion


		// ============================================================================
		// POST : /Account/Logout
		// ============================================================================
		#region Logout
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			TempData["ToastrSuccess"] = Toastr.LogoutSuccess;
			_logger.LogInformation("Utilisateur déconnecté.");

			return RedirectToAction("Login");
		}
		#endregion


		// ============================================================================
		// GET : /Account/AccessDenied
		// ============================================================================
		#region AccessDenied
		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}
		#endregion
	}
}
