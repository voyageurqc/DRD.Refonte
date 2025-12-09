// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 InstitutionConfiguration.cs
// Type de fichier                Configuration EF Core
// Classe                         InstitutionConfiguration
// Emplacement                    Data/Configurations/GrpSystemTables
// Entités concernées             Institution, Branch
// Créé le                        2025-12-01
//
// Description
//     Configuration Entity Framework Core pour l'entité Institution. Définit
//     la clé primaire, les contraintes de structure ainsi que la relation
//     institution → succursales (1 → N).
//
// Fonctionnalité
//     - Définit InstitutionNumber comme clé primaire.
//     - Ignore Id hérité (non utilisé pour cette entité).
//     - Déclare les longueurs des colonnes.
//     - Configure la relation Institution (1) → Branches (N).
//
// Modifications
//     2025-12-09    Ajustements DRD (en-tête, résumés, régions).
//     2025-12-01    Création initiale conforme au standard DRD .NET 10.
// ============================================================================

using DRD.Domain.Entities.GrpSystemTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpSystemTables
{
    /// <summary>
    /// Configuration EF Core strictement alignée sur la table Institution.
    /// </summary>
    public class InstitutionConfiguration : IEntityTypeConfiguration<Institution>
    {
        #region DRD – Configuration
        /// <summary>
        /// Configure le mapping EF Core pour l'entité Institution.
        /// </summary>
        public void Configure(EntityTypeBuilder<Institution> builder)
        {
            #region DRD – Table
            /// <summary>Nom de la table physique réelle.</summary>
            builder.ToTable("Institution");
            #endregion

            #region DRD – Clé
            /// <summary>Définition de la clé primaire InstitutionNumber.</summary>
            builder.HasKey(e => e.InstitutionNumber);

            /// <summary>Ignore l’Id héritée (non utilisée pour cette entité).</summary>
            builder.Ignore(e => e.Id);
            #endregion

            #region DRD – Champs
            /// <summary>Configuration des propriétés principales.</summary>
            builder.Property(e => e.InstitutionNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);
            #endregion

            #region DRD – Relations
            /// <summary>Relation Institution (1) → Branches (N).</summary>
            builder.HasMany(e => e.Branches)
                .WithOne(b => b.Institution)
                .HasForeignKey(b => b.InstitutionNumber)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
        #endregion
    }
}
