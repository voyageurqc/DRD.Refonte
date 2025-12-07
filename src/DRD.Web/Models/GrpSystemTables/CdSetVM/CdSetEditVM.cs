// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetEditVM.cs
// Type de fichier                Classe C#
// Classe                         CdSetEditVM
// Emplacement                    Models/GrpSystemTables/CdSetVM
// Entités concernées             CdSet (édition)
// Créé le                        2025-12-07
//
// Description
//     ViewModel utilisé pour l’édition d’un enregistrement CdSet.
//     Les champs structuraux (Famille / Code) ne sont pas modifiables afin
//     d’assurer l’intégrité de la clé composite. Seules les descriptions et
//     l’état actif peuvent être modifiés.
//
// Fonctionnalité
//     - Affichage en lecture seule de la Famille et du Code.
//     - Modification des descriptions FR/EN.
//     - Activation / désactivation de l’entrée (IsActive).
//     - Conversion vers l’entité Domain via mutateurs DRD v10.
//     - Support d’un ReturnUrl pour une navigation cohérente.
//
// Modifications
//     2025-12-07    Version initiale DRD v10.
// ============================================================================

using DRD.Domain.Entities.GrpSystemTables;
using System.ComponentModel.DataAnnotations;

namespace DRD.Web.Models.GrpSystemTables.CdSetVM
{
	/// <summary>
	/// ViewModel utilisé pour la modification d’un CdSet existant.
	/// </summary>
	public class CdSetEditVM
	{
		// --------------------------------------------------------------------
		// REGION : Identification (lecture seule)
		// --------------------------------------------------------------------
		#region Identification

		/// <summary>
		/// Famille du code (TypeCode), affichée en lecture seule.
		/// </summary>
		[Display(Name = "CdSet_Family_Label")]
		public string TypeCode { get; set; } = string.Empty;

		/// <summary>
		/// Code unique dans la famille, affiché en lecture seule.
		/// </summary>
		[Display(Name = "CdSet_Code_Label")]
		public string Code { get; set; } = string.Empty;

		#endregion

		// --------------------------------------------------------------------
		// REGION : Descriptions éditables
		// --------------------------------------------------------------------
		#region Descriptions

		[Required]
		[StringLength(50)]
		[Display(Name = "CdSet_DescriptionFr_Label")]
		public string DescriptionFr { get; set; } = string.Empty;

		[StringLength(50)]
		[Display(Name = "CdSet_DescriptionEn_Label")]
		public string? DescriptionEn { get; set; }

		#endregion

		// --------------------------------------------------------------------
		// REGION : Paramètres d’état
		// --------------------------------------------------------------------
		#region État

		[Display(Name = "CdSet_IsActive_Label")]
		public bool IsActive { get; set; } = true;

		#endregion

		// --------------------------------------------------------------------
		// REGION : Navigation
		// --------------------------------------------------------------------
		#region Navigation

		/// <summary>
		/// Permet le retour à la bonne page après la sauvegarde.
		/// </summary>
		public string? ReturnUrl { get; set; }

		#endregion

		// --------------------------------------------------------------------
		// REGION : Mapping vers entité Domain
		// --------------------------------------------------------------------
		#region Mapping

		/// <summary>
		/// Applique les modifications à une entité CdSet existante.
		/// L’entité doit avoir été chargée depuis la base avant cet appel.
		/// </summary>
		/// <param name="entity">Entité existante à mettre à jour.</param>
		public void ApplyToEntity(CdSet entity)
		{
			entity.SetDescriptions(DescriptionFr, DescriptionEn);
			entity.IsActive = IsActive;
		}

		#endregion
	}
}
