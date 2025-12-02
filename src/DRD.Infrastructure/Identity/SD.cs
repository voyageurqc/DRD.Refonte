// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 SD.cs
// Type de fichier                Static Definitions
// Nature C#                      Static Class
// Emplacement                    Identity/
// Auteur                         Michel Gariépy
// Créé le                        2025-12-02
//
// Description
//     Définitions globales utilisées pour le système Identity DRD.
//     Centralise les rôles, les codes d’accès et l’usager administrateur,
//     afin d’éviter toute duplication ou incohérence.
//
// Fonctionnalité
//     - Définir tous les rôles Identity officiels.
//     - Définir les codes AccessType internes (AccessType table).
//     - Définir les informations de l’usager administrateur système.
//
// Modifications
//     2025-12-02    Version finale unifiée avec IdentitySeeder.
// ============================================================================

namespace DRD.Infrastructure.Identity
{
	/// <summary>
	/// Contient toutes les définitions statiques relatives à la sécurité DRD.
	/// </summary>
	public static class SD
	{
		// ---------------------------------------------------------
		// RÔLES IDENTITY DRD (officiels)
		// ---------------------------------------------------------
		public const string Role_AdminSystem = "AdminSystem";
		public const string Role_Admin = "Admin";
		public const string Role_User = "User";

		// ---------------------------------------------------------
		// TYPE D’ACCÈS (table AccessType)
		// ---------------------------------------------------------
		public const string AccessType_AdminSystem = "ADMIN_SYS";

		// ---------------------------------------------------------
		// COMPTE ADMINISTRATEUR SYSTÈME
		// ---------------------------------------------------------
		public const string Admin_Email = "michel.gariepy@uni.ca";
		public const string Admin_Password = "Admin@123";
		public const string Admin_FirstName = "Michel";
		public const string Admin_LastName = "Gariépy";
	}
}
