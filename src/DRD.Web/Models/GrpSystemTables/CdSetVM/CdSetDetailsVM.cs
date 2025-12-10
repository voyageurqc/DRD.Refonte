// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetDetailsVM.cs
// Type de fichier                ViewModel
// Classe                         CdSetDetailsVM
// Emplacement                    Models/GrpCdSetLN/CdSetVM
// Entités concernées             CdSet (détails)
// Créé le                        2025-12-07
//
// Description
//     ViewModel en lecture seule utilisé pour l’affichage détaillé d’un
//     enregistrement CdSet. Affiche les champs principaux, l’état actif,
//     la description locale, ainsi que les métadonnées (audit) via un
//     modal dédié DRD.
//
// Fonctionnalité
//     - Affichage TypeCode, Code, descriptions FR/EN/localisée.
//     - Statut actif/inactif.
//     - Présentation audit (CreatedBy / UpdatedBy / dates).
//     - Navigation DRD + actions standardisées.
//
// Modifications
//     2025-12-11    Ajout complet des commentaires XML selon DRD v10 (rules #90-#91).
//     2025-12-09    Conformité DRD v10 : ressources strongly-typed,
//                   ajout EntityName/UseActionButtons, localisation audit.
//     2025-12-07    Version initiale DRD v10.
// ============================================================================

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DRD.Resources.Common;
using DRD.Resources.LabelNames;

namespace DRD.Web.Models.GrpCdSetLN.CdSetVM
{
	/// <summary>
	/// ViewModel en lecture seule utilisé pour l’affichage détaillé d’un CdSet.
	/// Contient les champs de structure, les descriptions, l’état actif et les métadonnées audit.
	/// </summary>
	public class CdSetDetailsVM
	{
		// --------------------------------------------------------------------
		// REGION : Identification
		// --------------------------------------------------------------------
		#region Identification
		/// <summary>
		/// Famille du paramètre (clé composite partie 1).
		/// </summary>
		[Display(Name = nameof(CdSetLN.Field_TypeCode), ResourceType = typeof(CdSetLN))]
		public string TypeCode { get; set; } = string.Empty;

		/// <summary>
		/// Code unique dans la famille (clé composite partie 2).
		/// </summary>
		[Display(Name = nameof(CdSetLN.Field_Code), ResourceType = typeof(CdSetLN))]
		public string Code { get; set; } = string.Empty;
		#endregion

		// --------------------------------------------------------------------
		// REGION : Descriptions
		// --------------------------------------------------------------------
		#region Descriptions

		/// <summary>
		/// Description française.
		/// </summary>
		[Display(Name = nameof(CdSetLN.Field_DescriptionFr), ResourceType = typeof(CdSetLN))]
		public string DescriptionFr { get; set; } = string.Empty;

		/// <summary>
		/// Description anglaise.
		/// </summary>
		[Display(Name = nameof(CdSetLN.Field_DescriptionEn), ResourceType = typeof(CdSetLN))]
		public string? DescriptionEn { get; set; }

		/// <summary>
		/// Version localisée de la description, dépendante de la culture courante.
		/// </summary>
		public string DescriptionLocalized =>
			CultureInfo.CurrentUICulture.Name.StartsWith("en", StringComparison.OrdinalIgnoreCase)
				? (DescriptionEn ?? DescriptionFr)
				: DescriptionFr;

		#endregion

		// --------------------------------------------------------------------
		// REGION : État
		// --------------------------------------------------------------------
		#region État

		/// <summary>
		/// Indique si le code paramétrique est actif ou désactivé.
		/// </summary>
		[Display(Name = nameof(Common.IsActive), ResourceType = typeof(Common))]
		public bool IsActive { get; set; }

		#endregion

		// --------------------------------------------------------------------
		// REGION : Audit
		// --------------------------------------------------------------------
		#region Audit

		/// <summary>
		/// Date de création de l’enregistrement.
		/// </summary>
		[Display(Name = nameof(Common.CreatedOn), ResourceType = typeof(Common))]
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Identité de l’utilisateur ayant créé l’enregistrement.
		/// </summary>
		[Display(Name = nameof(Common.CreatedBy), ResourceType = typeof(Common))]
		public string? CreatedBy { get; set; }

		/// <summary>
		/// Date de la dernière modification.
		/// </summary>
		[Display(Name = nameof(Common.ModifiedOn), ResourceType = typeof(Common))]
		public DateTime ModificationDate { get; set; }

		/// <summary>
		/// Identité de l’utilisateur ayant effectué la dernière modification.
		/// </summary>
		[Display(Name = nameof(Common.ModifiedBy), ResourceType = typeof(Common))]
		public string? UpdatedBy { get; set; }

		#endregion

		// --------------------------------------------------------------------
		// REGION : Navigation
		// --------------------------------------------------------------------
		#region Navigation

		/// <summary>
		/// URL de retour contrôlé vers l’écran précédent.
		/// </summary>
		public string? ReturnUrl { get; set; }

		#endregion

		// --------------------------------------------------------------------
		// REGION : Actions DRD
		// --------------------------------------------------------------------
		#region Actions

		/// <summary>
		/// Nom de l’entité utilisé dans les boutons et métadonnées globales.
		/// </summary>
		public string EntityName { get; set; } = "CdSet";

		/// <summary>
		/// Contrôle l’affichage des boutons d’action standard DRD.
		/// </summary>
		public bool UseActionButtons { get; set; } = true;

		#endregion
	}
}
