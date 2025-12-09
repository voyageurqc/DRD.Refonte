// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 UserAudit.cs
// Type de fichier                Abstract Class
// Classe                         UserAudit
// Emplacement                    Common/
// Entités concernées             ApplicationUser, AccessType, UserViewAccess
// Créé le                        2025-11-30
//
// Description
//     Classe de base utilisée pour ajouter les champs d’audit aux entités
//     Infrastructure (ex. : ApplicationUser, AccessType). Permet une gestion
//     uniforme de la création, modification et de l’état actif des entités.
//
// Fonctionnalité
//     - Centralise les champs d’audit pour éviter la duplication.
//     - Fournit un modèle cohérent entre les entités système.
//     - Ne dépend pas du Domain et reste propre à l’Infrastructure.
//
// Modifications
//     2025-12-09    Ajustements DRD (régions + résumés + en-tête).
//     2025-11-30    Création initiale conforme au standard DRD.
// ============================================================================

namespace DRD.Infrastructure.Common
{
    /// <summary>
    /// Classe de base ajoutant les champs d’audit aux entités Infrastructure.
    /// </summary>
    public abstract class UserAudit
    {
        #region DRD – Champs d’audit
        /// <summary>
        /// Date de création de l’entité (UTC).
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Identifiant de l’usager ayant créé l’entité.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Date de dernière modification de l’entité (UTC).
        /// </summary>
        public DateTime ModificationDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Identifiant de l’usager ayant effectué la dernière modification.
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Indique si l’entité est active (par défaut : true).
        /// </summary>
        public bool IsActive { get; set; } = true;
        #endregion
    }
}
