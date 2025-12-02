// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 ClientDetail.cs
// Type de fichier                Entity
// Nature C#                      Class
// Emplacement                    Entities/GrpClient/
// Auteur                         Michel Gariépy
// Créé le                        2025-06-17
//
// Description
//     Représente un détail DRD associé à un client maître. Cette entité contient
//     les informations transactionnelles, opérationnelles et historiques liées
//     au traitement DRD d’un client.
//
// Fonctionnalité
//     - Définit les propriétés métiers associées à un détail DRD.
//     - Hérite de BaseAuditableEntity pour fournir le suivi d’audit uniforme.
//     - Sert de ligne de détail rattachée à l’entité Client.
//
// Modifications
//     2025-11-30    Conversion vers ClientDetail (nomenclature Domain propre).
//     2025-07-14    Refonte pour hériter de BaseAuditableEntity.
//     2025-06-17    Création initiale.
// ============================================================================

using DRD.Domain.Common;
using DRD.Domain.Entities.GrpClient;

namespace DRD.Domain.Entities.GrpClient
{
	/// <summary>
	/// Représente un détail DRD lié à un client maître.
	/// </summary>
	public class ClientDetail : BaseAuditableEntity
	{
		#region Identification
		/// <summary>
		/// Identifiant du client maître (clé composite).
		/// </summary>
		public int ClientNumber { get; private set; }

		/// <summary>
		/// Numéro DRD interne (clé composite).
		/// </summary>
		public int DrdNumber { get; private set; }
		#endregion


		#region Parent Relation
		/// <summary>
		/// Client parent auquel appartient ce détail.
		/// </summary>
		public Client Client { get; private set; } = null!;
		#endregion


		#region Transaction Information
		public string TransactionType { get; private set; } = string.Empty;
		public int TransactionCode { get; private set; }
		public string FrequencyCode { get; private set; } = string.Empty;
		public int DelayCode { get; private set; }
		public long TransitNumber { get; private set; }
		public int TransitId { get; private set; }
		public int Transit { get; private set; }
		public long FolioNumber { get; private set; }
		public int CheckDigit { get; private set; }
		public int DrdFileNumber { get; private set; }
		public int IndividualNumber { get; private set; }
		#endregion


		#region Dates
		public DateTime? LastTransactionDate { get; private set; }
		public DateTime? IntegrationDate { get; private set; }
		public DateTime? CompletedDate { get; private set; }
		public DateTime? LastInvoiceDate { get; private set; }
		public DateTime? NextInvoiceDate { get; private set; }
		#endregion


		#region Contact
		public string ContactName { get; private set; } = string.Empty;
		public long Phone1 { get; private set; }
		public long Phone2 { get; private set; }
		#endregion


		#region Operation Flags
		public string AutonomousFlag { get; private set; } = string.Empty;
		public string CurrencyCode { get; private set; } = string.Empty;
		public string FileFormat { get; private set; } = string.Empty;
		public int CompanyLimitAmount { get; private set; }
		public int IndividualLimitAmount { get; private set; }
		public int NextFileNumberToValidate { get; private set; }
		public string DepartureReason { get; private set; } = string.Empty;
		public long TransitFeeNumber { get; private set; }
		public long FolioFeeNumber { get; private set; }
		public string InvoiceCalculationMode { get; private set; } = string.Empty;
		public string InvoiceFrequencyCode { get; private set; } = string.Empty;
		public string GLAccount { get; private set; } = string.Empty;
		public int InvoiceNumber { get; private set; }
		public string Description { get; private set; } = string.Empty;
		public string InternalUseOnly { get; private set; } = string.Empty;
		public string TransmissionInProgress { get; private set; } = string.Empty;
		public string DepositWithdrawalCode { get; private set; } = string.Empty;
		public int SecondaryDrdNumber { get; private set; }
		public string MasterClientFlag { get; private set; } = string.Empty;
		public int SignatureCount { get; private set; }
		public string AuthorizationLevel { get; private set; } = string.Empty;
		public string PreDepositDelay { get; private set; } = string.Empty;
		public string PayingClient { get; private set; } = string.Empty;
		#endregion
	}
}
