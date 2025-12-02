// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 WebMessageLink.cs
// Type de fichier                Entity
// Nature C#                      Class
// Emplacement                    Entities/GrpWebMessage/
// Auteur                         Michel Gariépy
// Créé le                        2025-06-17
//
// Description
//     Représente un lien hypertexte associé à un message système WebMessage. 
//     Chaque lien peut fournir une ressource externe, une documentation ou 
//     une action complémentaire pour l’usager.
//
// Fonctionnalité
//     - Stocke un titre et une URL.
//     - Est associé à un WebMessage parent.
//     - Hérite de BaseAuditableEntity (audit complet).
//
// Modifications
//     2025-11-30    Version nettoyée Domain (suppression EF, setters protégés).
//     2025-07-14    Refonte initiale (ancien projet).
//     2025-06-17    Création initiale.
// ============================================================================

using DRD.Domain.Common;
using DRD.Domain.Entities.GrpWebMessage;

namespace DRD.Domain.Entities.GrpWebMessage
{
	/// <summary>
	/// Représente un lien hypertexte associé à un WebMessage.
	/// </summary>
	public class WebMessageLink : BaseAuditableEntity
	{
		#region Identification
		/// <summary>
		/// Identifiant du message parent auquel ce lien est rattaché.
		/// </summary>
		public int WebMessageId { get; private set; }
		#endregion


		#region Link Data
		/// <summary>
		/// Titre du lien affiché à l’usager.
		/// </summary>
		public string Title { get; private set; } = string.Empty;

		/// <summary>
		/// URL complète vers la ressource externe ou la documentation.
		/// </summary>
		public string Url { get; private set; } = string.Empty;
		#endregion


		#region Relations
		/// <summary>
		/// Message parent auquel ce lien appartient.
		/// </summary>
		public WebMessage WebMessage { get; private set; } = null!;
		#endregion
	}
}
