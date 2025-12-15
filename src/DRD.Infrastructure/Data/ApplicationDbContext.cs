// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 ApplicationDbContext.cs
// Type de fichier                DbContext
// Nature C#                      Class
// Emplacement                    Data/
// Auteur                         Michel Gariépy
// Créé le                        2025-12-02
//
// Description
//     Contexte de base de données principal pour l'application DRD. Intègre
//     les entités métiers (Domain), les entités Identity personnalisées et
//     applique les configurations EF Core.
//
// Fonctionnalité
//     - Expose les DbSet pour toutes les entités Domain/Identity nécessaires.
//     - Intègre ASP.NET Identity via IdentityDbContext<ApplicationUser>.
//     - Applique automatiquement toutes les configurations IEntityTypeConfiguration
//       via ApplyConfigurationsFromAssembly.
//
// Modifications
//     2025-12-15    Ajout de la surcharge SaveChangesAsync dans ApplicationDbContext
//					 afin de centraliser la mise à jour automatique des métadonnées d’audit
//					 (CreationDate, CreatedBy, ModificationDate, UpdatedBy).
//     2025-12-09    Ajustements DRD (résumés des régions, validation CdSet).
//     2025-12-02    Création initiale (clean start .NET 10, BD DRDv10).
// ============================================================================

using DRD.Application.Common.Interfaces;
using DRD.Domain.Common;
using DRD.Domain.Entities.GrpClient;
using DRD.Domain.Entities.GrpSystemTables;
using DRD.Domain.Entities.GrpWebMessage;
using DRD.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace DRD.Infrastructure.Data
{
    /// <summary>
    /// Contexte EF Core principal de l'application DRD.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		#region DRD – Constructeurs
		/// <summary>
		/// Initialise une nouvelle instance du contexte de base de données DRD.
		/// </summary>
		/// <param name="options">Options de configuration du contexte.</param>
		private readonly ICurrentUserService _currentUserService;

		public ApplicationDbContext(
			DbContextOptions<ApplicationDbContext> options,
			ICurrentUserService currentUserService)
			: base(options)
		{
			_currentUserService = currentUserService;
		}
		#endregion

		#region DRD – DbSet Domain
		/// <summary>
		/// Jeu d'entités pour les clients maîtres DRD.
		/// </summary>
		public DbSet<Client> Clients { get; set; } = null!;

        /// <summary>
        /// Jeu d'entités pour les détails DRD associés aux clients.
        /// </summary>
        public DbSet<ClientDetail> ClientDetails { get; set; } = null!;

        /// <summary>
        /// Jeu d'entités pour les individus associés aux détails DRD.
        /// </summary>
        public DbSet<Individual> Individuals { get; set; } = null!;

        /// <summary>
        /// Jeu d'entités pour les codes paramétriques (CdSet).
        /// </summary>
        public DbSet<CdSet> CdSets { get; set; } = null!;

        /// <summary>
        /// Jeu d'entités pour la table interne de suivi des tables.
        /// </summary>
        public DbSet<DatabaseTable> DatabaseTables { get; set; } = null!;

        /// <summary>
        /// Jeu d'entités pour les institutions financières.
        /// </summary>
        public DbSet<Institution> Institutions { get; set; } = null!;

        /// <summary>
        /// Jeu d'entités pour les succursales financières.
        /// </summary>
        public DbSet<Branch> Branches { get; set; } = null!;

        /// <summary>
        /// Jeu d'entités pour les messages Web affichés aux usagers.
        /// </summary>
        public DbSet<WebMessage> WebMessages { get; set; } = null!;

        /// <summary>
        /// Jeu d'entités pour les liens associés aux messages Web.
        /// </summary>
        public DbSet<WebMessageLink> WebMessageLinks { get; set; } = null!;

        /// <summary>
        /// Jeu d'entités pour les états individuels des messages Web.
        /// </summary>
        public DbSet<WebMessageUser> WebMessageUsers { get; set; } = null!;
        #endregion

        #region DRD – DbSet Identity complémentaires
        /// <summary>
        /// Jeu d'entités pour les types d'accès internes.
        /// </summary>
        public DbSet<AccessType> AccessTypes { get; set; } = null!;

        /// <summary>
        /// Jeu d'entités pour les vues disponibles dans l'application.
        /// </summary>
        public DbSet<ApplicationView> ApplicationViews { get; set; } = null!;

        /// <summary>
        /// Jeu d'entités pour les droits d'accès par utilisateur et par vue.
        /// </summary>
        public DbSet<UserViewAccess> UserViewAccesses { get; set; } = null!;
		#endregion
		#region DRD – Audit & SaveChanges
		/// <summary>
		/// Intercepte les sauvegardes EF Core afin de mettre à jour automatiquement
		/// les champs de métadonnées (création / modification) pour toutes les entités
		/// implémentant IAuditableEntity.
		/// </summary>
		public override async Task<int> SaveChangesAsync(
			CancellationToken cancellationToken = default)
		{
			var now = DateTime.UtcNow;

			// Valeur temporaire — sera remplacée par ICurrentUserService
			var currentUser = _currentUserService?.UserName ?? "SYSTEM";

			foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
			{
				if (entry.State == EntityState.Added)
				{
					entry.Entity.CreationDate = now;
					entry.Entity.CreatedBy = currentUser;
					entry.Entity.ModificationDate = now;
					entry.Entity.UpdatedBy = currentUser;
				}
				else if (entry.State == EntityState.Modified)
				{
					entry.Entity.ModificationDate = now;
					entry.Entity.UpdatedBy = currentUser;

					// Protection DRD : jamais modifier la création
					entry.Property(nameof(IAuditableEntity.CreationDate)).IsModified = false;
					entry.Property(nameof(IAuditableEntity.CreatedBy)).IsModified = false;
				}
			}

			return await base.SaveChangesAsync(cancellationToken);
		}
		#endregion

		#region DRD – Configuration EF Core
		/// <summary>
		/// Configure le modèle EF Core (mapping entités ↔ tables).
		/// Applique automatiquement toutes les configurations présentes
		/// dans l'assembly DRD.Infrastructure.
		/// </summary>
		/// <param name="modelBuilder">Constructeur de modèle EF Core.</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Appel obligatoire de la configuration Identity.
            base.OnModelCreating(modelBuilder);

            // Application automatique des configurations IEntityTypeConfiguration<>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        #endregion
    }
}
