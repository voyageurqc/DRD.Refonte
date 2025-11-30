// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 AccessType.cs
// Type de fichier                Infrastructure Entity
// Nature C#                      Class
// Emplacement                    Identity/
// Auteur                         Michel Gariépy
// Créé le                        2025-06-17
//
// Description
//     Représente un type d’accès utilisateur dans le système DRD. Chaque type
//     d’accès peut être assigné à un ou plusieurs utilisateurs.
//
// Fonctionnalité
//     - Définition d’un code d’accès (clé naturelle).
//     - Description bilingue.
//     - Champs d’audit via UserAudit.
//     - Relation avec ApplicationUser.
//
// Modifications
//     2025-11-30    Version conforme Clean Architecture (déplacée hors Domain).
//     2025-07-14    Ajustements initiaux.
// ============================================================================

using DRD.Infrastructure.Common;

namespace DRD.Infrastructure.Identity
{
	/// <summary>
	/// Type d’accès assignable aux utilisateurs.
	/// </summary>
	public class AccessType : UserAudit
	{
		#region Identification

		/// <summary>
		/// Code unique du type d’accès (clé naturelle).
		/// </summary>
		public string AccessTypeCode { get; private set; } = string.Empty;

		#endregion


		#region Descriptions

		/// <summary>
		/// Description française du type d’accès.
		/// </summary>
		public string DescriptionFr { get; private set; } = string.Empty;

		/// <summary>
		/// Description anglaise du type d’accès.
		/// </summary>
		public string? DescriptionEn { get; private set; }

		#endregion


		#region Relations

		/// <summary>
		/// Utilisateurs auxquels ce type d’accès est assigné.
		/// </summary>
		public ICollection<ApplicationUser> Users { get; private set; }
			= new List<ApplicationUser>();

		#endregion
	}
}
