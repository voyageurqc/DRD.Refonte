// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetCreateVM.cs
// Type de fichier                Classe C#
// Classe                         CdSetCreateVM
// Emplacement                    Models/GrpSystemTables/CdSetVM
// Entités concernées             CdSet (création)
// Créé le                        2025-12-07
//
// Description
//     ViewModel utilisé pour la création d’un nouvel enregistrement CdSet.
//     Permet de sélectionner une famille existante (TypeCode) ou d’en créer
//     une nouvelle. Les labels affichés sont basés sur les ressources
//     bilingues afin d'assurer une interface cohérente.
//
// Fonctionnalité
//     - Sélection d’une famille existante (TypeCode).
//     - Création d’une nouvelle famille si l’option spéciale est choisie.
//     - Validation des champs requis.
//     - Conversion propre vers l’entité CdSet via les mutateurs Domain.
//
// Modifications
//     2025-12-07    Mise à jour ToEntity() pour utiliser SetFamily(), 
//                   SetCodeValue(), SetDescriptions() (DRD v10).
//     2025-12-07    Version initiale DRD v10.
// ============================================================================

using DRD.Domain.Entities.GrpSystemTables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DRD.Web.Models.GrpSystemTables.CdSetVM
{
	/// <summary>
	/// ViewModel utilisé pour la création d’un nouvel enregistrement CdSet.
	/// </summary>
	public class CdSetCreateVM
	{
		// --------------------------------------------------------------------
		// REGION : Famille (TypeCode UI)
		// --------------------------------------------------------------------
		#region Famille

		/// <summary>
		/// Liste des familles existantes (TypeCodes).
		/// </summary>
		public IEnumerable<string> AvailableTypeCodes { get; set; } = new List<string>();

		/// <summary>
		/// Famille sélectionnée (TypeCode existant).
		/// </summary>
		[Display(Name = "CdSet_Family_Label")]
		public string? SelectedTypeCode { get; set; }

		/// <summary>
		/// Nouvelle famille si l’utilisateur choisit l’option "Nouvelle famille".
		/// </summary>
		[Display(Name = "CdSet_Family_New_Label")]
		[StringLength(20)]
		public string? NewTypeCode { get; set; }

		/// <summary>
		/// Famille réellement utilisée (nouvelle ou existante).
		/// </summary>
		public string TypeCodeFinal =>
			SelectedTypeCode == "CdSet_Family_NewOption"
				? NewTypeCode ?? string.Empty
				: SelectedTypeCode ?? string.Empty;

		#endregion

		// --------------------------------------------------------------------
		// REGION : Champs du Code
		// --------------------------------------------------------------------
		#region Code

		[Required]
		[StringLength(20)]
		[Display(Name = "CdSet_Code_Label")]
		public string Code { get; set; } = string.Empty;

		[Required]
		[StringLength(50)]
		[Display(Name = "CdSet_DescriptionFr_Label")]
		public string DescriptionFr { get; set; } = string.Empty;

		[StringLength(50)]
		[Display(Name = "CdSet_DescriptionEn_Label")]
		public string? DescriptionEn { get; set; }

		#endregion

		// --------------------------------------------------------------------
		// REGION : Conversion vers entité Domain
		// --------------------------------------------------------------------
		#region Mapping

		/// <summary>
		/// Convertit ce ViewModel en entité Domain CdSet via les mutateurs Domain.
		/// L’audit (CreatedBy, UpdatedBy, Dates) sera géré dans le contrôleur.
		/// </summary>
		public CdSet ToEntity()
		{
			var entity = new CdSet();

			entity.SetFamily(TypeCodeFinal);
			entity.SetCodeValue(Code);
			entity.SetDescriptions(DescriptionFr, DescriptionEn);

			entity.IsActive = true;

			return entity;
		}

		#endregion
	}
}
