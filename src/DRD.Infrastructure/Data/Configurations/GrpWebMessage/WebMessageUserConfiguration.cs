// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 WebMessageUserConfiguration.cs
// Type de fichier                Configuration EF Core
// Nature C#                      Class
// Emplacement                    Data/Configurations/GrpWebMessage
// Auteur                         Michel Gariépy
// Créé le                        2025-12-01
//
// Description
//     Configuration EF Core pour l'entité WebMessageUser. Définit la clé
//     primaire, les contraintes essentielles et la relation vers WebMessage.
//
// Fonctionnalité
//     - Définir la clé primaire (Id hérité de BaseAuditableEntity).
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
	/// Configuration EF Core pour l'entité WebMessageUser.
	/// </summary>
	public class WebMessageUserConfiguration : IEntityTypeConfiguration<WebMessageUser>
	{
		#region DRD – Configuration
		/// <summary>
		/// Configure le mapping Entity Framework Core pour l'entité WebMessageUser.
		/// </summary>
		public void Configure(EntityTypeBuilder<WebMessageUser> builder)
		{
			#region DRD – Table
			/// <summary>Définition du nom de la table physique.</summary>
			builder.ToTable("WebMessageUser");
			#endregion

			#region DRD – Clé primaire
			/// <summary>
			/// WebMessageUser n'a pas de clé naturelle.
			/// On utilise donc la clé Id héritée de BaseAuditableEntity.
			/// </summary>
			builder.HasKey(e => e.Id);
			#endregion

			#region DRD – Colonnes principales
			builder.Property(e => e.WebMessageId)
				   .IsRequired();

			builder.Property(e => e.UserId)
				   .IsRequired()
				   .HasMaxLength(50);

			builder.Property(e => e.ReadDate);

			builder.Property(e => e.ActionDate);

			builder.Property(e => e.Accepted);
			#endregion

			#region DRD – Relations
			/// <summary>
			/// Relation 1 → N :
			/// Un WebMessage possède plusieurs WebMessageUser.
			/// </summary>
			builder.HasOne(e => e.WebMessage)
				   .WithMany(m => m.Users)
				   .HasForeignKey(e => e.WebMessageId)
				   .OnDelete(DeleteBehavior.Cascade);
			#endregion
		}
		#endregion
	}
}
