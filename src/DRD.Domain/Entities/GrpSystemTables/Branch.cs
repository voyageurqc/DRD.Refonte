// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 Branch.cs
// Type de fichier                Entity
// Classe                         Branch
// Emplacement                    Entities/GrpSystemTables/
// Entités concernées             Branch, Institution
// Créé le                        2025-07-02
//
// Description
//     Représente une succursale d’une institution financière canadienne. Une
//     institution peut avoir zéro ou plusieurs succursales, y compris une
//     succursale fictive lorsque l’institution opère exclusivement en ligne.
//
// Fonctionnalité
//     - Identifie une succursale via son numéro d’institution et son numéro local.
//     - Contient les informations d’adresse et d’identification.
//     - Fournit un numéro de transit calculé (InstitutionNumber + BranchNumber).
//     - Hérite de BaseAuditableEntity.
//
// Notes métier
//     - Les règles de suppression ou désactivation relèvent de la couche Application.
//     - Une succursale peut être virtuelle pour les institutions sans présence physique.
//
// Modifications
//     2025-12-09    Ajustements DRD (régions, résumés, en-tête).
//     2025-11-30    Version nettoyée Domain (renommage + suppression EF).
//     2025-07-14    Refonte initiale (ancien projet).
//     2025-07-02    Création initiale.
// ============================================================================

using DRD.Domain.Common;

namespace DRD.Domain.Entities.GrpSystemTables
{
    /// <summary>
    /// Représente une succursale d’une institution financière.
    /// </summary>
    public class Branch : BaseAuditableEntity
    {
        #region DRD – Identification
        /// <summary>
        /// Numéro officiel de l'institution financière parent.
        /// </summary>
        public string InstitutionNumber { get; private set; } = string.Empty;

        /// <summary>
        /// Numéro unique de la succursale au sein de l’institution.
        /// </summary>
        public string BranchNumber { get; private set; } = string.Empty;
        #endregion

        #region DRD – Informations
        /// <summary>
        /// Nom ou étiquette locale de la succursale (facultatif).
        /// </summary>
        public string? BranchName { get; private set; }

        /// <summary>
        /// Adresse civique.
        /// </summary>
        public string AddressLine { get; private set; } = string.Empty;

        /// <summary>
        /// Ville.
        /// </summary>
        public string City { get; private set; } = string.Empty;

        /// <summary>
        /// Code de province (ex.: QC, ON, NB).
        /// </summary>
        public string ProvinceCode { get; private set; } = string.Empty;

        /// <summary>
        /// Code postal.
        /// </summary>
        public string PostalCode { get; private set; } = string.Empty;

        /// <summary>
        /// Numéro de transit calculé (institution + succursale).
        /// </summary>
        public string TransitNumber => $"{InstitutionNumber}{BranchNumber}";
        #endregion

        #region DRD – Relations
        /// <summary>
        /// Institution financière parent.
        /// </summary>
        public Institution Institution { get; private set; } = null!;
        #endregion
    }
}
