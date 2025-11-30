// ============================================================================
// Projet                         DRD.Domain
// Nom du fichier                 CodeSet.cs
// Type de fichier                Entity
// Nature C#                      Class
// Emplacement                    Entities/SystemTables/
// Auteur                         Michel Gariépy
// Créé le                        2025-06-17
//
// Description
//     Représente une valeur d’un ensemble de codes génériques (Code Set).
//     Utilisé pour alimenter des listes déroulantes ou des choix uniformes,
//     en mode bilingue (FR/EN).
//
// Fonctionnalité
//     - Stocke des valeurs paramétriques utilisables partout dans le système.
//     - Définit un type de code, une valeur, et des descriptions bilingues.
//     - Hérite de BaseAuditableEntity (Id, audit, actif/inactif).
//
// Modifications
//     2025-11-30    Version nettoyée Domain (bilingue FR/EN + suppression EF).
//     2025-07-14    Refonte initiale (ancien projet).
//     2025-06-17    Création initiale.
// ============================================================================

using DRD.Domain.Common;

namespace DRD.Domain.Entities.SystemTables
{
	/// <summary>
	/// Représente une entrée générique de type Code Set utilisée dans l’ensemble du système.
	/// </summary>
	public class CodeSet : BaseAuditableEntity
	{
		#region Identification
		/// <summary>
		/// Catégorie ou type du code (ex.: PaymentType, Status, Country).
		/// </summary>
		public string TypeCode { get; private set; } = string.Empty;

		/// <summary>
		/// Valeur unique du code dans sa catégorie.
		/// </summary>
		public string Code { get; private set; } = string.Empty;
		#endregion


		#region Descriptions
		/// <summary>
		/// Description française.
		/// </summary>
		public string DescriptionFr { get; private set; } = string.Empty;

		/// <summary>
		/// Description anglaise.
		/// </summary>
		public string? DescriptionEn { get; private set; }
		#endregion


		#region Display Order (Optional)
		/// <summary>
		/// Ordre d’affichage facultatif dans les listes (0 = non spécifié).
		/// </summary>
		public int DisplayOrder { get; private set; }
		#endregion
	}
}
