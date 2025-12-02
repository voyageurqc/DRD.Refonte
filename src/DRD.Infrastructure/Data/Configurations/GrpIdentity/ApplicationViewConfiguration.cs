// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 ApplicationViewConfiguration.cs
// Type de fichier                Configuration EF Core
// Nature C#                      Class
// Emplacement                    Data/Configurations/GrpIdentity
// Auteur                         Michel Gariépy
// Créé le                        2025-12-01
//
// Description
//     Configuration EF Core pour l'entité ApplicationView. Cette configuration
//     définit la clé primaire, les contraintes sur les colonnes et la relation
//     vers UserViewAccess.
//
// Fonctionnalité
//     - Définir la clé primaire ViewCode (clé naturelle).
//     - Ignorer la propriété Id héritée de UserAudit.
//     - Configurer les contraintes essentielles (required, max length).
//     - Configurer la relation 1 → N vers UserViewAccess.
//
// Modifications
//     2025-12-01    Création initiale conforme au standard DRD (Option B).
//     2025-12-02    Ajout de Ignore(Id) pour compatibilité EF Core (DRDv10).
// ============================================================================

using DRD.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpIdentity
{
	public class ApplicationViewConfiguration : IEntityTypeConfiguration<ApplicationView>
	{
		public void Configure(EntityTypeBuilder<ApplicationView> builder)
		{
			// Table
			builder.ToTable("ApplicationView");

			// Clé primaire
			builder.HasKey(e => e.ViewCode);

			// Colonnes principales
			builder.Property(e => e.ViewCode)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(e => e.Controller)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(e => e.Action)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(e => e.DescriptionFr)
				   .IsRequired()
				   .HasMaxLength(200);

			builder.Property(e => e.DescriptionEn)
				   .HasMaxLength(200);

			// Relations
			builder.HasMany(e => e.ViewAccesses)
				   .WithOne(v => v.ApplicationView)
				   .HasForeignKey(v => v.ViewCode)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
