// ============================================================================
//     Projet                         DRD.Infrastructure
//     Nom du fichier                 CodeSetConfiguration.cs
//     Type de fichier                Classe C#
//     Nature C#                      Configuration EF Core
//     Emplacement                    Data/Configurations/GrpSystemTables
//     Auteur                         Michel Gariépy
//     Créé le                        2025-12-01
//
//     Description
//         Configuration Entity Framework Core pour l'entité CodeSet. Définit la
//         clé composite, les contraintes de longueur et la table physique.
//
//     Fonctionnalité
//         - Définir la clé primaire composite (TypeCode + Code).
//         - Définir les longueurs maximales des champs.
//         - Spécifier le nom de la table associée en base de données.
//         - Laisser la colonne Id héritée disponible mais non clé.
//
//     Modifications
//         2025-12-01    Création initiale conforme au standard DRD .NET 10.
// ============================================================================

using DRD.Domain.Entities.GrpSystemTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpSystemTables
{
	/// <summary>
	/// Configuration EF Core pour l'entité <see cref="CodeSet"/>.
	/// </summary>
	public class CodeSetConfiguration : IEntityTypeConfiguration<CodeSet>
	{
		#region Configuration

		/// <summary>
		/// Configure le mapping Entity Framework Core pour l'entité CodeSet.
		/// </summary>
		/// <param name="builder">
		/// Constructeur de type permettant de définir les règles de mapping.
		/// </param>
		public void Configure(EntityTypeBuilder<CodeSet> builder)
		{
			// Nom de la table physique.
			// Si tu veux conserver le nom historique "CdSet", remplace "CodeSet" par "CdSet".
			builder.ToTable("CodeSet");

			// Clé primaire composite (TypeCode + Code).
			builder.HasKey(e => new { e.TypeCode, e.Code });

			// Propriétés obligatoires + longueurs maximales.
			builder.Property(e => e.TypeCode)
				   .IsRequired()
				   .HasMaxLength(20);

			builder.Property(e => e.Code)
				   .IsRequired()
				   .HasMaxLength(20);

			builder.Property(e => e.DescriptionFr)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.Property(e => e.DescriptionEn)
				   .HasMaxLength(50);

		}

		#endregion
	}
}
