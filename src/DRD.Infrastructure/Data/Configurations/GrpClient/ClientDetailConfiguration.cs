// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 ClientDetailConfiguration.cs
// Type de fichier                Configuration EF Core
// Nature C#                      Class
// Emplacement                    Data/Configurations/GrpClient
// Auteur                         Michel Gariépy
// Créé le                        2025-12-01
//
// Description
//     Configuration minimisée EF Core pour l'entité ClientDetail. Cette
//     configuration définit la clé composite, la relation vers Client et les
//     contraintes essentielles sur les colonnes.
//
// Fonctionnalité
//     - Définir la clé composite (ClientNumber + DrdNumber).
//     - Ignorer la propriété Id héritée de BaseAuditableEntity.
//     - Configurer quelques contraintes essentielles (required, max length).
//     - Configurer la relation 1 → N avec Client.
//
// Modifications
//     2025-12-01    Version initiale conforme au standard DRD (Option B).
// ============================================================================

using DRD.Domain.Entities.Client;
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
		/// Configure le mapping Entity Framework Core pour l'entité ClientDetail.
		/// </summary>
		public void Configure(EntityTypeBuilder<ClientDetail> builder)
		{
			#region DRD – Table
			/// <summary>
			/// Définition du nom de la table physique.
			/// </summary>
			builder.ToTable("ClientDetail");
			#endregion

			#region DRD – Clé primaire composite
			/// <summary>
			/// ClientDetail est identifié par une clé composite :
			/// ClientNumber + DrdNumber.
			/// </summary>
			builder.HasKey(e => new { e.ClientNumber, e.DrdNumber });

			/// <summary>
			/// Ignore la propriété Id héritée (clé composite utilisée).
			/// </summary>
			builder.Ignore(e => e.Id);
			#endregion

			#region DRD – Colonnes principales (minimal)
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

			#region DRD – Relation vers Client
			/// <summary>
			/// Relation 1 → N :
			/// Un Client possède plusieurs ClientDetail.
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
