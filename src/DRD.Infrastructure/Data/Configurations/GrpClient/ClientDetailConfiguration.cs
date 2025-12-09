// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 ClientDetailConfiguration.cs
// Type de fichier                Configuration EF Core
// Classe                         ClientDetailConfiguration
// Emplacement                    Data/Configurations/GrpClient
// Entités concernées             ClientDetail, Client
// Créé le                        2025-12-01
//
// Description
//     Configuration Entity Framework Core pour l'entité ClientDetail. Cette
//     configuration définit la clé composite, les contraintes essentielles ainsi
//     que la relation 1 → N vers l'entité Client. Le niveau de détails est volontairement
//     minimal conformément aux règles DRDv10.
//
// Fonctionnalité
//     - Définit la clé composite (ClientNumber + DrdNumber).
//     - Ignore la propriété Id héritée (clé composite utilisée).
//     - Configure les longueurs maximales sur les colonnes importantes.
//     - Configure la relation vers Client (1 → N).
//
// Modifications
//     2025-12-09    Ajustements DRD (régions, en-tête Option B, résumés).
//     2025-12-01    Version initiale conforme au standard DRD.
// ============================================================================

using DRD.Domain.Entities.GrpClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpClient
{
    /// <summary>
    /// Configuration EF Core pour l'entité ClientDetail.
    /// </summary>
    public class ClientDetailConfiguration : IEntityTypeConfiguration<ClientDetail>
    {
        #region DRD – Configuration
        /// <summary>
        /// Configure le mapping Entity Framework Core pour ClientDetail.
        /// </summary>
        public void Configure(EntityTypeBuilder<ClientDetail> builder)
        {
            #region DRD – Table
            /// <summary>Nom de la table physique associée à ClientDetail.</summary>
            builder.ToTable("ClientDetail");
            #endregion

            #region DRD – Clé
            /// <summary>
            /// Définition de la clé composite : ClientNumber + DrdNumber.
            /// </summary>
            builder.HasKey(e => new { e.ClientNumber, e.DrdNumber });

            /// <summary>
            /// Ignore la propriété Id héritée (clé composite utilisée).
            /// </summary>
            builder.Ignore(e => e.Id);
            #endregion

            #region DRD – Champs
            /// <summary>Configuration minimale des propriétés.</summary>

            builder.Property(e => e.TransactionType)
                   .HasMaxLength(10);

            builder.Property(e => e.FrequencyCode)
                   .HasMaxLength(10);

            builder.Property(e => e.ContactName)
                   .HasMaxLength(150);

            builder.Property(e => e.DepartureReason)
                   .HasMaxLength(50);

            builder.Property(e => e.InvoiceCalculationMode)
                   .HasMaxLength(10);

            builder.Property(e => e.InvoiceFrequencyCode)
                   .HasMaxLength(10);

            builder.Property(e => e.GLAccount)
                   .HasMaxLength(50);

            builder.Property(e => e.Description)
                   .HasMaxLength(500);

            builder.Property(e => e.InternalUseOnly)
                   .HasMaxLength(200);

            builder.Property(e => e.TransmissionInProgress)
                   .HasMaxLength(10);

            builder.Property(e => e.DepositWithdrawalCode)
                   .HasMaxLength(10);

            builder.Property(e => e.AuthorizationLevel)
                   .HasMaxLength(10);

            builder.Property(e => e.PreDepositDelay)
                   .HasMaxLength(10);

            builder.Property(e => e.PayingClient)
                   .HasMaxLength(10);
            #endregion

            #region DRD – Relations
            /// <summary>
            /// Relation 1 → N : un Client possède plusieurs ClientDetail.
            /// </summary>
            builder.HasOne(e => e.Client)
                   .WithMany(c => c.Details)
                   .HasForeignKey(e => e.ClientNumber)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
        #endregion
    }
}
