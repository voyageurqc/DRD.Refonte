// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 WebMessageUserConfiguration.cs
// Type de fichier                Configuration EF Core
// Classe                         WebMessageUserConfiguration
// Emplacement                    Data/Configurations/GrpWebMessage
// Entités concernées             WebMessageUser, WebMessage
// Créé le                        2025-12-01
//
// Description
//     Configuration EF Core pour l'entité WebMessageUser. Définit la clé
//     primaire, les contraintes essentielles et la relation vers WebMessage.
//
// Fonctionnalité
//     - Définir la clé primaire (Id hérité).
//     - Définir les contraintes essentielles (required, max length).
//     - Configurer la relation 1 → N vers WebMessage.
//
// Modifications
//     2025-12-09    Ajustements DRD (en-tête, résumés XML, régions DRD).
//     2025-12-01    Création initiale conforme au standard DRD.
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

            #region DRD – Clé
            /// <summary>Utilisation de Id hérité comme clé primaire.</summary>
            builder.HasKey(e => e.Id);
            #endregion

            #region DRD – Champs
            /// <summary>Colonnes principales.</summary>

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
            /// <summary>Relation 1 → N vers WebMessage.</summary>
            builder.HasOne(e => e.WebMessage)
                   .WithMany(m => m.Users)
                   .HasForeignKey(e => e.WebMessageId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
        #endregion
    }
}
