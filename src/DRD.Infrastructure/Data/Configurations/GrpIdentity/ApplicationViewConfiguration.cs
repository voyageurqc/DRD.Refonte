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
// ============================================================================

using DRD.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpIdentity
{
	/// <summary>
	/// Configuration EF Core pour l'entité ApplicationView.
	/// </summary>
	public class ApplicationViewConfiguration : IEntityTypeConfiguration<ApplicationView>
	{
		#region DRD – Configuration
		/// <summary>
		/// Configure le mapping EF Core pour l'entité ApplicationView.
		/// </summary>
		public void Configure(EntityTypeBuilder<ApplicationView> builder)
		{
			#region DRD – Table
			/// <summary>Définition du nom de la table physique.</summary>
			builder.ToTable("ApplicationView");
			#endregion

			#region DRD – Clé primaire
			/// <summary>
			/// ApplicationView utilise ViewCode comme clé naturelle.
			/// </summary>
			builder.HasKey(e => e.ViewCode);

			#endregion

			#region DRD – Colonnes principales
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
			#endregion

			#region DRD – Relations
			/// <summary>
			/// Un ApplicationView possède plusieurs UserViewAccess.
			/// </summary>
			builder.HasMany(e => e.ViewAccesses)
				   .WithOne(v => v.ApplicationView)
				   .HasForeignKey(v => v.ViewCode)
				   .OnDelete(DeleteBehavior.Cascade);
			#endregion
		}
		#endregion
	}
}
