// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 LoginVM.cs
// Type de fichier                ViewModel
// Classe                         LoginVM
// Emplacement                    Models/Account
// Entité(s)                      LoginVM
// Créé le                        2025-12-03
//
// Description
//     Modèle de vue pour la connexion des utilisateurs. Utilise l'approche
//     de localisation par ResourceType/ResourceName pour garantir la détection
//     des erreurs de localisation à la compilation.
//
// Fonctionnalité
//     - Validation des champs.
//     - Localisation via ressources DRD pour la robustesse à la compilation.
//     - Compatible avec AccountController (Login GET/POST).
//
// Modifications
//     2025-12-03    Version traditionnelle .NET v10 basée sur les attributs
//                   ResourceName + ResourceType (validation runtime).
//     2025-12-05    Uniformisation des noms de ressources d'erreur :
//                   FieldRequired -> Validation_Required; InvalidEmail -> Validation_Email.
// ============================================================================

using DRD.Resources.Common;
using DRD.Resources.LabelNames;
using System.ComponentModel.DataAnnotations;

namespace DRD.Web.Models.Account
{
	public class LoginVM
	{
		[Required(ErrorMessageResourceName = "Validation_Required",
				 ErrorMessageResourceType = typeof(Common))]
		[EmailAddress(ErrorMessageResourceName = "Validation_Email",
					 ErrorMessageResourceType = typeof(Common))]
		[Display(Name = "Email", ResourceType = typeof(EntityLN))]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessageResourceName = "Validation_Required",
				 ErrorMessageResourceType = typeof(Common))]
		[DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(EntityLN))]
		public string Password { get; set; } = string.Empty;

		[Display(Name = "RememberMe", ResourceType = typeof(EntityLN))]
		public bool RememberMe { get; set; }
	}
}