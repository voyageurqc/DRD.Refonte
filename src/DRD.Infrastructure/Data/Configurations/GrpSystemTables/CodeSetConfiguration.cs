// ============================================================================
// Projet:      DRD.Infrastructure
// Fichier:     CdSetConfiguration.cs
// Type:        Configuration EF Core
// Classe:      Class
// Emplacement: Data/Configurations/GrpSystemTables
// Entité(s):   CdSet
// Créé le:     2025-12-01
//
// Description:
//     Configuration Entity Framework Core pour l'entité CdSet. Assure une
//     correspondance stricte avec la base de données existante, incluant la clé
//     composite, les longueurs exactes et les champs hérités d'audit.
//
// Fonctionnalité:
//     - Définir la clé primaire composite (TypeCode + Code).
//     - Définir les longueurs maximales telles qu'en BD (nvarchar(20/50)).
//     - Mapper l'entité sur la table "CodeSet" (table physique réelle).
//     - Conserver Id (hérité) sans l'utiliser comme clé.
//
// Modifications:
//     2025-12-06	Correction du mapping pour refléter structure BD réelle.
//     2025-12-01	Création initiale conforme standard DRD .NET 10.
// ============================================================================

using DRD.Domain.Entities.GrpSystemTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DRD.Infrastructure.Data.Configurations.GrpSystemTables
{
	/// <summary>
	/// Configuration EF Core strictement alignée sur la table existante CodeSet.
	/// </summary>
	public class CdSetConfiguration : IEntityTypeConfiguration<CdSet>
	{
		#region Configuration
		/// <summary>
		/// Configure le mapping Entity Framework Core pour l'entité CdSet.
		/// </summary>
		public void Configure(EntityTypeBuilder<CdSet> builder)
		{
			#region Table
			builder.ToTable("CodeSet"); // nom réel de la table dans ta BD
			#endregion

			#region Key
			builder.HasKey(e => new { e.TypeCode, e.Code });
			#endregion

			#region Fields
			builder.Property(e => e.TypeCode)
				.IsRequired()
				.HasMaxLength(20)
				.HasColumnType("nvarchar(20)");

			builder.Property(e => e.Code)
				.IsRequired()
				.HasMaxLength(20)
				.HasColumnType("nvarchar(20)");

			builder.Property(e => e.DescriptionFr)
				.IsRequired()
				.HasMaxLength(50)
				.HasColumnType("nvarchar(50)");

			builder.Property(e => e.DescriptionEn)
				.HasMaxLength(50)
				.HasColumnType("nvarchar(50)");

			#endregion

			#region Audit
			builder.Property(e => e.CreationDate).HasColumnType("datetime2");
			builder.Property(e => e.ModificationDate).HasColumnType("datetime2");
			builder.Property(e => e.CreatedBy).HasColumnType("nvarchar(max)");
			builder.Property(e => e.UpdatedBy).HasColumnType("nvarchar(max)");
			builder.Property(e => e.IsActive).HasColumnType("bit");
			#endregion
		}
		#endregion
	}
}
