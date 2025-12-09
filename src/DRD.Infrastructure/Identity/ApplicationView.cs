// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 ApplicationView.cs
// Type de fichier                Entity
// Classe                         ApplicationView
// Emplacement                    Identity/
// Entités concernées             ApplicationView, UserViewAccess
// Créé le                        2025-07-02
//
// Description
//     Représente une vue interne (Controller + Action) pouvant être liée
//     à un type d’accès utilisateur. Sert au système d’autorisation
//     granulaire interne utilisé dans l’administration DRD.
//
// Fonctionnalité
//     - Identifie une action via un code unique (ViewCode).
//     - Fournit la localisation bilingue des descriptions.
//     - Gère la relation entre les vues et les droits d’accès par utilisateur.
//     - Hérite de UserAudit pour assurer un suivi complet.
//
// Modifications
//     2025-12-09    Ajustements DRD (régions, résumés, en-tête).
//     2025-12-02    Setters publics pour compatibilité EF Core (DRDv10).
//     2025-11-30    Nettoyage complet ; déplacement Domain → Infrastructure.
//     2025-07-14    Ajustements initiaux.
// ============================================================================

using DRD.Infrastructure.Common;

namespace DRD.Infrastructure.Identity
{
    /// <summary>
    /// Représente une action contrôlable au niveau des permissions internes DRD.
    /// </summary>
    public class ApplicationView : UserAudit
    {
        #region DRD – Identification
        /// <summary>
        /// Code unique représentant cette vue/action.
        /// (Clé naturelle configurée via EF Core.)
        /// </summary>
        public string ViewCode { get; set; } = string.Empty;
        #endregion


        #region DRD – Mappage MVC
        /// <summary>
        /// Nom du contrôleur associé.
        /// </summary>
        public string Controller { get; set; } = string.Empty;

        /// <summary>
        /// Nom de l’action associée.
        /// </summary>
        public string Action { get; set; } = string.Empty;
        #endregion


        #region DRD – Descriptions
        /// <summary>
        /// Description française affichée dans l’interface d’administration.
        /// </summary>
        public string DescriptionFr { get; set; } = string.Empty;

        /// <summary>
        /// Description anglaise affichée dans l’interface d’administration.
        /// </summary>
        public string? DescriptionEn { get; set; }
        #endregion


        #region DRD – Relations
        /// <summary>
        /// Liste des droits d’accès liés à cette vue.
        /// </summary>
        public ICollection<UserViewAccess> ViewAccesses { get; private set; }
            = new List<UserViewAccess>();
        #endregion
    }
}
