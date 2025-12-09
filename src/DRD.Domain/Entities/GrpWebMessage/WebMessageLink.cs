// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 WebMessageLink.cs
// Type de fichier                Entity
// Classe                         WebMessageLink
// Emplacement                    Entities/GrpWebMessage/
// Entités concernées             WebMessageLink, WebMessage
// Créé le                        2025-06-17
//
// Description
//     Représente un lien hypertexte associé à un message système WebMessage.
//     Chaque lien peut fournir une ressource externe, une documentation ou une
//     action complémentaire pour l’usager.
//
// Fonctionnalité
//     - Stocke un titre et une URL.
//     - Est associé à un WebMessage parent.
//     - Hérite de BaseAuditableEntity (audit complet).
//
// Modifications
//     2025-12-09    Ajustements DRD (régions FR, résumés, en-tête).
//     2025-11-30    Version nettoyée Domain (suppression EF, setters protégés).
//     2025-07-14    Refonte initiale (ancien projet).
//     2025-06-17    Création initiale.
// ============================================================================

using DRD.Domain.Common;

namespace DRD.Domain.Entities.GrpWebMessage
{
    /// <summary>
    /// Représente un lien hypertexte associé à un message système WebMessage.
    /// </summary>
    public class WebMessageLink : BaseAuditableEntity
    {
        #region DRD – Identification
        /// <summary>
        /// Identifiant du message parent auquel ce lien est rattaché.
        /// </summary>
        public int WebMessageId { get; private set; }
        #endregion

        #region DRD – Données du lien
        /// <summary>
        /// Titre du lien affiché à l’usager.
        /// </summary>
        public string Title { get; private set; } = string.Empty;

        /// <summary>
        /// URL complète vers la ressource externe ou un document lié.
        /// </summary>
        public string Url { get; private set; } = string.Empty;
        #endregion

        #region DRD – Relations
        /// <summary>
        /// Message parent auquel ce lien appartient.
        /// </summary>
        public WebMessage WebMessage { get; private set; } = null!;
        #endregion
    }
}
