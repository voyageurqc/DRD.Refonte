// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 UserViewAccess.cs
// Type de fichier                Infrastructure Entity
// Nature C#                      Class
// Emplacement                    Identity/
// Auteur                         Michel Gariépy
// Créé le                        2025-06-17
//
// Description
//     Représente un droit d’accès pour un utilisateur donné vers une vue/action
//     spécifique de l’application. Sert d’entité de liaison entre ApplicationUser
//     et ApplicationView.
//
// Fonctionnalité
//     - Définit une relation plusieurs-à-plusieurs entre utilisateurs et vues.
//     - Possède une clé composite (UserId + ViewCode).
//     - Contient un code de privilège (ex.: READ, WRITE, ADMIN).
//     - Hérite de UserAudit pour assurer la traçabilité.
//
// Modifications
//     2025-11-30    Version propre DRD (suppression EF, audit UserAudit).
//     2025-07-14    Ajustements initiaux.
//     2025-12-02    Setters publics pour compatibilité EF Core (DRDv10).
// ============================================================================

using DRD.Infrastructure.Common;

namespace DRD.Infrastructure.Identity
{
	/// <summary>
	/// Liaison représentant un droit d’accès d’un utilisateur à une vue.
	/// </summary>
	public class UserViewAccess : UserAudit
	{
		#region Identification

		/// <summary>
		/// Identifiant de l’utilisateur (clé composite part 1).
		/// </summary>
		public string UserId { get; set; } = string.Empty;

		/// <summary>
		/// Code de la vue (clé composite part 2).
		/// </summary>
		public string ViewCode { get; set; } = string.Empty;

		#endregion


		#region Privilege

		/// <summary>
		/// Code du privilège accordé (ex.: READ, WRITE, ADMIN).
		/// </summary>
		public string PrivilegeCode { get; set; } = string.Empty;

		#endregion


		#region Relations

		/// <summary>
		/// Utilisateur auquel ce droit d’accès appartient.
		/// </summary>
		public ApplicationUser User { get; set; } = null!;

		/// <summary>
		/// Vue à laquelle l’utilisateur a accès.
		/// </summary>
		public ApplicationView ApplicationView { get; set; } = null!;

		#endregion
	}
}
