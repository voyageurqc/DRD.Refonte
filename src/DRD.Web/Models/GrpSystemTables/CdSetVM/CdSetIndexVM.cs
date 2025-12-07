// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetIndexVM.cs
// Type de fichier                ViewModel (VM)
// Classe                         CdSetIndexVM
// Emplacement                    Models/GrpSystemTables/CdSetVM
// Entités concernées             CdSet (projection UI)
// Créé le                        2025-12-07
//
// Description
//     ViewModel principal pour la vue Index du module CdSet. Contient les
//     informations nécessaires à l’affichage de la liste, à l’utilisation de
//     DataTables, aux filtres disponibles ainsi qu’aux actions standardisées
//     via la vue partielle _EntityActionButtons.
//
// Fonctionnalité
//     - Transporter la liste de CdSetRowVM pour DataTables.
//     - Gérer les filtres (TypeCode, recherche).
//     - Conserver les informations de navigation (CurrentUrl, ReferrerUrl).
//     - Supporter les actions standardisées (Detail/Edit/Delete/Deactivate).
//
// Modifications
//     2025-12-07    Ajout EntityName + UseActionButtons (standard DRD v10).
//     2025-12-07    Version initiale DRD v10 (DisplayOrder retiré).
// ============================================================================

using System.Collections.Generic;

namespace DRD.Web.Models.GrpSystemTables.CdSetVM
{
    /// <summary>
    /// Modèle de vue pour l’écran Index des CdSet.
    /// </summary>
    public class CdSetIndexVM
    {
        // --------------------------------------------------------------------
        // REGION : Liste principale
        // --------------------------------------------------------------------
        #region Liste principale
        /// <summary>
        /// Liste des éléments CdSet à afficher (DataTables).
        /// </summary>
        public IEnumerable<CdSetRowVM> CdSets { get; set; } = new List<CdSetRowVM>();
        #endregion


        // --------------------------------------------------------------------
        // REGION : Filtres
        // --------------------------------------------------------------------
        #region Filtres

        /// <summary>
        /// Liste disponible des familles (TypeCodes) pour filtrage.
        /// </summary>
        public IEnumerable<string> AvailableTypeCodes { get; set; } = new List<string>();

        /// <summary>
        /// Famille sélectionnée pour filtrage.
        /// </summary>
        public string? SelectedTypeCode { get; set; }

        /// <summary>
        /// Requête de recherche (optionnelle).
        /// </summary>
        public string? SearchQuery { get; set; }

        /// <summary>
        /// Indique si le filtre Famille doit être affiché.
        /// </summary>
        public bool ShowTypeCodeFilter { get; set; } = true;

        #endregion


        // --------------------------------------------------------------------
        // REGION : Navigation DRD
        // --------------------------------------------------------------------
        #region Navigation DRD

        /// <summary>
        /// URL de la page courante (pour conserver l'état DataTables).
        /// </summary>
        public string? CurrentUrl { get; set; }

        /// <summary>
        /// URL précédente afin d’offrir un retour contrôlé.
        /// </summary>
        public string? ReferrerUrl { get; set; }

        #endregion


        // --------------------------------------------------------------------
        // REGION : Métadonnées pour Actions
        // --------------------------------------------------------------------
        #region Actions

        /// <summary>
        /// Nom localisé de l’entité (utilisé par EntityActionButtonsVM).
        /// </summary>
        public string EntityName { get; set; } = "CdSet";

        /// <summary>
        /// Indique si les act
