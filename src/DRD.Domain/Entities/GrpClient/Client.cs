// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 Client.cs
// Type de fichier                Entity
// Classe                         Client
// Emplacement                    Entities/GrpClient/
// Entités concernées             Client, ClientDetail, Individual
// Créé le                        2025-06-18
//
// Description
//     Représente un client du système DRD. Contient les informations de base,
//     d’identification, d’adresse ainsi que certains paramètres opérationnels.
//     Ce modèle est utilisé comme entité principale pour la gestion des clients.
//
// Fonctionnalité
//     - Définit les propriétés métiers d’un client.
//     - Hérite de BaseAuditableEntity pour fournir le suivi d’audit uniforme.
//     - Sert de modèle central pour la gestion interne des informations client.
//
// Modifications
//     2025-12-09    Ajustements DRD (régions, résumés, en-tête).
//     2025-11-30    Conversion vers Client.cs (nomenclature Domain propre).
//     2025-07-14    Refonte pour hériter de BaseAuditableEntity.
//     2025-06-18    Création initiale.
// ============================================================================

using DRD.Domain.Common;

namespace DRD.Domain.Entities.GrpClient
{
    /// <summary>
    /// Représente un client dans le domaine DRD.
    /// </summary>
    public class Client : BaseAuditableEntity
    {
        #region DRD – Identification
        /// <summary>
        /// Numéro interne du client (hérité du système).
        /// Peut servir de clé naturelle si requis.
        /// </summary>
        public int ClientNumber { get; private set; }
        #endregion

        #region DRD – Information de base
        /// <summary>
        /// Nom principal du client (ligne 1).
        /// </summary>
        public string ClientName1 { get; private set; } = string.Empty;

        /// <summary>
        /// Nom secondaire du client (ligne 2).
        /// </summary>
        public string? ClientName2 { get; private set; }

        /// <summary>
        /// Adresse ligne 1.
        /// </summary>
        public string Address1 { get; private set; } = string.Empty;

        /// <summary>
        /// Adresse ligne 2.
        /// </summary>
        public string? Address2 { get; private set; }

        /// <summary>
        /// Ville où est situé le client.
        /// </summary>
        public string City { get; private set; } = string.Empty;

        /// <summary>
        /// Code pays ISO (ex: CA).
        /// </summary>
        public string CountryCode { get; private set; } = "CA";

        /// <summary>
        /// Code province ISO (ex: NB).
        /// </summary>
        public string ProvinceCode { get; private set; } = "NB";

        /// <summary>
        /// Code postal.
        /// </summary>
        public string PostalCode { get; private set; } = string.Empty;
        #endregion

        #region DRD – Contact
        /// <summary>
        /// Nom du contact principal.
        /// </summary>
        public string ContactName { get; private set; } = string.Empty;

        /// <summary>
        /// Téléphone principal.
        /// </summary>
        public string Phone1 { get; private set; } = string.Empty;

        /// <summary>
        /// Téléphone secondaire.
        /// </summary>
        public string? Phone2 { get; private set; }

        /// <summary>
        /// Courriel du client.
        /// </summary>
        public string Email { get; private set; } = string.Empty;
        #endregion

        #region DRD – Organisation
        /// <summary>
        /// Type d’organisation (hérité du système).
        /// </summary>
        public string? OrganizationType { get; private set; }

        /// <summary>
        /// Numéro d’organisation.
        /// </summary>
        public int? OrganizationNumber { get; private set; }

        /// <summary>
        /// Numéro de l’employé responsable.
        /// </summary>
        public int? EmployeeNumber { get; private set; }

        /// <summary>
        /// Nom de l’employé responsable.
        /// </summary>
        public string? EmployeeName { get; private set; }
        #endregion

        #region DRD – Paramètres opérationnels
        /// <summary>
        /// Ordre de saisie (ancien paramètre interne).
        /// </summary>
        public string EntryOrder { get; private set; } = "A";

        /// <summary>
        /// Code interne MCPA.
        /// </summary>
        public string McpaCode { get; private set; } = "A";

        /// <summary>
        /// Mode de transfert (ex: “F” fichier).
        /// </summary>
        public string TransferMode { get; private set; } = "F";

        /// <summary>
        /// Type de compte bancaire (ex: "CK" checking).
        /// </summary>
        public string AccountType { get; private set; } = "CK";

        /// <summary>
        /// Format du rapport (ex: "W").
        /// </summary>
        public string ReportType { get; private set; } = "W";

        /// <summary>
        /// Adresse du serveur distant.
        /// </summary>
        public string? ServerLocation { get; private set; }

        /// <summary>
        /// Code culture du client (ex: FR, EN-CA).
        /// </summary>
        public string CultureCode { get; private set; } = "FR";

        /// <summary>
        /// Type client DRD (valeur interne).
        /// </summary>
        public int ClientTypeDrd { get; private set; } = 0;
        #endregion

        #region DRD – Relations
        /// <summary>
        /// Liste des détails liés au client.
        /// </summary>
        public ICollection<ClientDetail> Details { get; private set; } = new List<ClientDetail>();
        #endregion
    }
}
