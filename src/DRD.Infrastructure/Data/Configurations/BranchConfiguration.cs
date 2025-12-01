// ============================================================================
//     Projet                         DRD.Infrastructure
//     Nom du fichier                 BranchConfiguration.cs
//     Type de fichier                Classe C#
//     Nature C#                      Configuration EF Core
//     Emplacement                    Data/Configurations
//     Auteur                         Michel Gariépy
//     Créé le                        2025-12-01
//
//     Description
//         Configuration Entity Framework Core pour l'entité Branch. Décrit la
//         clé composite, les contraintes de structure ainsi que la relation
//         parent vers Institution.
//
//     Fonctionnalité
//         - Définir la clé composite (InstitutionNumber + BranchNumber).
//         - Ignorer la propriété Id héritée de BaseAuditableEntity.
//         - Définir les contraintes sur les colonnes (longueur, required).
//         - Configurer la relation vers Institution.
//
//     Modifications
//         2025-12-01    Création initiale conforme au standard DRD .NET 10.
// ============================================================================

using DRD.Domain.Entities.SystemTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations
{
	public class BranchConfiguration : IEntityTypeConfiguration<Branch>
	{
		public void Configure(EntityTypeBuilder<Branch> builder)
		{
			builder.ToTable("Branch");

			// Clé primaire composite
			builder.HasKey(e => new { e.InstitutionNumber, e.BranchNumber });

			// Ignorer Id hérité
			builder.Ignore(e => e.Id);

			// ----------------------------
			// Identification
			// ----------------------------
			builder.Property(e => e.InstitutionNumber)
				   .IsRequired()
				   .HasMaxLength(10);

			builder.Property(e => e.BranchNumber)
				   .IsRequired()
				   .HasMaxLength(10);

			// ----------------------------
			// Informations
			// ----------------------------

			builder.Property(e => e.BranchName)
				   .HasMaxLength(150);

			builder.Property(e => e.AddressLine)
				   .IsRequired()
				   .HasMaxLength(200);

			builder.Property(e => e.City)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(e => e.ProvinceCode)
				   .IsRequired()
				   .HasMaxLength(5);

			builder.Property(e => e.PostalCode)
				   .IsRequired()
				   .HasMaxLength(15);

			// TransitNumber est une propriété calculée → aucun mapping
			builder.Ignore(e => e.TransitNumber);

			// ----------------------------
			// Relation vers Institution
			// ----------------------------
			builder.HasOne(e => e.Institution)
				   .WithMany(i => i.Branches)
				   .HasForeignKey(e => e.InstitutionNumber)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
