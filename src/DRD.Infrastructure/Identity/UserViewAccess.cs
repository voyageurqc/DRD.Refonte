// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 UserViewAccess.cs
// Type de fichier                Entity
// Classe                         UserViewAccess
// Emplacement                    Identity/
// Entités concernées             UserViewAccess, ApplicationUser, ApplicationView
// Créé le                        2025-06-17
//
// Description
//     Représente un droit d’accès spécifique entre un utilisateur et une vue
//     (Controller + Action). Sert de table de liaison permettant un contrôle
//     d’accès granulaire au sein de l’application DRD.
//
// Fonctionnalité
//     - Relation plusieurs-à-plusieurs entre ApplicationUser et ApplicationView.
//     - Possède une clé composite (UserId + ViewCode).
//     - Stocke un code de privilège (READ, WRITE, ADMIN, etc.).
//     - Hérite de UserAudit pour assurer une traçabilité complète.
//
// Modifications
//     2025-12-09    Ajustements DRD (régions, résumés, en-tête).
//     2025-12-02    Setters publics pour compatibilité EF Core.
//     2025-11-30    Version DRD propre (audit UserAudit, nettoyage).
//     2025-07-14    Ajustements initiaux.
// ============================================================================

using DRD.Infrastructure.Common;

namespace DRD.Infrastructure.Identity
{
    /// <summary>
    /// Liaison représentant un droit d’accès d’un utilisateur à une vue.
    /// </summary>
    public class UserViewAccess : UserAudit
    {
        #region DRD – Identification
        /// <summary>
        /// Identifiant de l’utilisateur (clé composite partie 1).
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Code de la vue (clé composite partie 2).
        /// </summary>
        public string ViewCode { get; set; } = string.Empty;
        #endregion


        #region DRD – Privilège
        /// <summary>
        /// Code du privilège accordé (ex.: READ, WRITE, ADMIN).
        /// </summary>
        public string PrivilegeCode { get; set; } = string.Empty;
        #endregion


        #region DRD – Relations
        /// <summary>
        /// Utilisateur auquel ce droit d’accès appartient.
        /// </summary>
        public ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Vue associée à ce droit d’accès.
        /// </summary>
        public ApplicationView ApplicationView { get; set; } = null!;
        #endregion
    }
}
