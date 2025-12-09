// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 ApplicationUser.cs
// Type de fichier                Entity (Identity)
// Classe                         ApplicationUser
// Emplacement                    Identity/
// Entités concernées             ApplicationUser, AccessType, UserViewAccess
// Créé le                        2025-07-02
//
// Description
//     Représente un utilisateur du système DRD. Étend IdentityUser pour
//     inclure des informations personnelles, des préférences de travail,
//     des codes d’accès internes ainsi que des champs d’audit cohérents
//     avec les conventions DRD.
//
// Fonctionnalité
//     - Compatible avec ASP.NET Core Identity (hérite de IdentityUser).
//     - Contient les informations personnelles (prénom, nom, adresse, téléphone).
//     - Contient les préférences de travail (imprimantes, secteur).
//     - Supporte les codes et types d’accès internes (AccessType).
//     - Réplique les champs d’audit afin d’assurer une cohérence globale.
//
// Modifications
//     2025-12-09    Ajout en-tête DRD v10, résumés et régions (aucune modif métier).
//     2025-12-02    Ajustement AccessType (setter public) pour compatibilité EF.
//     2025-11-30    Alignement ApplicationUser ↔ UserAudit (pas d’héritage multiple).
// ============================================================================

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DRD.Infrastructure.Identity
{
    /// <summary>
    /// Utilisateur DRD basé sur ASP.NET Identity avec champs personnalisés.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        #region DRD – Informations personnelles
        /// <summary>
        /// Prénom de l’utilisateur.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Nom de famille de l’utilisateur.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        #endregion


        #region DRD – Préférences utilisateur
        /// <summary>
        /// Imprimante par défaut sélectionnée par l’utilisateur.
        /// </summary>
        public string DefaultPrinter { get; set; } = string.Empty;

        /// <summary>
        /// Imprimante laser secondaire.
        /// </summary>
        public string LaserPrinter { get; set; } = string.Empty;
        #endregion


        #region DRD – Accès et rôles internes
        /// <summary>
        /// Code du secteur interne (ex.: FIN, ADM, SYS).
        /// </summary>
        public string SectorCode { get; set; } = string.Empty;

        /// <summary>
        /// Code du type d’accès interne (clé étrangère).
        /// </summary>
        public string? AccessTypeCode { get; set; }

        /// <summary>
        /// Code de menu personnalisé (optionnel).
        /// </summary>
        public string? MenuCode { get; set; }

        /// <summary>
        /// Droits d’accès personnalisés par vue.
        /// </summary>
        public ICollection<UserViewAccess> ViewAccesses { get; private set; }
            = new List<UserViewAccess>();

        /// <summary>
        /// Navigation EF Core vers le type d’accès.
        /// Setter public requis pour EF.
        /// </summary>
        public AccessType? AccessType { get; set; }
        #endregion


        #region DRD – Champs d’audit
        /// <summary>
        /// Date de création du compte.
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Utilisateur ayant créé ce compte.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Date de dernière modification.
        /// </summary>
        public DateTime ModificationDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Utilisateur ayant effectué la dernière modification.
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Indique si le compte est actif.
        /// </summary>
        public bool IsActive { get; set; } = true;
        #endregion
    }
}
