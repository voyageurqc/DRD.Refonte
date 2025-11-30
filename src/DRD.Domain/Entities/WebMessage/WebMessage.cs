// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 WebMessage.cs
// Type de fichier                Entity
// Nature C#                      Class
// Emplacement                    Entities/WebMessage/
// Auteur                         Michel Gariépy
// Créé le                        2025-06-17
//
// Description
//     Représente un message système destiné aux usagers DRD. Chaque message peut
//     être bilingue, obligatoire ou optionnel, et limité ou non à une période donnée.
//
// Fonctionnalité
//     - Contient les titres et contenus bilingues.
//     - Définit le type de message et son caractère obligatoire ou non.
//     - Peut exiger une confirmation de la part de l’usager.
//     - Peut être limité à une période d’affichage (StartDate / EndDate).
//     - Possède des relations vers WebMessageUser (état individuel) et WebMessageLink.
//
// Modifications
//     2025-11-30    Suppression de IsPersistent (gestion via WebMessageUser + période).
//     2025-11-30    Version nettoyée Domain (suppression EF, nomenclature FR/EN).
//     2025-07-14    Ajout du support FR/EN dans le contenu et le titre.
//     2025-06-17    Création initiale.
// ============================================================================

using DRD.Domain.Common;

namespace DRD.Domain.Entities.WebMessage
{
	/// <summary>
	/// Représente un message système bilingue destiné à l'affichage aux utilisateurs.
	/// </summary>
	public class WebMessage : BaseAuditableEntity
	{
		#region Identification
		/// <summary>
		/// Numéro interne du message (clé naturelle).
		/// </summary>
		public int MessageNumber { get; private set; }
		#endregion


		#region Titles
		public string TitleFr { get; private set; } = string.Empty;
		public string TitleEn { get; private set; } = string.Empty;
		#endregion


		#region Contents
		public string ContentFr { get; private set; } = string.Empty;
		public string ContentEn { get; private set; } = string.Empty;
		#endregion


		#region Message Settings
		/// <summary>
		/// Type métier du message (ex.: Maintenance, Update, Alert, Warning, etc.).
		/// </summary>
		public string MessageType { get; private set; } = string.Empty;

		/// <summary>
		/// Indique si l’usager doit obligatoirement agir/valider ce message.
		/// </summary>
		public bool IsMandatory { get; private set; }

		/// <summary>
		/// Indique si l’usager doit confirmer explicitement la lecture du message.
		/// </summary>
		public bool RequiresConfirmation { get; private set; }
		#endregion


		#region Validity Period
		/// <summary>
		/// Date de début d’affichage du message (nullable).
		/// </summary>
		public DateTime? StartDate { get; private set; }

		/// <summary>
		/// Date de fin d’affichage du message (nullable). Null = sans limite de fin.
		/// </summary>
		public DateTime? EndDate { get; private set; }
		#endregion


		#region Relations
		/// <summary>
		/// Liens associés à ce message (ciblage, contexte, etc.).
		/// </summary>
		public ICollection<WebMessageLink> Links { get; private set; } = new List<WebMessageLink>();

		/// <summary>
		/// États individuels du message pour chaque usager.
		/// </summary>
		public ICollection<WebMessageUser> Users { get; private set; } = new List<WebMessageUser>();
		#endregion
	}
}
