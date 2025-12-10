// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetEditVM.cs
// Type de fichier                ViewModel
// Classe                         CdSetEditVM
// Emplacement                    Models/GrpCdSetLN/CdSetVM
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
//     - Support d’un ReturnUrl pour une navigation cohérente.
//     - Compatible avec les actions standard DRD (View/Edit/Delete).
//
// Modifications
//     2025-12-11    DRD v10 : correction CS1587 (summary de région → commentaire régulier)
//                   et maintien CS1591 (XML sur propriétés uniquement).
//     2025-12-11    DRD v10 : ajout des commentaires XML requis pour CS1591.
//     2025-12-09    Conformité DRD v10 : ressources strongly-typed + ajout UseActionButtons.
//     2025-12-07    Version initiale DRD v10.
// ============================================================================

using System.ComponentModel.DataAnnotations;
using DRD.Resources.Common;
using DRD.Resources.LabelNames;

namespace DRD.Web.Models.GrpCdSetLN.CdSetVM
{
	/// <summary>
	/// ViewModel utilisé pour la modification d’un enregistrement CdSet existant.
	/// Contient les champs structuraux non modifiables et les champs éditables.
	/// </summary>
	public class CdSetEditVM
	{
		// --------------------------------------------------------------------
		// REGION : Identification
		// --------------------------------------------------------------------
		// <summary>
		//     Informations d’identification de la clé composite TypeCode + Code (lecture seule).
		// </summary>
		#region Identification

		/// <summary>
		/// Famille du code paramétrique. Non modifiable afin de préserver la clé composite.
		/// </summary>
		[Display(Name = nameof(CdSetLN.Field_TypeCode), ResourceType = typeof(CdSetLN))]
		public string TypeCode { get; set; } = string.Empty;

		/// <summary>
		/// Code unique dans la famille. Non modifiable.
		/// </summary>
		[Display(Name = nameof(CdSetLN.Field_Code), ResourceType = typeof(CdSetLN))]
		public string Code { get; set; } = string.Empty;

		#endregion


		// --------------------------------------------------------------------
		// REGION : Descriptions
		// --------------------------------------------------------------------
		// <summary>
		//     Descriptions bilingues modifiables (FR / EN).
		// </summary>
		#region Descriptions

		/// <summary>
		/// Description française. Champ requis.
		/// </summary>
		[Required(ErrorMessageResourceName = nameof(Common.Validation_Required),
				  ErrorMessageResourceType = typeof(Common))]
		[StringLength(50)]
		[Display(Name = nameof(CdSetLN.Field_DescriptionFr), ResourceType = typeof(CdSetLN))]
		public string DescriptionFr { get; set; } = string.Empty;

		/// <summary>
		/// Description anglaise. Optionnelle.
		/// </summary>
		[StringLength(50)]
		[Display(Name = nameof(CdSetLN.Field_DescriptionEn), ResourceType = typeof(CdSetLN))]
		public string? DescriptionEn { get; set; }

		#endregion


		// --------------------------------------------------------------------
		// REGION : État
		// --------------------------------------------------------------------
		// <summary>
		//     Indicateur d’activation ou de désactivation du CdSet.
		// </summary>
		#region État

		/// <summary>
		/// True si l’entrée est active, False si désactivée.
		/// </summary>
		[Display(Name = nameof(Common.IsActive), ResourceType = typeof(Common))]
		public bool IsActive { get; set; } = true;

		#endregion


		// --------------------------------------------------------------------
		// REGION : Navigation
		// --------------------------------------------------------------------
		// <summary>
		//     Paramètre de navigation permettant un retour contrôlé vers la page précédente.
		// </summary>
		#region Navigation

		/// <summary>
		/// URL de retour après l'édition.
		/// </summary>
		public string? ReturnUrl { get; set; }

		#endregion


		// --------------------------------------------------------------------
		// REGION : Actions DRD
		// --------------------------------------------------------------------
		// <summary>
		//     Paramètres activant les boutons DRD standardisés (View / Edit / Delete).
		// </summary>
		#region Actions

		/// <summary>
		/// Indique si les boutons d’action standard DRD doivent être affichés.
		/// </summary>
		public bool UseActionButtons { get; set; } = true;

		#endregion
	}
}
