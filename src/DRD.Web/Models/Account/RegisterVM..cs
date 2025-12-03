// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 RegisterVM.cs
// Type de fichier                ViewModel
// Classe                         RegisterVM
// Emplacement                    Models/Account
// Entités concernées             RegisterVM, ApplicationUser
// Créé le                        2025-12-03
//
// Description
//     Modèle d'inscription permettant la création d'un utilisateur Identity
//     enrichi selon les besoins du système DRD.
//
// Fonctionnalité
//     - Champs personnels essentiels (prénom, nom).
//     - Champs Identity standard (email, mot de passe).
//     - Champs préférences DRD (SectorCode, imprimantes).
//     - Validation complète avec localisation.
//     - Compatible avec AccountController (Register GET/POST).
//
// Modifications
//     2025-12-03    Version initiale DRD v10 (remplace RegisterViewModel v9).
// ============================================================================

using System.ComponentModel.DataAnnotations;
using DRD.Resources.Common;
using DRD.Resources.FieldNames;

namespace DRD.Web.Models.Account
{
	/// <summary>
	/// ViewModel utilisé pour la création d'un utilisateur DRD.
	/// </summary>
	public class RegisterVM
	{
		[Required(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "FieldRequired")]
		[Display(Name = "FirstName", ResourceType = typeof(FieldNames))]
		public string FirstName { get; set; } = string.Empty;

		[Required(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "FieldRequired")]
		[Display(Name = "LastName", ResourceType = typeof(FieldNames))]
		public string LastName { get; set; } = string.Empty;

		[Required(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "FieldRequired")]
		[EmailAddress(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "InvalidEmail")]
		[Display(Name = "Email", ResourceType = typeof(FieldNames))]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "FieldRequired")]
		[StringLength(100, MinimumLength = 6,
			ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "PasswordTooShort")]
		[DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(FieldNames))]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "FieldRequired")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "PasswordsDoNotMatch")]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(FieldNames))]
		public string ConfirmPassword { get; set; } = string.Empty;

		[Required(ErrorMessageResourceType = typeof(Common), ErrorMessageResourceName = "FieldRequired")]
		[Display(Name = "SectorCode", ResourceType = typeof(FieldNames))]
		public string SectorCode { get; set; } = string.Empty;

		[Display(Name = "DefaultPrinter", ResourceType = typeof(FieldNames))]
		public string? DefaultPrinter { get; set; }

		[Display(Name = "LaserPrinter", ResourceType = typeof(FieldNames))]
		public string? LaserPrinter { get; set; }
	}
}
