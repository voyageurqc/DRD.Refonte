// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 ClientConfiguration.cs
// Type de fichier                Configuration EF Core
// Classe                         ClientConfiguration
// Emplacement                    Data/Configurations/GrpClient
// Entités concernées             Client, ClientDetail
// Créé le                        2025-12-01
//
// Description
//     Configuration Entity Framework Core pour l'entité Client. Décrit la clé
//     naturelle, les contraintes de structure, ainsi que la relation Client →
//     ClientDetail (1 → N). Le Client sert de maître pour l’ensemble du module.
//
// Fonctionnalité
//     - Définit ClientNumber comme clé naturelle.
//     - Ignore Id hérité (non utilisé comme clé).
//     - Configure les longueurs et contraintes des colonnes.
//     - Configure la relation Client (1) → ClientDetail (N).
//
// Modifications
//     2025-12-09    Ajustements DRD (en-tête, résumés, régions).
//     2025-12-01    Version DRD conforme (header, régions, alignement).
//     2025-12-01    Création initiale générée selon les nouvelles entités Domain.
// ============================================================================

using DRD.Domain.Entities.GrpClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpClient
{
    /// <summary>
    /// Configuration EF Core alignée sur la table Client.
    /// </summary>
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        #region DRD – Configuration
        /// <summary>
        /// Configure le mapping Entity Framework Core pour l'entité Client.
        /// </summary>
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            #region DRD – Table
            /// <summary>Nom de la table physique associée à Client.</summary>
            builder.ToTable("Client");
            #endregion

            #region DRD – Clé
            /// <summary>Définit la clé primaire basée sur ClientNumber.</summary>
            builder.HasKey(e => e.ClientNumber);

            /// <summary>Ignore Id héritée (clé naturelle utilisée).</summary>
            builder.Ignore(e => e.Id);
            #endregion

            #region DRD – Champs
            /// <summary>Configuration des propriétés principales.</summary>

            builder.Property(e => e.ClientNumber)
                .IsRequired();

            builder.Property(e => e.ClientName1)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.ClientName2)
                .HasMaxLength(150);

            builder.Property(e => e.Address1)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Address2)
                .HasMaxLength(200);

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.CountryCode)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.ProvinceCode)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.PostalCode)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.ContactName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Phone1)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(e => e.Phone2)
                .HasMaxLength(25);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.OrganizationType)
                .HasMaxLength(20);

            builder.Property(e => e.EmployeeName)
                .HasMaxLength(150);

            builder.Property(e => e.EntryOrder)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.McpaCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.TransferMode)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.AccountType)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.ReportType)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.ServerLocation)
                .HasMaxLength(200);

            builder.Property(e => e.CultureCode)
                .IsRequired()
                .HasMaxLength(10);
            #endregion

            #region DRD – Relations
            /// <summary>
            /// Relation 1 → N entre Client et ClientDetail.
            /// </summary>
            builder.HasMany(e => e.Details)
                .WithOne(d => d.Client)
                .HasForeignKey(d => d.ClientNumber)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
        #endregion
    }
}
