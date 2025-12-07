// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetDetailsVM.cs
// Type de fichier                Classe C#
// Classe                         CdSetDetailsVM
// Emplacement                    Models/GrpSystemTables/CdSetVM
// Entités concernées             CdSet (détails)
// Créé le                        2025-12-07
//
// Description
//     ViewModel en lecture seule utilisé pour l’affichage détaillé d’un
//     enregistrement CdSet. Affiche les champs principaux, l’état actif,
//     la description localisée, ainsi que les métadonnées (audit) via un
//     bouton dédié permettant d’ouvrir un modal DRD standard.
//
// Fonctionnalité
//     - Affichage de la famille (TypeCode) et du Code.
//     - Affichage des descriptions FR/EN + version localisée.
//     - Affichage du statut actif/inactif.
//     - Présentation des informations d’audit (CreatedBy, Dates, etc.).
//     - Support du ReturnUrl pour navigation contrôlée.
//     - Conformité totale au standard DRD v10.
//
// Modifications
//     2025-12-07    Version initiale DRD v10.
// ============================================================================

using System;
using System.ComponentModel.DataAnnotations;

namespace DRD.Web.Models.GrpSystemTables.CdSetVM
{
	/// <summary>
	/// ViewModel en lecture seule pour la consultation d’un CdSet.
	/// </summary>
	public class CdSetDetailsVM
	{
		// --------------------------------------------------------------------
		// REGION : Identification (lecture seule)
		// --------------------------------------------------------------------
		#region Identification

		/// <summary>
		/// Famille (TypeCode interne), affichée comme "Famille" via ressources.
		/// </summary>
		[Display(Name = "CdSet_Family_Label")]
		public string TypeCode { get; set; } = string.Empty;

		/// <summary>
		/// Code unique dans cette famille.
		/// </summary>
		[Display(Name = "CdSet_Code_Label")]
		public string Code { get; set; } = string.Empty;

		#endregion

		// --------------------------------------------------------------------
		// REGION : Descriptions
		// --------------------------------------------------------------------
		#region Descriptions

		[Display(Name = "CdSet_DescriptionFr_Label")]
		public string DescriptionFr { get; set; } = string.Empty;

		[Display(Name = "CdSet_DescriptionEn_Label")]
		public string? DescriptionEn { get; set; }

		/// <summary>
		/// Retourne la description FR ou EN selon la culture active.
		/// Logique appliquée dans le contrôleur ou l’UI (bilinguisme).
		/// </summary>
		public string DescriptionLocalized { get; set; } = string.Empty;

		#endregion

		// --------------------------------------------------------------------
		// REGION : État
		// --------------------------------------------------------------------
		#region État

		[Display(Name = "CdSet_IsActive_Label")]
		public bool IsActive { get; set; }

		#endregion

		// --------------------------------------------------------------------
		// REGION : Audit (affiché via un modal Metadata)
		// --------------------------------------------------------------------
		#region Audit

		public DateTime CreationDate { get; set; }
		public string? CreatedBy { get; set; }

		public DateTime ModificationDate { get; set; }
		public string? UpdatedBy { get; set; }

		#endregion

		// --------------------------------------------------------------------
		// REGION : Navigation
		// --------------------------------------------------------------------
		#region Navigation

		/// <summary>
		/// Permet de retourner à la page précédente après consultation.
		/// </summary>
		public string? ReturnUrl { get; set; }

		#endregion
	}
}
