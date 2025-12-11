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
//     2025-12-11   Ajout complet des /// XML documentation pour conformité CS1591.
//     2025-12-05   Renommage en RegisterVM. Remplacement de FullName par FirstName
//                  et LastName. Implémentation de la localisation par ResourceType/Name
//                  pour la détection des erreurs de ressources à la compilation.
// ============================================================================

using System.ComponentModel.DataAnnotations;
using DRD.Resources.Common;
using DRD.Resources.LabelNames;

namespace DRD.Web.Models.Account
{
	/// <summary>
	/// ViewModel utilisé pour l'inscription d'un nouvel utilisateur DRD.
	/// Contient l'ensemble des champs nécessaires à la création d'un ApplicationUser,
	/// incluant informations d'identification, mot de passe et préférences.
	/// </summary>
	public class RegisterVM
	{
		// --------------------------------------------------------------------
		// REGION : Identification
		// --------------------------------------------------------------------
		#region Identification

		/// <summary>
		/// Prénom de l'utilisateur. Champ obligatoire.
		/// </summary>
		[Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Common))]
		[Display(Name = "FirstName", ResourceType = typeof(EntityLN))]
		public string FirstName { get; set; } = string.Empty;

		/// <summary>
		/// Nom de famille de l'utilisateur. Champ obligatoire.
		/// </summary>
		[Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Common))]
		[Display(Name = "LastName", ResourceType = typeof(EntityLN))]
		public string LastName { get; set; } = string.Empty;

		/// <summary>
		/// Adresse courriel servant d'identifiant de connexion. Champ obligatoire.
		/// </summary>
		[Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Common))]
		[EmailAddress(ErrorMessageResourceName = "Validation_Email", ErrorMessageResourceType = typeof(Common))]
		[Display(Name = "Email", ResourceType = typeof(EntityLN))]
		public string Email { get; set; } = string.Empty;

		#endregion

		// --------------------------------------------------------------------
		// REGION : Mot de passe
		// --------------------------------------------------------------------
		#region Mot de passe

		/// <summary>
		/// Mot de passe du compte utilisateur. Champ obligatoire.
		/// </summary>
		[Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Common))]
		[DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(EntityLN))]
		public string Password { get; set; } = string.Empty;

		/// <summary>
		/// Confirmation du mot de passe saisie par l'utilisateur.
		/// Elle doit correspondre au champ <see cref="Password"/>.
		/// </summary>
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessageResourceName = "Validation_ComparePassword", ErrorMessageResourceType = typeof(Common))]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(EntityLN))]
		public string ConfirmPassword { get; set; } = string.Empty;

		#endregion

		// --------------------------------------------------------------------
		// REGION : Préférences de l'utilisateur
		// --------------------------------------------------------------------
		#region Préférences de l'utilisateur

		/// <summary>
		/// Code du secteur auquel l'utilisateur appartient. Champ obligatoire.
		/// </summary>
		[Required(ErrorMessageResourceName = "Validation_Required", ErrorMessageResourceType = typeof(Common))]
		[Display(Name = "SectorCode", ResourceType = typeof(EntityLN))]
		public string SectorCode { get; set; } = string.Empty;

		/// <summary>
		/// Imprimante par défaut de l'utilisateur (facultatif).
		/// </summary>
		[Display(Name = "DefaultPrinter", ResourceType = typeof(EntityLN))]
		public string DefaultPrinter { get; set; } = string.Empty;

		/// <summary>
		/// Imprimante laser préférée de l'utilisateur (facultatif).
		/// </summary>
		[Display(Name = "LaserPrinter", ResourceType = typeof(EntityLN))]
		public string LaserPrinter { get; set; } = string.Empty;

		#endregion
	}
}
