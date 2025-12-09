// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 IndividualConfiguration.cs
// Type de fichier                Configuration EF Core
// Classe                         IndividualConfiguration
// Emplacement                    Data/Configurations/GrpClient
// Entités concernées             Individual, ClientDetail
// Créé le                        2025-12-01
//
// Description
//     Configuration EF Core pour l'entité Individual. Cette configuration
//     définit la clé composite, les contraintes essentielles et la relation
//     1 → N vers ClientDetail. Le niveau de détails est volontairement
//     minimal conformément aux règles DRDv10.
//
// Fonctionnalité
//     - Définit la clé composite (ClientNumber + DrdNumber + IndividualNumber).
//     - Ignore la propriété Id héritée.
//     - Configure les longueurs maximales pertinentes.
//     - Configure la relation vers ClientDetail.
//
// Modifications
//     2025-12-09    Ajustements DRD (régions, en-tête Option B, résumés).
//     2025-12-01    Création initiale conforme au standard DRD.
// ============================================================================

using DRD.Domain.Entities.GrpClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpClient
{
    /// <summary>
    /// Configuration EF Core pour l'entité Individual.
    /// </summary>
    public class IndividualConfiguration : IEntityTypeConfiguration<Individual>
    {
        #region DRD – Configuration
        /// <summary>
        /// Configure le mapping Entity Framework Core pour Individual.
        /// </summary>
        public void Configure(EntityTypeBuilder<Individual> builder)
        {
            #region DRD – Table
            /// <summary>Définition du nom de la table physique.</summary>
            builder.ToTable("Individual");
            #endregion

            #region DRD – Clé
            /// <summary>
            /// Définition de la clé composite :
            /// ClientNumber + DrdNumber + IndividualNumber.
            /// </summary>
            builder.HasKey(e => new { e.ClientNumber, e.DrdNumber, e.IndividualNumber });

            /// <summary>
            /// Ignore la propriété Id héritée.
            /// </summary>
            builder.Ignore(e => e.Id);
            #endregion

            #region DRD – Champs
            /// <summary>Configuration minimale des propriétés.</summary>

            builder.Property(e => e.IndividualName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(e => e.IndividualName1)
                   .HasMaxLength(150);

            builder.Property(e => e.IndividualName2)
                   .HasMaxLength(150);

            builder.Property(e => e.Address1)
                   .HasMaxLength(200);

            builder.Property(e => e.Address2)
                   .HasMaxLength(200);

            builder.Property(e => e.City)
                   .HasMaxLength(100);

            builder.Property(e => e.CountryCode)
                   .HasMaxLength(5);

            builder.Property(e => e.ProvinceCode)
                   .HasMaxLength(5);

            builder.Property(e => e.PostalCode)
                   .HasMaxLength(15);

            builder.Property(e => e.Email)
                   .HasMaxLength(150);

            builder.Property(e => e.Phone1)
                   .HasMaxLength(25);

            builder.Property(e => e.Phone2)
                   .HasMaxLength(25);

            builder.Property(e => e.LanguageCode)
                   .HasMaxLength(10);

            builder.Property(e => e.Reference)
                   .HasMaxLength(50);

            builder.Property(e => e.KeyName)
                   .HasMaxLength(50);
            #endregion

            #region DRD – Relations
            /// <summary>
            /// Relation 1 → N :
            /// Un ClientDetail possède plusieurs Individuals.
            /// </summary>
            builder.HasOne(e => e.ClientDetail)
                   .WithMany()
                   .HasForeignKey(e => new { e.ClientNumber, e.DrdNumber })
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
        #endregion
    }
}
