// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 UserViewAccessConfiguration.cs
// Type de fichier                Configuration EF Core
// Nature C#                      Class
// Emplacement                    Data/Configurations/GrpIdentity
// Auteur                         Michel Gariépy
// Créé le                        2025-12-01
//
// Description
//     Configuration EF Core pour l'entité UserViewAccess. Cette entité définit
//     un droit d'accès (privilège) d'un utilisateur vers une vue spécifique.
//     La configuration précise la clé composite, les contraintes et les relations.
//
// Fonctionnalité
//     - Définir la clé composite (UserId + ViewCode).
//     - Ignorer la propriété Id héritée de UserAudit.
//     - Configurer les contraintes essentielles (required, max length).
//     - Configurer les relations vers ApplicationUser et ApplicationView.
//
// Modifications
//     2025-12-01    Création initiale conforme au standard DRD (Option B).
// ============================================================================

using DRD.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpIdentity
{
	/// <summary>
	/// Configuration EF Core pour l'entité UserViewAccess.
	/// </summary>
	public class UserViewAccessConfiguration : IEntityTypeConfiguration<UserViewAccess>
	{
		#region DRD – Configuration
		/// <summary>
		/// Configure le mapping EF Core pour l'entité UserViewAccess.
		/// </summary>
		public void Configure(EntityTypeBuilder<UserViewAccess> builder)
		{
			#region DRD – Table
			/// <summary>Définition du nom de la table physique.</summary>
			builder.ToTable("UserViewAccess");
			#endregion

			#region DRD – Clé primaire composite
			/// <summary>
			/// Un droit d'accès est identifié par :
			/// - l'utilisateur (UserId)
			/// - la vue (ViewCode)
			/// </summary>
			builder.HasKey(e => new { e.UserId, e.ViewCode });

			/// <summary>
			/// Ignore la propriété Id héritée de UserAudit.
			/// Elle n'est pas utilisée dans la clé composite.
			/// </summary>
			builder.Ignore(e => e.Id);
			#endregion

			#region DRD – Colonnes principales
			builder.Property(e => e.UserId)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.Property(e => e.ViewCode)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(e => e.PrivilegeCode)
				   .IsRequired()
				   .HasMaxLength(20);
			#endregion

			#region DRD – Relations
			/// <summary>Relation 1 → N vers ApplicationUser.</summary>
			builder.HasOne(e => e.User)
				   .WithMany(u => u.ViewAccesses)
				   .HasForeignKey(e => e.UserId)
				   .OnDelete(DeleteBehavior.Cascade);

			/// <summary>Relation 1 → N vers ApplicationView.</summary>
			builder.HasOne(e => e.ApplicationView)
				   .WithMany(v => v.ViewAccesses)
				   .HasForeignKey(e => e.ViewCode)
				   .OnDelete(DeleteBehavior.Cascade);
			#endregion
		}
		#endregion
	}
}
