// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 UserAudit.cs
// Type de fichier                Infrastructure Entity Base
// Nature C#                      Abstract Class
// Emplacement                    Common/
// Auteur                         Michel Gariépy
// Créé le                        2025-11-30
//
// Description
//     Classe de base utilisée pour ajouter les champs d’audit aux entités
//     Infrastructure (ex.: ApplicationUser). Ce mécanisme permet de suivre
//     automatiquement la création, la modification, ainsi que l’état actif.
//
// Fonctionnalité
//     - Centralise les propriétés d’audit pour éviter la duplication.
//     - Utilisée par les entités système qui nécessitent une traçabilité.
//     - Ne dépend pas du Domain et reste spécifique à l’Infrastructure.
//
// Modifications
//     2025-11-30    Création initiale conforme au standard DRD.
// ============================================================================

namespace DRD.Infrastructure.Common
{
	/// <summary>
	/// Classe de base ajoutant les champs d’audit aux entités Infrastructure.
	/// </summary>
	public abstract class UserAudit
	{
		/// <summary>
		/// Date de création de l’entité (UTC).
		/// </summary>
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Identifiant de l’usager ayant créé l’entité.
		/// </summary>
		public string? CreatedBy { get; set; }

		/// <summary>
		/// Date de dernière modification de l’entité (UTC).
		/// </summary>
		public DateTime ModificationDate { get; set; } = DateTime.UtcNow;

		/// <summary>
		/// Identifiant de l’usager ayant effectué la dernière modification.
		/// </summary>
		public string? UpdatedBy { get; set; }

		/// <summary>
		/// Indique si l’entité est active (par défaut : true).
		/// </summary>
		public bool IsActive { get; set; } = true;

	}
}
