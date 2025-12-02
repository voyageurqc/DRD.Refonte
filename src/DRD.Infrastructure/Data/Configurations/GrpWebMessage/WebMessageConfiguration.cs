// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 WebMessageConfiguration.cs
// Type de fichier                Configuration EF Core
// Nature C#                      Class
// Emplacement                    Data/Configurations/GrpWebMessage
// Auteur                         Michel Gariépy
// Créé le                        2025-12-01
//
// Description
//     Configuration Entity Framework Core pour l'entité WebMessage. Définit la
//     clé primaire, les contraintes essentielles et les relations vers les
//     entités WebMessageLink et WebMessageUser.
//
// Fonctionnalité
//     - Définir la clé primaire (MessageNumber).
//     - Ignorer la propriété Id héritée de BaseAuditableEntity.
//     - Configurer les contraintes essentielles (required, max length).
//     - Configurer les relations 1 → N vers WebMessageLink et WebMessageUser.
//
// Modifications
//     2025-12-01    Création initiale conforme au standard DRD (Option B).
// ============================================================================

using DRD.Domain.Entities.GrpWebMessage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpWebMessage
{
	/// <summary>
	/// Configuration EF Core pour l'entité WebMessage.
	/// </summary>
	public class WebMessageConfiguration : IEntityTypeConfiguration<WebMessage>
	{
		#region DRD – Configuration
		/// <summary>
		/// Configure le mapping Entity Framework Core pour l'entité WebMessage.
		/// </summary>
		public void Configure(EntityTypeBuilder<WebMessage> builder)
		{
			#region DRD – Table
			/// <summary>
			/// Définition du nom de la table physique.
			/// </summary>
			builder.ToTable("WebMessage");
			#endregion

			#region DRD – Clé primaire
			/// <summary>
			/// WebMessage est identifié par MessageNumber (clé naturelle).
			/// </summary>
			builder.HasKey(e => e.MessageNumber);

			/// <summary>
			/// Ignore la propriété Id héritée (clé naturelle utilisée).
			/// </summary>
			builder.Ignore(e => e.Id);
			#endregion

			#region DRD – Colonnes principales
			builder.Property(e => e.TitleFr)
				   .IsRequired()
				   .HasMaxLength(200);

			builder.Property(e => e.TitleEn)
				   .IsRequired()
				   .HasMaxLength(200);

			builder.Property(e => e.ContentFr)
				   .IsRequired();

			builder.Property(e => e.ContentEn)
				   .IsRequired();

			builder.Property(e => e.MessageType)
				   .IsRequired()
				   .HasMaxLength(50);
			#endregion

			#region DRD – Relations
			/// <summary>
			/// Relation 1 → N vers WebMessageLink.
			/// </summary>
			builder.HasMany(e => e.Links)
				   .WithOne(l => l.WebMessage)
				   .HasForeignKey(l => l.WebMessageId)
				   .OnDelete(DeleteBehavior.Cascade);

			/// <summary>
			/// Relation 1 → N vers WebMessageUser.
			/// </summary>
			builder.HasMany(e => e.Users)
				   .WithOne(u => u.WebMessage)
				   .HasForeignKey(u => u.WebMessageId)
				   .OnDelete(DeleteBehavior.Cascade);
			#endregion
		}
		#endregion
	}
}
