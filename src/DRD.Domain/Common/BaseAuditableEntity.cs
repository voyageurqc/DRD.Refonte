// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 BaseAuditableEntity.cs
// Type de fichier                Classe C#
// Classe                         BaseAuditableEntity
// Emplacement                    Common
// Entités concernées             Toutes entités auditables
// Créé le                        2025-07-14
//
// Description
//     Classe de base abstraite pour toutes les entités nécessitant un audit
//     (dates de création, modification, utilisateur, statut actif). Permet une
//     gestion centralisée et homogène de l’audit dans tout le domaine.
//
// Fonctionnalité
//     - Hérite de BaseEntity pour l'identité.
//     - Implémente IAuditableEntity.
//     - Fournit les méthodes d’audit SetCreationAudit et SetModificationAudit.
//     - Applique les règles DRD : DateTime.UtcNow, IsActive=true.
//
// Modifications
//     2025-12-07    Ajout des méthodes SetCreationAudit et SetModificationAudit
//                   selon la norme DRD v10.
//     2025-11-18    Ajout procédé Maj champs Metadata.
//     2025-07-14    Standardisation initiale du fichier.
// ============================================================================

namespace DRD.Domain.Common
{
	/// <summary>
	/// Classe de base pour les entités auditables.
	/// Gère automatiquement les dates et utilisateurs de création/modification.
	/// </summary>
	public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
	{
		#region Propriétés d’audit

		public DateTime CreationDate { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime ModificationDate { get; set; }
		public string? UpdatedBy { get; set; }
		public bool IsActive { get; set; } = true;

		#endregion

		#region Méthodes d’audit DRD v10

		/// <summary>
		/// Initialise les champs d’audit lors de la création d’une entité.
		/// </summary>
		/// <param name="userName">Nom de l’utilisateur courant (ou "Anonymous").</param>
		public void SetCreationAudit(string? userName)
		{
			var user = string.IsNullOrWhiteSpace(userName) ? "Anonymous" : userName;

			CreationDate = DateTime.UtcNow;
			CreatedBy = user;

			ModificationDate = DateTime.UtcNow;
			UpdatedBy = user;

			IsActive = true;
		}

		/// <summary>
		/// Met à jour les champs d’audit lors d’une modification d’entité.
		/// </summary>
		/// <param name="userName">Nom de l’utilisateur courant (ou "Anonymous").</param>
		public void SetModificationAudit(string? userName)
		{
			var user = string.IsNullOrWhiteSpace(userName) ? "Anonymous" : userName;

			ModificationDate = DateTime.UtcNow;
			UpdatedBy = user;
		}

		#endregion
	}
}
