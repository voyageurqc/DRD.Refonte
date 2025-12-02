// ============================================================================
//     Projet                         DRD.Infrastructure
//     Nom du fichier                 DatabaseTableConfiguration.cs
//     Type de fichier                Classe C#
//     Nature C#                      Configuration EF Core
//     Emplacement                    Data/Configurations/GrpSystemTables
//     Auteur                         Michel Gariépy
//     Créé le                        2025-12-01
//
//     Description
//         Configuration Entity Framework Core pour l'entité DatabaseTable.
//         Cette entité représente un registre interne servant à suivre le
//         nombre d'enregistrements dans chaque table de la base.
//
//     Fonctionnalité
//         - Définir la clé primaire basée sur TableName.
//         - Ignorer la propriété Id héritée de BaseAuditableEntity.
//         - Définir les contraintes de structure : longueur, required.
//
//     Modifications
//         2025-12-01    Création initiale conforme au standard DRD .NET 10.
// ============================================================================

using DRD.Domain.Entities.SystemTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpSystemTables
{
	/// <summary>
	/// Configuration EF Core pour l'entité <see cref="DatabaseTable"/>.
	/// </summary>
	public class DatabaseTableConfiguration : IEntityTypeConfiguration<DatabaseTable>
	{
		#region Configuration

		/// <summary>
		/// Configure le mapping Entity Framework Core pour l'entité DatabaseTable.
		/// </summary>
		/// <param name="builder">Builder utilisé pour définir les règles de mapping.</param>
		public void Configure(EntityTypeBuilder<DatabaseTable> builder)
		{
			// Nom de la table physique dans la BD.
			builder.ToTable("DatabaseTable");

			// La clé primaire est le nom de la table.
			builder.HasKey(e => e.TableName);

			// L'Id hérité de BaseAuditableEntity n'est pas utilisé pour cette entité.
			builder.Ignore(e => e.Id);

			// Propriétés
			builder.Property(e => e.TableName)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(e => e.RowCount)
				   .IsRequired(); // nombre d'enregistrements, entier obligatoire
		}

		#endregion
	}
}
