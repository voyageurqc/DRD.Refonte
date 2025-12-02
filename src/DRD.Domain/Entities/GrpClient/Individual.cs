// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 Individual.cs
// Type de fichier                Entity
// Nature C#                      Class
// Emplacement                    Entities/GrpClient
// Auteur                         Michel Gariépy
// Créé le                        2025-11-05
//
// Description
//     Représente un individu associé à un client et à un dossier DRD. Cette entité
//     contient des informations personnelles, de communication, bancaires et
//     opérationnelles relatives au traitement DRD. Elle est gérée exclusivement par
//     les administrateurs ou gestionnaires client et ne représente pas un utilisateur
//     du système.
//
// Fonctionnalité
//     - Définit les propriétés métiers d’un individu lié à un ClientDetail.
//     - Hérite de BaseAuditableEntity pour fournir le suivi d’audit uniforme.
//     - Ne possède aucun lien avec ASP.NET Identity.
//     - Sert de composante interne du sous-domaine Client (client maître → détails → individu).
//
// Modifications
//     2025-11-30    Suppression complète des références Identity (AuthenticationUserId).
//     2025-11-30    Conversion vers Individual.cs (nomenclature Domain propre).
//     2025-11-08    Ajustement final (ancien projet).
//     2025-11-05    Création initiale.
// ============================================================================

using DRD.Domain.Common;
using DRD.Domain.Entities.GrpClient;

namespace DRD.Domain.Entities.GrpClient
{
	/// <summary>
	/// Représente un individu lié à un ClientDetail dans le domaine DRD.
	/// </summary>
	public class Individual : BaseAuditableEntity
	{
		#region Identification
		/// <summary>
		/// Numéro du client maître (clé composite).
		/// </summary>
		public int ClientNumber { get; private set; }

		/// <summary>
		/// Numéro DRD (clé composite).
		/// </summary>
		public int DrdNumber { get; private set; }

		/// <summary>
		/// Numéro interne de l’individu (clé composite).
		/// </summary>
		public int IndividualNumber { get; private set; }
		#endregion


		#region Personal Information
		public string IndividualName { get; private set; } = string.Empty;
		public string? IndividualName1 { get; private set; }
		public string? IndividualName2 { get; private set; }
		#endregion


		#region Address
		public string? Address1 { get; private set; }
		public string? Address2 { get; private set; }
		public string? City { get; private set; }
		public string? CountryCode { get; private set; }
		public string? ProvinceCode { get; private set; }
		public string? PostalCode { get; private set; }
		#endregion


		#region Communication
		public string? Email { get; private set; }
		public string? Phone1 { get; private set; }
		public string? Phone2 { get; private set; }
		public string? LanguageCode { get; private set; }
		#endregion


		#region Banking Information
		public long? TransitNumber { get; private set; }
		public int? TransitId { get; private set; }
		public int? Transit { get; private set; }

		public string? FolioNumber { get; private set; }
		public int? CheckDigit { get; private set; }
		public int? TransactionCode { get; private set; }
		#endregion


		#region Transfer Settings
		public int? RepeatCount { get; private set; }
		public string? FrequencyCode { get; private set; }

		public int? TransferAmount { get; private set; }
		public int? PreviousTransferAmount { get; private set; }
		public int? MaxAmount { get; private set; }
		public int? TotalAmount { get; private set; }

		public int? EndDate { get; private set; }
		public long? NextTransactionDate { get; private set; }
		public int? LastTransactionDate { get; private set; }

		public string? Reference { get; private set; }
		public int? TransferDay { get; private set; }
		#endregion


		#region Automatic Data
		public long? AutoTransitNumber { get; private set; }
		public int? AutoTransitId { get; private set; }
		public int? AutoTransit { get; private set; }

		public string? AutoFolioNumber { get; private set; }
		public int? AutoCheckDigit { get; private set; }

		public int? StartDate { get; private set; }
		public string? KeyName { get; private set; }

		public int? TransferLimitAmount { get; private set; }
		#endregion


		#region Relations
		/// <summary>
		/// Détail DRD auquel cet individu appartient.
		/// </summary>
		public ClientDetail ClientDetail { get; private set; } = null!;
		#endregion
	}
}
