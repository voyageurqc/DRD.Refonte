// ============================================================================
// Projet:      DRD.Web
// Fichier:     CdSetCreateVM.cs
// Type:        ViewModel
// Classe:      CdSetCreateVM
// Emplacement: Models/GrpSystemTables/CdSetVM
// Entité(s):   CdSet (création)
// Créé le:     2025-12-07
//
// Description:
//     ViewModel utilisé pour la création d’un nouvel enregistrement CdSet.
//     Supporte la sélection d'une famille existante ou la création d'une nouvelle.
//     Validation DRD v10 conditionnelle et bilingue via ressources strongly-typed.
//
// Fonctionnalité:
//     - Sélection d’une famille existante ou ajout d’une nouvelle.
//     - Validation conditionnelle NewFamily obligatoire si NEW_OPTION sélectionné.
//     - Normalisation (Trim) cohérente avec la validation Domain.
//     - Conversion sécurisée vers l’entité Domain via mutateurs.
//     - Support ReturnUrl et actions DRD dans les vues.
//
// Modifications:
//	   2025-12-13    Suppression définitive de _NEW_ :
//					- aucune valeur technique exposée
//					- nouvelle famille déclenchée par SelectedFamily vide
//     2025-12-11	Ajout interface IValidatableObject, validation conditionnelle,
//					renommage NEW_OPTION, Trim(), ToEntity corrigé (IsActive).
//     2025-12-09	Harmonisation AvailableFamilies/SelectedFamily/NewFamily.
//     2025-12-09	Conformité DRD v10 : ressources strongly-typed + metadata actions.
//     2025-12-07	Version initiale DRD v10.
// ============================================================================

using System.ComponentModel.DataAnnotations;
using DRD.Domain.Entities.GrpSystemTables;
using DRD.Resources.Common;
using DRD.Resources.LabelNames;
using DRD.Resources.MessagesMetier.CdSet;

namespace DRD.Web.Models.GrpSystemTables.CdSetVM
{
	/// <summary>
	/// ViewModel utilisé pour la création d’un nouvel enregistrement CdSet.
	/// Intègre validation conditionnelle et normalisation DRD v10.
	/// </summary>
	public class CdSetCreateVM : IValidatableObject
	{
		/// <summary>
		/// Valeur interne du dropdown permettant d’afficher la zone "Nouvelle famille".
		/// </summary>

		// --------------------------------------------------------------------
		// REGION : Famille
		// --------------------------------------------------------------------
		#region Famille

		/// <summary>Liste des familles existantes à afficher dans le dropdown.</summary>
		/// <summary>Liste des familles existantes à afficher dans le dropdown.</summary>
		public IEnumerable<string> AvailableFamilies { get; set; } = [];

		/// <summary>Famille sélectionnée par l’usager (existante ou NEW_OPTION).</summary>
		[Display(Name = nameof(CdSetLN.Field_TypeCode), ResourceType = typeof(CdSetLN))]
		public string? SelectedFamily { get; set; }

		/// <summary>Nouvelle famille saisie si l’usager choisit NEW_OPTION.</summary>
		[StringLength(20)]
		[Display(Name = nameof(CdSetLN.NewFamily), ResourceType = typeof(CdSetLN))]
		public string? NewFamily { get; set; }

		/// <summary>
		/// Détermine la valeur finale du TypeCode selon le choix de l’usager.
		/// Trim systématique pour harmoniser avec la validation Domain.
		/// </summary>
		public string TypeCodeFinal =>
			!string.IsNullOrWhiteSpace(SelectedFamily)
				? SelectedFamily.Trim()
				: (NewFamily?.Trim() ?? string.Empty);

		#endregion

		// --------------------------------------------------------------------
		// REGION : Champs du Code
		// --------------------------------------------------------------------
		#region Code

		[Required(ErrorMessageResourceName = nameof(Common.Validation_Required),
				  ErrorMessageResourceType = typeof(Common))]
		[StringLength(20)]
		[Display(Name = nameof(CdSetLN.Field_Code), ResourceType = typeof(CdSetLN))]
		public string Code { get; set; } = string.Empty;

		[Required(ErrorMessageResourceName = nameof(Common.Validation_Required),
				  ErrorMessageResourceType = typeof(Common))]
		[StringLength(50)]
		[Display(Name = nameof(CdSetLN.Field_DescriptionFr), ResourceType = typeof(CdSetLN))]
		public string DescriptionFr { get; set; } = string.Empty;

		[StringLength(50)]
		[Display(Name = nameof(CdSetLN.Field_DescriptionEn), ResourceType = typeof(CdSetLN))]
		public string? DescriptionEn { get; set; }

		/// <summary>État actif/inactif du CdSet.</summary>
		public bool IsActive { get; set; } = true;

		/// <summary>URL de retour après création.</summary>
		public string? ReturnUrl { get; set; }

		#endregion

		// --------------------------------------------------------------------
		// REGION : Mapping Domain
		// --------------------------------------------------------------------
		#region Mapping

		/// <summary>
		/// Convertit ce ViewModel vers une entité CdSet Domain avec mutateurs sécurisés.
		/// </summary>
		public CdSet ToEntity()
		{
			var entity = new CdSet();

			entity.SetFamily(TypeCodeFinal);
			entity.SetCodeValue(Code?.Trim() ?? string.Empty);
			entity.SetDescriptions(DescriptionFr?.Trim() ?? string.Empty,
								   DescriptionEn?.Trim());
			entity.IsActive = IsActive;

			return entity;
		}

		#endregion

		// --------------------------------------------------------------------
		// REGION : Validation conditionnelle
		// --------------------------------------------------------------------
		#region Validation

		/// <summary>
		/// Validation conditionnelle DRD : NewFamily obligatoire si NEW_OPTION est sélectionné.
		/// </summary>
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			// Aucune famille sélectionnée → nouvelle famille obligatoire
			if (string.IsNullOrWhiteSpace(SelectedFamily)
				&& string.IsNullOrWhiteSpace(NewFamily))
			{
				yield return new ValidationResult(
					CdSetMM.Validation_NewFamilyRequired,
					[nameof(NewFamily)]
				);
			}
		}

		#endregion

		// --------------------------------------------------------------------
		// REGION : Actions DRD
		// --------------------------------------------------------------------
		#region Actions

		/// <summary>Nom de l’entité affichée dans les boutons d’actions DRD.</summary>
		public string EntityName { get; set; } = "CdSet";

		/// <summary>Indique s’il faut afficher les boutons d’action DRD.</summary>
		public bool UseActionButtons { get; set; } = true;

		#endregion
	}
}
