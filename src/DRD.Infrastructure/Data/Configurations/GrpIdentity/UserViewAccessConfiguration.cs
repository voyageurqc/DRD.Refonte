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
//     - Conserver le champ Id hérité (audit), sans l'utiliser en clé.
//     - Configurer les contraintes essentielles (required, max length).
//     - Configurer les relations vers ApplicationUser et ApplicationView.
//
// Modifications
//     2025-12-01    Création initiale conforme au standard DRD (Option B).
//     2025-12-02    Suppression de Ignore(Id) — nécessaire pour UserAudit.
// ============================================================================

using DRD.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpIdentity
{
	public class UserViewAccessConfiguration : IEntityTypeConfiguration<UserViewAccess>
	{
		public void Configure(EntityTypeBuilder<UserViewAccess> builder)
		{
			// Table
			builder.ToTable("UserViewAccess");

			// Clé composite
			builder.HasKey(e => new { e.UserId, e.ViewCode });

			// Colonnes
			builder.Property(e => e.UserId)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.Property(e => e.ViewCode)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(e => e.PrivilegeCode)
				   .IsRequired()
				   .HasMaxLength(20);

			// Relations
			builder.HasOne(e => e.User)
				   .WithMany(u => u.ViewAccesses)
				   .HasForeignKey(e => e.UserId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(e => e.ApplicationView)
				   .WithMany(v => v.ViewAccesses)
				   .HasForeignKey(e => e.ViewCode)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
