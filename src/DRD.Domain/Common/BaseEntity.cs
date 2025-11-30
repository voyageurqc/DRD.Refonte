// ============================================================================
// 💻 Projet                 : DRD.Domain
// 📄 Nom du fichier          : BaseEntity.cs
// 📄 Classe du fichier      : Class (Abstract)
// 📍 Emplacement            : ~/Common/
// 🏛️ Entité(s) touchée(s)   : (Toutes)
// 📅 Créé le                : 2025-07-02
//
// 📌 Description :
//     Classe de base pour toutes les entités du système. Fournit un
//     identifiant unique global (GUID).
//
// 🎯 Fonctionnalité :
//     - Assure que chaque nouvelle entité possède un ID unique lors de sa création.
//
// 🛠️ Modifications :
//     - 2025-07-10 : Ajout d'un constructeur pour initialiser l'ID automatiquement.
// ============================================================================

using System;

namespace DRD.Domain.Common
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Le constructeur est appelé à chaque création d'une nouvelle entité.
        /// Il garantit que la propriété Id a toujours une nouvelle valeur unique.
        /// </summary>
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// L'identifiant unique global (GUID) pour l'entité.
        /// </summary>
        public Guid Id { get; set; }
    }
}
