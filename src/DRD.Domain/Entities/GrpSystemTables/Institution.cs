// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 Institution.cs
// Type de fichier                Entity
// Classe                         Institution
// Emplacement                    Entities/GrpSystemTables/
// Entités concernées             Institution, Branch
// Créé le                        2025-07-02
//
// Description
//     Représente une institution financière (banque, caisse, fiducie). Les données
//     proviennent généralement des sources gouvernementales canadiennes (Banque du
//     Canada, Payments Canada, etc.). Une institution peut exister sans succursale
//     (banques 100% en ligne).
//
// Fonctionnalité
//     - Identifie une institution par son numéro officiel.
//     - Stocke le nom officiel de l'institution.
//     - Peut contenir zéro ou plusieurs succursales (Branch).
//     - Hérite de BaseAuditableEntity pour le suivi d’audit.
//
// Notes métier
//     - Une institution peut ne pas avoir de succursale (banque exclusivement en ligne).
//     - Les règles métier (ex.: interdiction de supprimer une institution comportant des
//       succursales actives) relèvent de la couche Application.
//     - La création automatique d’une succursale fictive est gérée dans Application.
//
// Modifications
//     2025-12-09    Ajustements DRD (régions, résumés, en-tête).
//     2025-11-30    Version nettoyée Domain, renommage FinancialInstitution → Institution.
//     2025-07-14    Refonte initiale (ancien projet).
//     2025-07-02    Création initiale.
// ============================================================================

using DRD.Domain.Common;

namespace DRD.Domain.Entities.GrpSystemTables
{
    /// <summary>
    /// Représente une institution financière canadienne. Peut être une banque,
    /// une caisse ou une institution en ligne sans succursale.
    /// </summary>
    public class Institution : BaseAuditableEntity
    {
        #region DRD – Identification
        /// <summary>
        /// Numéro officiel unique d'une institution financière.
        /// </summary>
        public string InstitutionNumber { get; private set; } = string.Empty;

        /// <summary>
        /// Nom officiel de l’institution, tel que publié par les autorités gouvernementales.
        /// </summary>
        public string Name { get; private set; } = string.Empty;
        #endregion

        #region DRD – Relations
        /// <summary>
        /// Succursales associées à cette institution (peut être vide).
        /// </summary>
        public ICollection<Branch> Branches { get; private set; } = new List<Branch>();
        #endregion
    }
}
