// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 BranchConfiguration.cs
// Type de fichier                Configuration EF Core
// Classe                         BranchConfiguration
// Emplacement                    Data/Configurations/GrpSystemTables
// Entités concernées             Branch, Institution
// Créé le                        2025-12-01
//
// Description
//     Configuration Entity Framework Core pour l'entité Branch. Décrit la clé
//     composite, les contraintes de structure ainsi que la relation vers
//     Institution.
//
// Fonctionnalité
//     - Définit la clé composite (InstitutionNumber + BranchNumber).
//     - Ignore Id (hérité de BaseAuditableEntity).
//     - Applique les contraintes de longueur et de structure.
//     - Configure la relation Institution (1) → Branch (N).
//
// Modifications
//     2025-12-09    Ajustements DRD (régions, résumés, en-tête).
//     2025-12-01    Création initiale conforme au standard DRD .NET 10.
// ============================================================================

using DRD.Domain.Entities.GrpSystemTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpSystemTables
{
    /// <summary>
    /// Configuration EF Core strictement alignée sur la table Branch.
    /// </summary>
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        #region DRD – Configuration
        /// <summary>
        /// Configure le mapping EF Core pour l'entité Branch.
        /// </summary>
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            #region DRD – Table
            /// <summary>Nom de la table physique réelle.</summary>
            builder.ToTable("Branch");
            #endregion

            #region DRD – Clé
            /// <summary>Définition de la clé composite.</summary>
            builder.HasKey(e => new { e.InstitutionNumber, e.BranchNumber });

            /// <summary>Ignore l’Id héritée (non utilisée ici).</summary>
            builder.Ignore(e => e.Id);

            /// <summary>Ignore TransitNumber (propriété calculée).</summary>
            builder.Ignore(e => e.TransitNumber);
            #endregion

            #region DRD – Champs
            /// <summary>Configuration des propriétés métiers de Branch.</summary>

            builder.Property(e => e.InstitutionNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.BranchNumber)
                .IsRequired()
                .HasMaxLength(10);

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
            #endregion

            #region DRD – Relations
            /// <summary>Relation Institution (1) → Branches (N).</summary>
            builder.HasOne(e => e.Institution)
                .WithMany(i => i.Branches)
                .HasForeignKey(e => e.InstitutionNumber)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
        #endregion
    }
}
