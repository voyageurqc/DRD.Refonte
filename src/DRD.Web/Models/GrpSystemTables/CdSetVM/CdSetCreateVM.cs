// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetCreateVM.cs
// Type de fichier                ViewModel
// Classe                         CdSetCreateVM
// Emplacement                    Models/GrpSystemTables/CdSetVM
// Entités concernées             CdSet (création)
// Créé le                        2025-12-07
//
// Description
//     ViewModel utilisé pour la création d’un nouvel enregistrement CdSet.
//     Permet de sélectionner une famille existante (TypeCode) ou d’en créer
//     une nouvelle. Tous les labels sont entièrement localisés.
//
// Fonctionnalité
//     - Sélection d’une famille existante ou création d’une nouvelle.
//     - Validation uniformisée DRD v10 via ressources strongly-typed.
//     - Conversion vers l’entité Domain via mutateurs.
//     - Navigation DRD + intégration actions standardisées.
//
// Modifications
//     2025-12-09    Conformité DRD v10 : ressources strongly-typed,
//                   ajout EntityName/UseActionButtons, ReturnUrl public.
//     2025-12-07    Mise à jour ToEntity() pour SetFamily/SetDescriptions.
//     2025-12-07    Version initiale DRD v10.
// ============================================================================

using DRD.Domain.Entities.GrpSystemTables;
using DRD.Resources.SystemTables;
using DRD.Resources.Common;
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
        /// <summary>
        /// Gestion des familles existantes ou création d'une nouvelle.
        /// </summary>
        #region Famille

        /// <summary>Liste des familles existantes.</summary>
        public IEnumerable<string> AvailableTypeCodes { get; set; } = new List<string>();

        /// <summary>Famille sélectionnée dans la liste.</summary>
        [Display(Name = nameof(SystemTables.CdSet_Family_Label), ResourceType = typeof(SystemTables))]
        public string? SelectedTypeCode { get; set; }

        /// <summary>Nouvelle famille si l’utilisateur choisit l’option dédiée.</summary>
        [StringLength(20)]
        [Display(Name = nameof(SystemTables.CdSet_Family_New_Label), ResourceType = typeof(SystemTables))]
        public string? NewTypeCode { get; set; }

        /// <summary>Choix final (nouvelle ou existante).</summary>
        public string TypeCodeFinal =>
            SelectedTypeCode == SystemTables.CdSet_Family_NewOption
                ? NewTypeCode ?? string.Empty
                : SelectedTypeCode ?? string.Empty;

        #endregion


        // --------------------------------------------------------------------
        // REGION : Champs du Code
        // --------------------------------------------------------------------
        /// <summary>
        /// Champs principaux du CdSet (Code + Descriptions).
        /// </summary>
        #region Code

        [Required(ErrorMessageResourceName = nameof(Common.Validation_Required), ErrorMessageResourceType = typeof(Common))]
        [StringLength(20)]
        [Display(Name = nameof(SystemTables.CdSet_Code_Label), ResourceType = typeof(SystemTables))]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessageResourceName = nameof(Common.Validation_Required), ErrorMessageResourceType = typeof(Common))]
        [StringLength(50)]
        [Display(Name = nameof(SystemTables.CdSet_DescriptionFr_Label), ResourceType = typeof(SystemTables))]
        public string DescriptionFr { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = nameof(SystemTables.CdSet_DescriptionEn_Label), ResourceType = typeof(SystemTables))]
        public string? DescriptionEn { get; set; }

        /// <summary>URL de retour DRD.</summary>
        public string? ReturnUrl { get; set; }

        #endregion


        // --------------------------------------------------------------------
        // REGION : Conversion vers entité Domain
        // --------------------------------------------------------------------
        /// <summary>
        /// Conversion vers entité Domain via mutateurs DRD v10.
        /// </summary>
        #region Mapping

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


        // --------------------------------------------------------------------
        // REGION : Actions DRD
        // --------------------------------------------------------------------
        /// <summary>
        /// Métadonnées UI pour intégration automatique des actions DRD.
        /// </summary>
        #region Actions

        public string EntityName { get; set; } = "CdSet";
        public bool UseActionButtons { get; set; } = true;

        #endregion
    }
}
