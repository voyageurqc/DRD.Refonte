// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 RegisterVM.cs
// Type de fichier                ViewModel
// Classe                         RegisterVM
// Emplacement                    Models/Account
// Entités concernées             ApplicationUser
// Créé le                        2025-12-03 (Date d'origine de la VM)
//
// Description
//     Modèle de vue pour l'inscription des utilisateurs. Utilise l'approche
//     de localisation par ResourceType/ResourceName pour garantir la détection
//     des erreurs de localisation à la compilation.
//
// Fonctionnalité
//     - Contient les champs nécessaires à la création d'un ApplicationUser.
//     - Utilise des références statiques aux classes de ressources pour la robustesse.
//
// Modifications
//     2025-12-05    Renommage en RegisterVM. Remplacement de FullName par FirstName
//                   et LastName. Implémentation de la localisation par ResourceType/Name
//                   pour la détection des erreurs de ressources à la compilation.
// ============================================================================

using System.ComponentModel.DataAnnotations;
using DRD.Resources.Common;
using DRD.Resources.FieldNames;

namespace DRD.Web.Models.Account
{
	public class RegisterVM
	{
		#region Identification

		/// <summary>
		/// Prénom de l'utilisateur, obligatoire.
		/// </summary>
		[Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Common))]
		[Display(Name = "FirstName", ResourceType = typeof(FieldNames))]
		public string FirstName { get; set; } = string.Empty;

		/// <summary>
		/// Nom de l'utilisateur, obligatoire.
		/// </summary>
		[Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Common))]
		[Display(Name = "LastName", ResourceType = typeof(FieldNames))]
		public string LastName { get; set; } = string.Empty;

		/// <summary>
		/// Adresse courriel de l'utilisateur (utilisée comme nom d'utilisateur).
		/// </summary>
		[Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Common))]
		[EmailAddress(ErrorMessageResourceName = "Validation_Email", ErrorMessageResourceType = typeof(Common))]
		[Display(Name = "Email", ResourceType = typeof(FieldNames))]
		public string Email { get; set; } = string.Empty;

		#endregion

		#region Mot de passe

		/// <summary>
		/// Mot de passe du compte.
		/// </summary>
		[Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Common))]
		[DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(FieldNames))]
		public string Password { get; set; } = string.Empty;

		/// <summary>
		/// Confirmation du mot de passe.
		/// </summary>
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessageResourceName = "Validation_ComparePassword", ErrorMessageResourceType = typeof(Common))]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(FieldNames))]
		public string ConfirmPassword { get; set; } = string.Empty;

		#endregion

		#region Préférences de l'utilisateur

		/// <summary>
		/// Code du secteur de l'utilisateur, obligatoire.
		/// </summary>
		[Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Common))]
		[Display(Name = "SectorCode", ResourceType = typeof(FieldNames))]
		public string SectorCode { get; set; } = string.Empty;

		/// <summary>
		/// Imprimante par défaut.
		/// </summary>
		[Display(Name = "DefaultPrinter", ResourceType = typeof(FieldNames))]
		public string DefaultPrinter { get; set; } = string.Empty;

		/// <summary>
		/// Imprimante laser.
		/// </summary>
		[Display(Name = "LaserPrinter", ResourceType = typeof(FieldNames))]
		public string LaserPrinter { get; set; } = string.Empty;

		#endregion
	}
}