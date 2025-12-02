// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 WebMessageUser.cs
// Type de fichier                Entity
// Nature C#                      Class
// Emplacement                    Entities/GrpWebMessage/
// Auteur                         Michel Gariépy
// Créé le                        2025-06-27
//
// Description
//     Représente l’état individuel d’un message WebMessage pour un utilisateur.
//     Contient l’information de lecture, d’action, et le résultat (accepté ou refusé).
//
// Fonctionnalité
//     - Associe un WebMessage à un utilisateur spécifique.
//     - Enregistre les dates de lecture et d’action.
//     - Permet de savoir si un message obligatoire a été confirmé ou refusé.
//     - Sépare complètement la logique utilisateur du système d’authentification.
//
// Modifications
//     2025-11-30    Version nettoyée Domain (suppression EF + Identity).
//     2025-07-14    Refonte initiale (ancien projet).
//     2025-06-27    Création initiale.
// ============================================================================

using DRD.Domain.Common;
using DRD.Domain.Entities.GrpWebMessage;

namespace DRD.Domain.Entities.GrpWebMessage
{
	/// <summary>
	/// État individuel d’un message WebMessage pour un utilisateur.
	/// </summary>
	public class WebMessageUser : BaseAuditableEntity
	{
		#region Identification
		/// <summary>
		/// Identifiant du message.
		/// </summary>
		public int WebMessageId { get; private set; }

		/// <summary>
		/// Identifiant métier de l’usager (pas Identity).
		/// </summary>
		public string UserId { get; private set; } = string.Empty;
		#endregion


		#region Status
		/// <summary>
		/// Date de lecture du message par l'usager.
		/// </summary>
		public DateTime? ReadDate { get; private set; }

		/// <summary>
		/// Date de l'action prise (confirmation ou refus).
		/// </summary>
		public DateTime? ActionDate { get; private set; }

		/// <summary>
		/// null = aucune action,
		/// true = accepté,
		/// false = refusé.
		/// </summary>
		public bool? Accepted { get; private set; }
		#endregion


		#region Relations
		/// <summary>
		/// Le message auquel cet état individuel est rattaché.
		/// </summary>
		public WebMessage WebMessage { get; private set; } = null!;
		#endregion
	}
}
