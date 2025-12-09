// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 BaseEntity.cs
// Type de fichier                Class (Abstract)
// Classe                         BaseEntity
// Emplacement                    Common/
// Entités concernées             Toutes
// Créé le                        2025-07-02
//
// Description
//     Classe de base pour toutes les entités du système. Fournit un
//     identifiant unique global (GUID).
//
// Fonctionnalité
//     - Assure que chaque nouvelle entité possède un ID unique lors de sa création.
//
// Modifications
//     2025-12-09    Ajustements DRD (en-tête, résumés, régions).
//     2025-07-10    Ajout d'un constructeur pour initialiser l'ID automatiquement.
// ============================================================================

using System;

namespace DRD.Domain.Common
{
    /// <summary>
    /// Classe de base abstraite pour toutes les entités du système.
    /// Fournit un identifiant unique global.
    /// </summary>
    public abstract class BaseEntity
    {
        #region DRD – Constructeurs
        /// <summary>
        /// Constructeur appelé lors de la création de chaque nouvelle entité.
        /// Initialise automatiquement l'identifiant unique global (GUID).
        /// </summary>
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region DRD – Propriétés
        /// <summary>
        /// Identifiant unique global pour l'entité.
        /// </summary>
        public Guid Id { get; set; }
        #endregion
    }
}
