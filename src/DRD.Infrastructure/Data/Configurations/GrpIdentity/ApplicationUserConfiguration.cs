// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 ApplicationUserConfiguration.cs
// Type de fichier                Configuration EF Core
// Classe                         ApplicationUserConfiguration
// Emplacement                    Data/Configurations/GrpIdentity
// Entités concernées             ApplicationUser, UserViewAccess
// Créé le                        2025-12-01
//
// Description
//     Configuration EF Core pour l'entité ApplicationUser. Cette configuration
//     étend l'entité IdentityUser avec les champs personnalisés DRD, les
//     contraintes sur les colonnes et la relation vers UserViewAccess.
//
// Fonctionnalité
//     - Configurer les colonnes personnalisées (required, max length).
//     - Configurer la relation 1 → N avec UserViewAccess.
//     - Assurer un mapping propre des champs d'audit.
//
// Modifications
//     2025-12-09    Ajustements DRD (en-tête, résumés XML, organisation).
//     2025-12-01    Création initiale conforme au standard DRD.
// ============================================================================

using DRD.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpIdentity
{
    /// <summary>
    /// Configuration EF Core pour l'entité ApplicationUser.
    /// </summary>
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        #region DRD – Configuration
        /// <summary>
        /// Configure le mapping EF Core pour l'entité ApplicationUser.
        /// </summary>
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            #region DRD – Table
            /// <summary>Nom explicite de la table Identity User.</summary>
            builder.ToTable("ApplicationUser");
            #endregion

            #region DRD – Colonnes principales (Profil)
            /// <summary>Colonnes de profil utilisateur.</summary>

            builder.Property(e => e.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.AddressLine)
                   .HasMaxLength(200);

            builder.Property(e => e.City)
                   .HasMaxLength(100);

            builder.Property(e => e.Province)
                   .HasMaxLength(50);

            builder.Property(e => e.PostalCode)
                   .HasMaxLength(15);
            #endregion

            #region DRD – Colonnes principales (Préférences)
            /// <summary>Colonnes préférences utilisateur.</summary>

            builder.Property(e => e.DefaultPrinter)
                   .HasMaxLength(100);

            builder.Property(e => e.LaserPrinter)
                   .HasMaxLength(100);
            #endregion

            #region DRD – Colonnes principales (Codes internes)
            /// <summary>Colonnes internes DRD.</summary>

            builder.Property(e => e.SectorCode)
                   .HasMaxLength(20);

            builder.Property(e => e.AccessTypeCode)
                   .HasMaxLength(20);

            builder.Property(e => e.MenuCode)
                   .HasMaxLength(20);
            #endregion

            #region DRD – Audit Fields
            /// <summary>Champs d’audit DRD hérités de UserAudit.</summary>

            builder.Property(e => e.CreationDate)
                   .IsRequired();

            builder.Property(e => e.ModificationDate)
                   .IsRequired();

            builder.Property(e => e.CreatedBy)
                   .HasMaxLength(50);

            builder.Property(e => e.UpdatedBy)
                   .HasMaxLength(50);

            builder.Property(e => e.IsActive)
                   .IsRequired();
            #endregion

            #region DRD – Relations
            /// <summary>
            /// Un utilisateur possède plusieurs UserViewAccess.
            /// </summary>
            builder.HasMany(e => e.ViewAccesses)
                   .WithOne(v => v.User)
                   .HasForeignKey(v => v.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
        #endregion
    }
}
