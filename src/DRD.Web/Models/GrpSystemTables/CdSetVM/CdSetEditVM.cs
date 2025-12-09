// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetEditVM.cs
// Type de fichier                ViewModel
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
//     - Support d’un ReturnUrl pour une navigation cohérente.
//     - Compatible avec les actions standard DRD (View/Edit/Delete).
//
// Modifications
//     2025-12-09    Conformité DRD v10 : ressources strongly-typed + ajout UseActionButtons
//                   + suppression ApplyToEntity.
//     2025-12-07    Version initiale DRD v10.
// ============================================================================

using System.ComponentModel.DataAnnotations;
using DRD.Resources.SystemTables;
using DRD.Resources.FieldNames;

namespace DRD.Web.Models.GrpSystemTables.CdSetVM
{
    /// <summary>
    /// ViewModel utilisé pour la modification d’un CdSet existant.
    /// </summary>
    public class CdSetEditVM
    {
        // --------------------------------------------------------------------
        // REGION : Identification
        // --------------------------------------------------------------------
        /// <summary>Champs structuraux TypeCode et Code (lecture seule).</summary>
        #region Identification

        [Display(Name = nameof(CdSet_Family_Label), ResourceType = typeof(SystemTables))]
        public string TypeCode { get; set; } = string.Empty;

        [Display(Name = nameof(CdSet_Code_Label), ResourceType = typeof(SystemTables))]
        public string Code { get; set; } = string.Empty;

        #endregion


        // --------------------------------------------------------------------
        // REGION : Descriptions
        // --------------------------------------------------------------------
        /// <summary>Descriptions éditables FR/EN.</summary>
        #region Descriptions

        [Required(ErrorMessageResourceName = nameof(Common.Validation_Required),
                  ErrorMessageResourceType = typeof(Common.Common))]
        [StringLength(50)]
        [Display(Name = nameof(CdSet_DescriptionFr_Label), ResourceType = typeof(SystemTables))]
        public string DescriptionFr { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = nameof(CdSet_DescriptionEn_Label), ResourceType = typeof(SystemTables))]
        public string? DescriptionEn { get; set; }

        #endregion


        // --------------------------------------------------------------------
        // REGION : État
        // --------------------------------------------------------------------
        /// <summary>Activation / désactivation du CdSet.</summary>
        #region État

        [Display(Name = nameof(CdSet_IsActive_Label), ResourceType = typeof(SystemTables))]
        public bool IsActive { get; set; } = true;

        #endregion


        // --------------------------------------------------------------------
        // REGION : Navigation
        // --------------------------------------------------------------------
        /// <summary>URL de retour contrôlé.</summary>
        #region Navigation

        public string? ReturnUrl { get; set; }

        #endregion


        // --------------------------------------------------------------------
        // REGION : Actions DRD
        // --------------------------------------------------------------------
        /// <summary>Boutons standardisés Activer / Modifier / Supprimer.</summary>
        #region Actions

        public bool UseActionButtons { get; set; } = true;

        #endregion
    }
}
