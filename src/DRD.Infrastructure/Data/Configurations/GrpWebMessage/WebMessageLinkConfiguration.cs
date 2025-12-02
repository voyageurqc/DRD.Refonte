// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 WebMessageLinkConfiguration.cs
// Type de fichier                Configuration EF Core
// Nature C#                      Class
// Emplacement                    Data/Configurations/GrpWebMessage
// Auteur                         Michel Gariépy
// Créé le                        2025-12-01
//
// Description
//     Configuration EF Core pour l'entité WebMessageLink. Définit la clé
//     primaire, les contraintes essentielles et la relation vers WebMessage.
//
// Fonctionnalité
//     - Définir la clé primaire (Id ignoré car non utilisé).
//     - Définir les contraintes essentielles (required, max length).
//     - Configurer la relation 1 → N vers WebMessage.
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
	/// Configuration EF Core pour l'entité WebMessageLink.
	/// </summary>
	public class WebMessageLinkConfiguration : IEntityTypeConfiguration<WebMessageLink>
	{
		#region DRD – Configuration
		/// <summary>
		/// Configure le mapping Entity Framework Core pour l'entité WebMessageLink.
		/// </summary>
		public void Configure(EntityTypeBuilder<WebMessageLink> builder)
		{
			#region DRD – Table
			/// <summary>Définition du nom de la table physique.</summary>
			builder.ToTable("WebMessageLink");
			#endregion

			#region DRD – Clé primaire
			/// <summary>
			/// WebMessageLink n'a pas de clé naturelle propre.
			/// EF générera automatiquement une clé Id (sauf si ignorée).
			/// Ici, on utilise Id hérité comme clé technique.
			/// </summary>
			builder.HasKey(e => e.Id);
			#endregion

			#region DRD – Colonnes principales
			builder.Property(e => e.Title)
				   .IsRequired()
				   .HasMaxLength(200);

			builder.Property(e => e.Url)
				   .IsRequired()
				   .HasMaxLength(500);

			builder.Property(e => e.WebMessageId)
				   .IsRequired();
			#endregion

			#region DRD – Relations
			/// <summary>
			/// Relation 1 → N :
			/// Un WebMessage possède plusieurs WebMessageLink.
			/// </summary>
			builder.HasOne(e => e.WebMessage)
				   .WithMany(m => m.Links)
				   .HasForeignKey(e => e.WebMessageId)
				   .OnDelete(DeleteBehavior.Cascade);
			#endregion
		}
		#endregion
	}
}
