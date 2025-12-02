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
//     - Descriptions bilingues.
//     - Champs d’audit via UserAudit.
//     - Relation 1 → N avec ApplicationUser.
//     - Utilisable dans le seeding via un constructeur public.
//
// Modifications
//     2025-11-30    Version conforme Clean Architecture (déplacée hors Domain).
//     2025-12-02    Option B : Ajout d’un constructeur public avec paramètres,
//                   setters privés → conformité EF Core + IdentitySeeder.
// ============================================================================

using DRD.Infrastructure.Common;

namespace DRD.Infrastructure.Identity
{
	/// <summary>
	/// Type d’accès assignable aux utilisateurs.
	/// </summary>
	public class AccessType : UserAudit
	{
		#region Constructeur
		/// <summary>
		/// Constructeur utilisé pour créer un AccessType (ex. : seeding).
		/// </summary>
		public AccessType(
			string accessTypeCode,
			string descriptionFr,
			string? descriptionEn = null)
		{
			AccessTypeCode = accessTypeCode;
			DescriptionFr = descriptionFr;
			DescriptionEn = descriptionEn;
		}

		// Constructeur protégé EF Core
		protected AccessType() { }
		#endregion


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
		/// Liste des utilisateurs associés à ce type d’accès.
		/// </summary>
		public ICollection<ApplicationUser> Users { get; private set; }
			= new List<ApplicationUser>();

		#endregion
	}
}
