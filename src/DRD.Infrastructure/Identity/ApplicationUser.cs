// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 ApplicationUser.cs
// Type de fichier                Identity Entity
// Nature C#                      Class
// Emplacement                    Identity/
// Auteur                         Michel Gariépy
// Créé le                        2025-07-02
//
// Description
//     Représente un utilisateur du système DRD. Étend IdentityUser pour
//     inclure des informations personnelles, des préférences de travail,
//     des codes d’accès personnalisés et des champs d’audit.
//
// Fonctionnalité
//     - Compatible avec ASP.NET Core Identity.
//     - Contient les informations personnelles (prénom, nom, adresse, téléphone).
//     - Contient les préférences (imprimantes, secteur).
//     - Contient des codes d’accès internes.
//     - Reproduit les champs d’audit identiques à UserAudit pour cohérence
//       à travers l’Infrastructure.
//
// Modifications
//     2025-11-30    ApplicationUser mis à jour pour respecter UserAudit
//                   tout en tenant compte de l’impossibilité d’héritage multiple.
// ============================================================================

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DRD.Infrastructure.Identity
{
	public class ApplicationUser : IdentityUser
	{
		#region Personal Information

		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;

		public string? AddressLine { get; set; }
		public string? City { get; set; }
		public string? Province { get; set; }
		public string? PostalCode { get; set; }

		#endregion


		#region User Preferences

		public string DefaultPrinter { get; set; } = string.Empty;
		public string LaserPrinter { get; set; } = string.Empty;

		#endregion


		#region Access & Role Customization

		public string SectorCode { get; set; } = string.Empty;
		public string? AccessTypeCode { get; set; }
		public string? MenuCode { get; set; }

		public ICollection<UserViewAccess> ViewAccesses { get; private set; }
			= new List<UserViewAccess>();

		#endregion


		#region Audit Fields (Alignés sur UserAudit)

		/// <summary>Date de création du compte utilisateur (UTC).</summary>
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;

		/// <summary>Identifiant de l’usager ayant créé ce compte.</summary>
		public string? CreatedBy { get; set; }

		/// <summary>Date de la dernière modification du compte (UTC).</summary>
		public DateTime ModificationDate { get; set; } = DateTime.UtcNow;

		/// <summary>Identifiant de l’usager ayant effectué la dernière modification.</summary>
		public string? UpdatedBy { get; set; }

		/// <summary>Indique si le compte est actif.</summary>
		public bool IsActive { get; set; } = true;

		/// <summary>Niveau de sécurité interne du compte.</summary>
		public int SecurityLevel { get; set; } = 1;

		#endregion
	}
}
