// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 CdSetConfiguration.cs
// Type de fichier                Configuration EF Core
// Classe                         CdSetConfiguration
// Emplacement                    Data/Configurations/GrpSystemTables
// Entités concernées             CdSet
// Créé le                        2025-12-01
//
// Description
//     Configuration Entity Framework Core pour l'entité CdSet. Assure une
//     correspondance stricte avec la base de données existante, incluant la
//     clé composite, les longueurs exactes et les champs d’audit.
//
// Fonctionnalité
//     - Définit la clé primaire composite (TypeCode + Code).
//     - Définit les longueurs maximales nvarchar conformément à la BD réelle.
//     - Mappe l'entité sur la table CodeSet.
//     - Conserve l’Id (hérité) sans l’utiliser comme clé.
//
// Modifications
//     2025-12-09    Ajustements DRD (régions, résumés, en-tête).
//     2025-12-06    Correction du mapping pour refléter structure BD réelle.
//     2025-12-01    Création initiale conforme standard DRD .NET 10.
// ============================================================================

using DRD.Domain.Entities.GrpSystemTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpSystemTables
{
    /// <summary>
    /// Configuration EF Core strictement alignée sur la table CodeSet.
    /// </summary>
    public class CdSetConfiguration : IEntityTypeConfiguration<CdSet>
    {
        #region DRD – Configuration principale
        /// <summary>
        /// Configure le mapping Entity Framework Core pour l'entité CdSet.
        /// </summary>
        public void Configure(EntityTypeBuilder<CdSet> builder)
        {
            #region DRD – Table
            /// <summary>Nom de la table physique réelle.</summary>
            builder.ToTable("CodeSet");
            #endregion

            #region DRD – Clé
            /// <summary>Définition de la clé composite (TypeCode + Code).</summary>
            builder.HasKey(e => new { e.TypeCode, e.Code });
            #endregion

            #region DRD – Champs
            /// <summary>Configuration des propriétés métiers principales.</summary>

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

            #region DRD – Audit
            /// <summary>Configuration explicite des champs d’audit.</summary>

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
