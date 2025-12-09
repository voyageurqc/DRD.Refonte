// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 DatabaseTableConfiguration.cs
// Type de fichier                Configuration EF Core
// Classe                         DatabaseTableConfiguration
// Emplacement                    Data/Configurations/GrpSystemTables
// Entités concernées             DatabaseTable
// Créé le                        2025-12-01
//
// Description
//     Configuration Entity Framework Core pour l'entité DatabaseTable. Utilisée
//     pour représenter un registre système contenant le nombre d’enregistrements
//     présents dans chaque table de la base.
//
// Fonctionnalité
//     - Définit la clé primaire basée sur TableName.
//     - Ignore la propriété Id héritée (non utilisée comme clé).
//     - Déclare les contraintes de structure : required, longueur, type.
//
// Modifications
//     2025-12-09    Ajustements DRD (régions, résumés, en-tête).
//     2025-12-01    Création initiale conforme au standard DRD .NET 10.
// ============================================================================

using DRD.Domain.Entities.GrpSystemTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DRD.Infrastructure.Data.Configurations.GrpSystemTables
{
    /// <summary>
    /// Configuration EF Core strictement alignée sur la table DatabaseTable.
    /// </summary>
    public class DatabaseTableConfiguration : IEntityTypeConfiguration<DatabaseTable>
    {
        #region DRD – Configuration
        /// <summary>
        /// Configure la structure EF Core de l'entité DatabaseTable.
        /// </summary>
        public void Configure(EntityTypeBuilder<DatabaseTable> builder)
        {
            #region DRD – Table
            /// <summary>Nom de la table physique réelle.</summary>
            builder.ToTable("DatabaseTable");
            #endregion

            #region DRD – Clé
            /// <summary>Définition de la clé primaire basée sur TableName.</summary>
            builder.HasKey(e => e.TableName);

            /// <summary>Ignore l’Id héritée (non utilisée comme clé).</summary>
            builder.Ignore(e => e.Id);
            #endregion

            #region DRD – Champs
            /// <summary>Configuration des propriétés principales.</summary>

            builder.Property(e => e.TableName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.RowCount)
                .IsRequired();
            #endregion
        }
        #endregion
    }
}
