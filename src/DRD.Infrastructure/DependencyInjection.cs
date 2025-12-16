// ============================================================================
// Projet:      DRD.Infrastructure
// Fichier:     DependencyInjection.cs
// Type:        Configuration
// Classe:      Static Class
// Emplacement: /
// Entité(s):   UnitOfWork, CdSetRepository, CdSetService,
//              CurrentUserService, UserService
// Créé le:     2025-12-12
//
// Description
//     Point central d’enregistrement des composants techniques de la couche
//     Infrastructure. Cette classe expose l’extension AddInfrastructure() qui
//     permet à DRD.Web d’injecter tous les services d’accès aux données,
//     l’UnitOfWork, les repositories ainsi que les services techniques.
//
// Fonctionnalité
//     - Enregistrer les repositories (ex: CdSetRepository).
//     - Enregistrer UnitOfWork (IUnitOfWork).
//     - Enregistrer les services techniques (CdSetService, CurrentUserService,
//       UserService).
//     - Préparer l’évolution de l’infrastructure DRD v10 (Clients, Institutions).
//     - Maintenir Program.cs clair et conforme Clean Architecture.
//
// Modifications
//     2025-12-12    Création initiale DRD v10.
// ============================================================================

using DRD.Application.Common.Interfaces;
using DRD.Application.Common.Interfaces.Repositories;
using DRD.Application.Common.Interfaces.Services;
using DRD.Infrastructure.Common.Services;
using DRD.Infrastructure.Data;
using DRD.Infrastructure.Repositories.SystemTables;
using DRD.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DRD.Application.Popup.Services.Metadata;
using DRD.Infrastructure.Services.Metadata;


namespace DRD.Infrastructure
{
	/// <summary>
	/// Fournit les méthodes d’enregistrement DI pour la couche Infrastructure.
	/// </summary>
	public static class DependencyInjection
	{
		#region AddInfrastructure
		/// <summary>
		/// Enregistre les composants techniques (repositories, UnitOfWork, services).
		/// </summary>
		public static IServiceCollection AddInfrastructure(
			this IServiceCollection services,
			IConfiguration _configuration)
		{
			// =========================
			// Repositories
			// =========================
			services.AddScoped<ICdSetRepository, CdSetRepository>();

			// =========================
			// Unit of Work
			// =========================
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			// =========================
			// Services techniques
			// =========================
			services.AddScoped<ICurrentUserService, CurrentUserService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IMetadataDisplayService, MetadataDisplayService>();


			return services;
		}
		#endregion
	}
}
