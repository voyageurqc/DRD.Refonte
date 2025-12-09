// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetDetailsVM.cs
// Type de fichier                ViewModel
// Classe                         CdSetDetailsVM
// Emplacement                    Models/GrpSystemTables/CdSetVM
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
//     2025-12-09    Conformité DRD v10 : ressources strongly-typed,
//                   ajout EntityName/UseActionButtons, localisation audit.
//     2025-12-07    Version initiale DRD v10.
// ============================================================================

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DRD.Resources.SystemTables;
using DRD.Resources.Common;

namespace DRD.Web.Models.GrpSystemTables.CdSetVM
{
    /// <summary>
    /// ViewModel en lecture seule pour la consultation d’un CdSet.
    /// </summary>
    public class CdSetDetailsVM
    {
        // --------------------------------------------------------------------
        // REGION : Identification
        // --------------------------------------------------------------------
        /// <summary>Champs structuraux TypeCode et Code.</summary>
        #region Identification

        [Display(Name = nameof(SystemTables.CdSet_Family_Label), ResourceType = typeof(SystemTables))]
        public string TypeCode { get; set; } = string.Empty;

        [Display(Name = nameof(SystemTables.CdSet_Code_Label), ResourceType = typeof(SystemTables))]
        public string Code { get; set; } = string.Empty;

        #endregion


        // --------------------------------------------------------------------
        // REGION : Descriptions
        // --------------------------------------------------------------------
        /// <summary>Descriptions FR/EN + version locale.</summary>
        #region Descriptions

        [Display(Name = nameof(SystemTables.CdSet_DescriptionFr_Label), ResourceType = typeof(SystemTables))]
        public string DescriptionFr { get; set; } = string.Empty;

        [Display(Name = nameof(SystemTables.CdSet_DescriptionEn_Label), ResourceType = typeof(SystemTables))]
        public string? DescriptionEn { get; set; }

        /// <summary>
        /// Version localisée de la description selon la culture active.
        /// </summary>
        public string DescriptionLocalized =>
            CultureInfo.CurrentUICulture.Name.StartsWith("en", StringComparison.OrdinalIgnoreCase)
                ? (DescriptionEn ?? DescriptionFr)
                : DescriptionFr;

        #endregion


        // --------------------------------------------------------------------
        // REGION : État
        // --------------------------------------------------------------------
        /// <summary>Statut actif/inactif.</summary>
        #region État

        [Display(Name = nameof(SystemTables.CdSet_IsActive_Label), ResourceType = typeof(SystemTables))]
        public bool IsActive { get; set; }

        #endregion


        // --------------------------------------------------------------------
        // REGION : Audit
        // --------------------------------------------------------------------
        /// <summary>Métadonnées d’audit.</summary>
        #region Audit

        [Display(Name = nameof(Common.CreatedOn), ResourceType = typeof(Common))]
        public DateTime CreationDate { get; set; }

        [Display(Name = nameof(Common.CreatedBy), ResourceType = typeof(Common))]
        public string? CreatedBy { get; set; }

        [Display(Name = nameof(Common.ModifiedOn), ResourceType = typeof(Common))]
        public DateTime ModificationDate { get; set; }

        [Display(Name = nameof(Common.ModifiedBy), ResourceType = typeof(Common))]
        public string? UpdatedBy { get; set; }

        #endregion


        // --------------------------------------------------------------------
        // REGION : Navigation
        // --------------------------------------------------------------------
        /// <summary>Retour contrôlé.</summary>
        #region Navigation

        public string? ReturnUrl { get; set; }

        #endregion


        // --------------------------------------------------------------------
        // REGION : Actions DRD
        // --------------------------------------------------------------------
        /// <summary>Boutons action standard DRD.</summary>
        #region Actions

        public string EntityName { get; set; } = "CdSet";
        public bool UseActionButtons { get; set; } = true;

        #endregion
    }
}
