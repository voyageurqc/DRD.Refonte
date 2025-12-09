// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 IAuditableEntity.cs
// Type de fichier                Interface
// Classe                         IAuditableEntity
// Emplacement                    Common/
// Entités concernées             Toutes entités auditables
// Créé le                        2025-07-14
//
// Description
//     Interface définissant les champs d’audit obligatoires pour toutes
//     les entités nécessitant une gestion centralisée et uniforme.
//
// Fonctionnalité
//     - Définit les propriétés d’audit standardisées (création, modification).
//     - Utilisée par BaseAuditableEntity.
//     - Applique les conventions DRD v10.
//
// Modifications
//     2025-12-09    Ajout de l’en-tête DRD, régions et résumés.
//     2025-07-14    Création initiale.
// ============================================================================

namespace DRD.Domain.Common
{
    /// <summary>
    /// Interface définissant les propriétés d’audit utilisées par
    /// toutes les entités auditables dans le domaine DRD.
    /// </summary>
    public interface IAuditableEntity
    {
        #region DRD – Propriétés
        /// <summary>
        /// Date de création de l'entité (UTC).
        /// </summary>
        DateTime CreationDate { get; set; }

        /// <summary>
        /// Nom de l'utilisateur ayant créé l'entité.
        /// </summary>
        string? CreatedBy { get; set; }

        /// <summary>
        /// Date de la dernière modification (UTC).
        /// </summary>
        DateTime ModificationDate { get; set; }

        /// <summary>
        /// Nom de l'utilisateur ayant modifié l'entité.
        /// </summary>
        string? UpdatedBy { get; set; }

        /// <summary>
        /// Indique si l'entité est active.
        /// </summary>
        bool IsActive { get; set; }
        #endregion
    }
}
