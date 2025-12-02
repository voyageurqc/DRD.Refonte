// ============================================================================
//     Projet                         DRD.Infrastructure
//     Nom du fichier                 InstitutionConfiguration.cs
//     Type de fichier                Classe C#
//     Nature C#                      Configuration EF Core
//     Emplacement                    Data/Configurations/GrpSystemTables
//     Auteur                         Michel Gariépy
//     Créé le                        2025-12-01
//
//     Description
//         Configuration Entity Framework Core pour l'entité Institution.
//         Définit la clé primaire et les contraintes de structure.
//
//     Fonctionnalité
//         - Définir la clé primaire InstitutionNumber.
//         - Ignorer la propriété Id héritée.
//         - Définir les contraintes de longueur.
//         - Configurer la relation 1 → N avec Branch.
//
//     Modifications
//         2025-12-01    Création initiale conforme au standard DRD .NET 10.
// ============================================================================

using DRD.Domain.Entities.GrpSystemTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpSystemTables
{
	public class InstitutionConfiguration : IEntityTypeConfiguration<Institution>
	{
		public void Configure(EntityTypeBuilder<Institution> builder)
		{
			builder.ToTable("Institution");

			// Clé principale
			builder.HasKey(e => e.InstitutionNumber);

			// Ignorer Id hérité
			builder.Ignore(e => e.Id);

			// Colonnes
			builder.Property(e => e.InstitutionNumber)
				   .IsRequired()
				   .HasMaxLength(10);

			builder.Property(e => e.Name)
				   .IsRequired()
				   .HasMaxLength(150);

			// Relation Institution (1) → Branches (N)
			builder.HasMany(e => e.Branches)
				   .WithOne(b => b.Institution)
				   .HasForeignKey(b => b.InstitutionNumber)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
