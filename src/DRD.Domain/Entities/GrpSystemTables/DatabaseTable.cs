// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 DatabaseTable.cs
// Type de fichier                Entity
// Nature C#                      Class
// Emplacement                    Entities/GrpSystemTables/
// Auteur                         Michel Gariépy
// Créé le                        2025-07-02
//
// Description
//     Représente une table physique de la base de données pour fins de
//     surveillance système. Chaque entrée correspond à une table réelle,
//     accompagnée de son nombre de lignes et des informations d’audit.
//
// Fonctionnalité
//     - Stocke le nom de la table (TableName).
//     - Stocke le nombre actuel de lignes (RowCount).
//     - Hérite de BaseAuditableEntity pour créer automatiquement :
//           • CreationDate (UTC)
//           • CreatedBy
//           • ModificationDate (UTC)
//           • UpdatedBy
//           • IsActive
//     - Utilisé uniquement par les administrateurs système.
//     - Recréé ou rafraîchi automatiquement par un service périodique.
//
// Notes
//     - Entité strictement informationnelle, non modifiée par les usagers.
//     - Un seul enregistrement par table physique réelle.
//     - Toujours afficher ModificationDate et UpdatedBy dans l’interface admin.
//
// Modifications
//     2025-11-30    Version nettoyée Domain (suppression EF, setters privés).
//     2025-07-14    Refonte initiale (ancien projet).
//     2025-07-02    Création initiale.
// ============================================================================

using DRD.Domain.Common;
using DRD.Domain.Entities.GrpSystemTables;

namespace DRD.Domain.Entities.GrpSystemTables
{
	/// <summary>
	/// Métadonnées représentant une table physique de la base de données.
	/// Utilisée pour le monitoring interne et l'administration système.
	/// </summary>
	public class DatabaseTable : BaseAuditableEntity
	{
		#region Identification

		/// <summary>
		/// Nom de la table physique dans la base de données.
		/// </summary>
		public string TableName { get; private set; } = string.Empty;

		#endregion


		#region Metrics

		/// <summary>
		/// Nombre actuel de lignes dans la table.
		/// Cette valeur est rafraîchie périodiquement par un service interne.
		/// </summary>
		public long RowCount { get; private set; }

		#endregion
	}
}
