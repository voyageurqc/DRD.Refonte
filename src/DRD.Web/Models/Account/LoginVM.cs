// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 LoginVM.cs
// Type de fichier                ViewModel
// Classe                         LoginVM
// Emplacement                    Models/Account
// Entités concernées             LoginVM
// Créé le                        2025-12-03
//
// Description
//     Modèle de données utilisé pour la connexion des utilisateurs dans DRD.
//     Permet la saisie du courriel, du mot de passe et de l’option RememberMe.
//
// Fonctionnalité
//     - Validation des champs.
//     - Localisation via ressources DRD.
//     - Compatible avec AccountController (Login GET/POST).
//
// Modifications
//     2025-12-03    Version initiale DRD v10 (remplace LoginViewModel v9).
// ============================================================================

using System.ComponentModel.DataAnnotations;
using DRD.Resources.Common;
using DRD.Resources.FieldNames;

namespace DRD.Web.Models.Account
{
	/// <summary>
	/// ViewModel utilisé pour l'écran de connexion DRD.
	/// </summary>
	public class LoginVM
	{
		[Required(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "FieldRequired")]
		[EmailAddress(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "InvalidEmail")]
		[Display(Name = "Email", ResourceType = typeof(FieldNames))]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "FieldRequired")]
		[DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(FieldNames))]
		public string Password { get; set; } = string.Empty;

		[Display(Name = "RememberMe", ResourceType = typeof(FieldNames))]
		public bool RememberMe { get; set; }
	}
}
