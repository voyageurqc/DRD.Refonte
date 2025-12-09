// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 ApplicationViewConfiguration.cs
// Type de fichier                Configuration EF Core
// Classe                         ApplicationViewConfiguration
// Emplacement                    Data/Configurations/GrpIdentity
// Entités concernées             ApplicationView, UserViewAccess
// Créé le                        2025-12-01
//
// Description
//     Configuration EF Core pour l'entité ApplicationView. Cette configuration
//     définit la clé primaire, les contraintes de structure et la relation
//     vers UserViewAccess.
//
// Fonctionnalité
//     - Définir la clé primaire ViewCode.
//     - Configurer les contraintes essentielles (required, max length).
//     - Configurer la relation 1 → N vers UserViewAccess.
//
// Modifications
//     2025-12-09    Ajustements DRD (en-tête, résumés XML, organisation).
//     2025-12-02    Ajout de Ignore(Id) dans la version DRDv10 (retiré ensuite car non requis).
//     2025-12-01    Création initiale conforme au standard DRD.
// ============================================================================

using DRD.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpIdentity
{
    /// <summary>
    /// Configuration EF Core pour l'entité ApplicationView.
    /// </summary>
    public class ApplicationViewConfiguration : IEntityTypeConfiguration<ApplicationView>
    {
        #region DRD – Configuration générale
        /// <summary>
        /// Configure le mapping EF Core pour l'entité ApplicationView.
        /// </summary>
        public void Configure(EntityTypeBuilder<ApplicationView> builder)
        {
            #region DRD – Table
            /// <summary>Définition du nom de la table physique.</summary>
            builder.ToTable("ApplicationView");
            #endregion

            #region DRD – Clé
            /// <summary>Clé naturelle basée sur ViewCode.</summary>
            builder.HasKey(e => e.ViewCode);
            #endregion

            #region DRD – Champs
            /// <summary>Définition des contraintes principales.</summary>

            builder.Property(e => e.ViewCode)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Controller)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Action)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.DescriptionFr)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.DescriptionEn)
                   .HasMaxLength(200);
            #endregion

            #region DRD – Relations
            /// <summary>
            /// Relation 1 → N entre ApplicationView et UserViewAccess.
            /// </summary>
            builder.HasMany(e => e.ViewAccesses)
                   .WithOne(v => v.ApplicationView)
                   .HasForeignKey(v => v.ViewCode)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
        #endregion
    }
}
