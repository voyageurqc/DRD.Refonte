// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 AccessTypeConfiguration.cs
// Type de fichier                Configuration EF Core
// Classe                         AccessTypeConfiguration
// Emplacement                    Data/Configurations/GrpIdentity
// Entités concernées             AccessType, ApplicationUser
// Créé le                        2025-12-01
//
// Description
//     Configuration EF Core pour l'entité AccessType. Cette entité représente
//     un type d'accès interne utilisé pour classifier les utilisateurs et
//     déterminer leur niveau de permissions.
//
// Fonctionnalité
//     - Définir la clé primaire AccessTypeCode.
//     - Définir les contraintes essentielles sur les colonnes.
//     - Configurer la relation 1 → N vers ApplicationUser.
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
    /// Configuration EF Core pour l'entité AccessType.
    /// </summary>
    public class AccessTypeConfiguration : IEntityTypeConfiguration<AccessType>
    {
        #region DRD – Configuration
        /// <summary>
        /// Configure le mapping EF Core pour l'entité AccessType.
        /// </summary>
        public void Configure(EntityTypeBuilder<AccessType> builder)
        {
            #region DRD – Table
            /// <summary>Définition du nom de la table physique.</summary>
            builder.ToTable("AccessType");
            #endregion

            #region DRD – Clé
            /// <summary>Clé naturelle basée sur AccessTypeCode.</summary>
            builder.HasKey(e => e.AccessTypeCode);
            #endregion

            #region DRD – Champs
            /// <summary>Définition des contraintes principales.</summary>

            builder.Property(e => e.AccessTypeCode)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(e => e.DescriptionFr)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.DescriptionEn)
                   .HasMaxLength(200);
            #endregion

            #region DRD – Relations
            /// <summary>
            /// Un AccessType peut être lié à plusieurs utilisateurs.
            /// </summary>
            builder.HasMany(e => e.Users)
                   .WithOne(u => u.AccessType)
                   .HasForeignKey(u => u.AccessTypeCode)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
        #endregion
    }
}
