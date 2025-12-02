// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 ClientConfiguration.cs
// Type de fichier                Configuration EF Core
// Nature C#                      Class
// Emplacement                    Data/Configurations/
// Auteur                         Michel Gariépy
// Créé le                        2025-12-01
//
// Description
//     Configuration Entity Framework Core pour l'entité Client. Cette
//     configuration définit la clé primaire, les contraintes de colonnes
//     et la relation 1 → N avec ClientDetail.
//
// Fonctionnalité
//     - Définir la clé primaire ClientNumber (clé naturelle).
//     - Ignorer la propriété Id héritée de BaseAuditableEntity.
//     - Configurer les contraintes de structure (required, max length).
//     - Configurer la relation 1 → N avec ClientDetail.
//
// Modifications
//     2025-12-01    Version initiale générée selon les nouvelles entités Domain.
//     2025-12-01    Version DRD conforme au standard (header, régions, alignement).
// ============================================================================

using DRD.Domain.Entities.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ClientEntity = DRD.Domain.Entities.Client.Client;


namespace DRD.Infrastructure.Data.Configurations.GrpClient
{
	/// <summary>
	/// Configuration EF Core pour l'entité Client.
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
			/// <summary>
			/// Définit le nom de la table physique associée à l'entité Client.
			/// </summary>
			builder.ToTable("Client");
			#endregion

			#region DRD – Clé primaire
			/// <summary>
			/// Définit la clé primaire basée sur ClientNumber.
			/// </summary>
			builder.HasKey(e => e.ClientNumber);

			/// <summary>
			/// Ignore la propriété Id héritée (clé naturelle utilisée).
			/// </summary>
			builder.Ignore(e => e.Id);
			#endregion

			#region DRD – Colonnes principales
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
			/// Un Client possède plusieurs Details DRD.
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
