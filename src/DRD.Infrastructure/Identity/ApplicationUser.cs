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
//     2025-12-02    Ajustement AccessType (setter public) pour compatibilité EF.
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

		/// <summary>
		/// Navigation vers AccessType liée à AccessTypeCode.
		/// EF nécessite un setter public pour créer le lien.
		/// </summary>
		public AccessType? AccessType { get; set; }

		#endregion


		#region Audit Fields (Alignés sur UserAudit)

		public DateTime CreationDate { get; set; } = DateTime.UtcNow;

		public string? CreatedBy { get; set; }

		public DateTime ModificationDate { get; set; } = DateTime.UtcNow;

		public string? UpdatedBy { get; set; }

		public bool IsActive { get; set; } = true;

		#endregion
	}
}
