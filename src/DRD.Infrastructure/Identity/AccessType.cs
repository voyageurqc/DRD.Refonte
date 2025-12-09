// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 AccessType.cs
// Type de fichier                Entity
// Classe                         AccessType
// Emplacement                    Identity/
// Entités concernées             AccessType, ApplicationUser
// Créé le                        2025-06-17
//
// Description
//     Représente un type d’accès utilisateur dans le système DRD. Chaque type
//     d’accès peut être assigné à un ou plusieurs utilisateurs. Contient des
//     informations bilingues et les champs d’audit standards Infrastructure.
//
// Fonctionnalité
//     - Définition d’un code d’accès (clé naturelle).
//     - Descriptions bilingues.
//     - Champs d’audit via UserAudit.
//     - Relation 1 → N avec ApplicationUser.
//     - Compatible EF Core et IdentitySeeder.
//
// Modifications
//     2025-12-09    Ajustements DRD (régions, résumés, en-tête).
//     2025-12-02    Ajout du constructeur public pour IdentitySeeder.
//     2025-11-30    Déplacement vers Infrastructure (Clean Architecture).
//     2025-06-17    Création initiale.
// ============================================================================

using DRD.Infrastructure.Common;

namespace DRD.Infrastructure.Identity
{
    /// <summary>
    /// Type d’accès assignable aux utilisateurs DRD.
    /// </summary>
    public class AccessType : UserAudit
    {
        #region DRD – Constructeurs
        /// <summary>
        /// Constructeur public utilisé pour la création d’un type d’accès,
        /// notamment lors du seeding Identity.
        /// </summary>
        public AccessType(
            string accessTypeCode,
            string descriptionFr,
            string? descriptionEn = null)
        {
            AccessTypeCode = accessTypeCode;
            DescriptionFr = descriptionFr;
            DescriptionEn = descriptionEn;
        }

        /// <summary>
        /// Constructeur requis par EF Core.
        /// </summary>
        protected AccessType() { }
        #endregion


        #region DRD – Identification
        /// <summary>
        /// Code unique du type d’accès (clé naturelle).
        /// </summary>
        public string AccessTypeCode { get; private set; } = string.Empty;
        #endregion


        #region DRD – Descriptions
        /// <summary>
        /// Description française du type d’accès.
        /// </summary>
        public string DescriptionFr { get; private set; } = string.Empty;

        /// <summary>
        /// Description anglaise du type d’accès.
        /// </summary>
        public string? DescriptionEn { get; private set; }
        #endregion


        #region DRD – Relations
        /// <summary>
        /// Liste des utilisateurs possédant ce type d’accès.
        /// </summary>
        public ICollection<ApplicationUser> Users { get; private set; }
            = new List<ApplicationUser>();
        #endregion
    }
}
