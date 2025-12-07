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
//     2025-12-02    Création initiale (clean start .NET 10, BD DRDv10).
// ============================================================================

using DRD.Domain.Entities.GrpClient;
using DRD.Domain.Entities.GrpSystemTables;
using DRD.Domain.Entities.GrpWebMessage;
using DRD.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
		/// <param name="options">
		/// Options de configuration du contexte, injectées par le conteneur DI.
		/// </param>
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		#endregion

		#region DRD – DbSet Domain
		/// <summary>
		/// Clients DRD (niveau maître).
		/// </summary>
		public DbSet<Client> Clients { get; set; } = null!;

		/// <summary>
		/// Détails DRD par client.
		/// </summary>
		public DbSet<ClientDetail> ClientDetails { get; set; } = null!;

		/// <summary>
		/// Individus associés aux détails DRD.
		/// </summary>
		public DbSet<Individual> Individuals { get; set; } = null!;

		/// <summary>
		/// Codes paramétriques génériques (CodeSet/CdSet).
		/// </summary>
		public DbSet<CdSet> CdSets { get; set; } = null!;

		/// <summary>
		/// Table interne de suivi du nombre d’enregistrements par table.
		/// </summary>
		public DbSet<DatabaseTable> DatabaseTables { get; set; } = null!;

		/// <summary>
		/// Institutions financières.
		/// </summary>
		public DbSet<Institution> Institutions { get; set; } = null!;

		/// <summary>
		/// Succursales des institutions financières.
		/// </summary>
		public DbSet<Branch> Branches { get; set; } = null!;

		/// <summary>
		/// Messages Web publiés aux usagers.
		/// </summary>
		public DbSet<WebMessage> WebMessages { get; set; } = null!;

		/// <summary>
		/// Liens associés aux messages Web.
		/// </summary>
		public DbSet<WebMessageLink> WebMessageLinks { get; set; } = null!;

		/// <summary>
		/// Association messages Web ↔ usagers (lecture, actions).
		/// </summary>
		public DbSet<WebMessageUser> WebMessageUsers { get; set; } = null!;
		#endregion

		#region DRD – DbSet Identity complémentaires
		/// <summary>
		/// Types d'accès internes.
		/// </summary>
		public DbSet<AccessType> AccessTypes { get; set; } = null!;

		/// <summary>
		/// Vues applicatives disponibles (contrôleur / action).
		/// </summary>
		public DbSet<ApplicationView> ApplicationViews { get; set; } = null!;

		/// <summary>
		/// Droits d'accès par utilisateur et par vue.
		/// </summary>
		public DbSet<UserViewAccess> UserViewAccesses { get; set; } = null!;
		#endregion

		#region DRD – Configuration EF Core
		/// <summary>
		/// Configure le modèle EF Core (mapping entités ↔ tables).
		/// </summary>
		/// <param name="modelBuilder">
		/// Constructeur de modèle utilisé pour configurer les entités.
		/// </param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Appel de la configuration Identity de base.
			base.OnModelCreating(modelBuilder);

			// Application automatique de toutes les configurations IEntityTypeConfiguration
			// présentes dans l'assembly DRD.Infrastructure.
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		}
		#endregion
	}
}
