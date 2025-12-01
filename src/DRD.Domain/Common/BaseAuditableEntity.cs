// ============================================================================
// Projet:      DRD.Domain
// Fichier:     BaseAuditableEntity.cs
// Type:        Entity
// Classe:      Class
// Emplacement: Common/
// Entité(s):   (Base pour toutes les entités auditables)
// Créé le:     2025-07-14
//
// Description:
//     Classe de base abstraite pour toutes les entités qui nécessitent
//     un suivi d'audit (dates de création/modification, etc.).
//
// Fonctionnalité:
//     - Hérite de BaseEntity pour obtenir une clé primaire (si applicable).
//     - Implémente l'interface IAuditableEntity pour garantir les champs d'audit.
//
// Modifications:
//     2025-07-14: Standardisation du fichier.
//     2025-11-18: Ajout procédé Maj champs Metada
// ============================================================================

namespace DRD.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    public DateTime CreationDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime ModificationDate { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; } = true;
}
