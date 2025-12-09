// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 UserViewAccessConfiguration.cs
// Type de fichier                Configuration EF Core
// Classe                         UserViewAccessConfiguration
// Emplacement                    Data/Configurations/GrpIdentity
// Entités concernées             UserViewAccess, ApplicationUser, ApplicationView
// Créé le                        2025-12-01
//
// Description
//     Configuration EF Core pour l'entité UserViewAccess. Cette entité définit
//     un droit d'accès (privilège) d'un utilisateur vers une vue spécifique.
//     La configuration précise la clé composite, les contraintes et les relations.
//
// Fonctionnalité
//     - Définir la clé composite (UserId + ViewCode).
//     - Conserver Id hérité (audit), sans l’utiliser comme clé.
//     - Définir les contraintes essentielles (required, max length).
//     - Configurer les relations vers ApplicationUser et ApplicationView.
//
// Modifications
//     2025-12-09    Ajustements DRD (en-tête, régions, résumés XML).
//     2025-12-02    Suppression de Ignore(Id) — nécessaire pour UserAudit.
//     2025-12-01    Création initiale conforme au standard DRD.
// ============================================================================

using DRD.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpIdentity
{
    /// <summary>
    /// Configuration EF Core pour l'entité UserViewAccess.
    /// </summary>
    public class UserViewAccessConfiguration : IEntityTypeConfiguration<UserViewAccess>
    {
        #region DRD – Configuration générale
        /// <summary>
        /// Configure le mapping Entity Framework Core pour l'entité UserViewAccess.
        /// </summary>
        public void Configure(EntityTypeBuilder<UserViewAccess> builder)
        {
            #region DRD – Table
            /// <summary>Définition du nom de la table physique.</summary>
            builder.ToTable("UserViewAccess");
            #endregion

            #region DRD – Clé composite
            /// <summary>
            /// Définition de la clé composite : UserId + ViewCode.
            /// Id (hérité) est conservé pour audit.
            /// </summary>
            builder.HasKey(e => new { e.UserId, e.ViewCode });
            #endregion

            #region DRD – Champs
            /// <summary>Définition des contraintes principales.</summary>

            builder.Property(e => e.UserId)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(e => e.ViewCode)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.PrivilegeCode)
                   .IsRequired()
                   .HasMaxLength(20);
            #endregion

            #region DRD – Relations
            /// <summary>
            /// Relation 1 → N : un utilisateur possède plusieurs accès.
            /// </summary>
            builder.HasOne(e => e.User)
                   .WithMany(u => u.ViewAccesses)
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            /// <summary>
            /// Relation 1 → N : une vue peut être assignée à plusieurs utilisateurs.
            /// </summary>
            builder.HasOne(e => e.ApplicationView)
                   .WithMany(v => v.ViewAccesses)
                   .HasForeignKey(e => e.ViewCode)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
        #endregion
    }
}
