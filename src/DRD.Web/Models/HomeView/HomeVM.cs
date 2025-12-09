// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 HomeVM.cs
// Type de fichier                ViewModel
// Classe                         HomeVM
// Emplacement                    Models/Home
// Entités concernées             ApplicationUser, WebMessage
// Créé le                        2025-06-26
//
// Description
//     ViewModel pour la page d'accueil DRD (Home/Index). Contient les données
//     visibles de l'utilisateur connecté ainsi que les éléments d'accueil
//     optionnels tels que messages système.
//
// Fonctionnalité
//     - Exposer le nom complet de l’utilisateur connecté.
//     - Prévoir l’extension future pour messages dynamiques.
//     - S'intégrer proprement avec le contrôleur Home.
//
// Modifications
//     2025-12-09    Mise à niveau complète au standard DRD v10
//                   (en-tête, XML docs, régions).
// ============================================================================

using System.Collections.Generic;

namespace DRD.Web.Models.Home
{
    /// <summary>
    /// ViewModel utilisé par la vue Home/Index.
    /// </summary>
    public class HomeVM
    {
        #region Informations utilisateur
        /// <summary>
        /// Nom complet de l’utilisateur connecté (FirstName + LastName).
        /// </summary>
        public string UserFullName { get; set; } = string.Empty;
        #endregion

        #region Messages d'accueil (optionnel DRD)
        /// <summary>
        /// Liste de messages actifs pouvant être affichés sur l’accueil.
        /// Utilisé plus tard dans l’évolution du module WebMessage.
        /// </summary>
        public IEnumerable<string> ActiveMessages { get; set; } = new List<string>();
        #endregion
    }
}
