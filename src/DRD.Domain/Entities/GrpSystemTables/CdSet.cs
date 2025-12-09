// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 CdSet.cs
// Type de fichier                Entity
// Classe                         CdSet
// Emplacement                    Entities/GrpSystemTables
// Entités concernées             CdSet
// Créé le                        2025-06-17
//
// Description
//     Entité représentant une entrée paramétrique générique (Code Set).
//     Chaque enregistrement appartient à une famille (TypeCode) et possède un
//     code unique dans cette famille, accompagné de descriptions bilingues.
//     Utilisé pour toutes les listes déroulantes et sélecteurs du projet.
//
// Fonctionnalité
//     - Stockage centralisé de valeurs paramétriques.
//     - Support natif bilingue (FR/EN).
//     - Encapsulation via mutateurs Domain.
//     - Héritage de BaseAuditableEntity (audit complet + IsActive).
//
// Modifications
//     2025-12-09    Ajustements DRD (en-tête, régions, résumés).
//     2025-12-07    Ajout des mutateurs Domain (SetFamily, SetCodeValue,
//                   SetDescriptions, ModifyFields) selon DRD v10.
//     2025-11-30    Nettoyage Domain + suppression EF details.
//     2025-07-14    Refonte initiale (ancien projet).
//     2025-06-17    Création initiale.
// ============================================================================

using DRD.Domain.Common;

namespace DRD.Domain.Entities.GrpSystemTables
{
    /// <summary>
    /// Représente une entrée générique de type Code Set utilisée dans tout le système.
    /// Une famille (TypeCode) regroupe plusieurs codes (Code), chacun avec des descriptions FR/EN.
    /// </summary>
    public class CdSet : BaseAuditableEntity
    {
        #region DRD – Identification
        /// <summary>
        /// Nom de la famille du code (ex.: Province, Country, PaymentType).
        /// </summary>
        public string TypeCode { get; private set; } = string.Empty;

        /// <summary>
        /// Valeur du code dans la famille.
        /// </summary>
        public string Code { get; private set; } = string.Empty;
        #endregion

        #region DRD – Descriptions
        /// <summary>
        /// Description française du code.
        /// </summary>
        public string DescriptionFr { get; private set; } = string.Empty;

        /// <summary>
        /// Description anglaise du code.
        /// </summary>
        public string? DescriptionEn { get; private set; }
        #endregion

        #region DRD – Mutateurs Domain
        /// <summary>
        /// Définit la famille (TypeCode) lors de la création de l'entité.
        /// </summary>
        /// <param name="typeCode">Nom de la famille.</param>
        public void SetFamily(string typeCode)
        {
            TypeCode = typeCode;
        }

        /// <summary>
        /// Définit la valeur du code dans la famille.
        /// </summary>
        /// <param name="code">Code unique dans la famille.</param>
        public void SetCodeValue(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Définit les descriptions FR et EN.
        /// </summary>
        /// <param name="fr">Description en français.</param>
        /// <param name="en">Description en anglais.</param>
        public void SetDescriptions(string fr, string? en)
        {
            DescriptionFr = fr;
            DescriptionEn = en;
        }

        /// <summary>
        /// Met à jour les champs éditables (Description FR, EN, IsActive).
        /// Ne modifie jamais les clés (TypeCode, Code).
        /// </summary>
        public void ModifyFields(string descriptionFr, string? descriptionEn, bool isActive)
        {
            DescriptionFr = descriptionFr;
            DescriptionEn = descriptionEn;
            IsActive = isActive;

            // Audit mis à jour automatiquement via EF Interceptors
            ModificationDate = DateTime.UtcNow;
        }
        #endregion
    }
}
