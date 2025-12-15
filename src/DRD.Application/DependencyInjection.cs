// ============================================================================
// Projet:      DRD.Application
// Fichier:     DependencyInjection.cs
// Type:        Configuration
// Classe:      Static Class
// Emplacement: /
// Entité(s):   CdSetService
// Créé le:     2025-12-12
//
// Description
//     Point d’enregistrement central de la couche Application. Cette classe
//     expose l’extension AddApplication() utilisée par DRD.Web afin de
//     configurer les services applicatifs (cas d’usage métier).
//
// Fonctionnalité
//     - Enregistrer les services applicatifs (ex: CdSetService).
//     - Maintenir une séparation claire entre Application et Infrastructure.
//     - Conserver Program.cs léger et conforme Clean Architecture.
//
// Modifications
//     2025-12-15    Enregistrement des services applicatifs (CdSet).
// ============================================================================

using DRD.Application.Common.Interfaces.Services;
using DRD.Application.IServices.SystemTables;
using DRD.Application.Services.SystemTables;
using Microsoft.Extensions.DependencyInjection;

namespace DRD.Application
{
	public static class DependencyInjection
	{
		#region AddApplication
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			// =========================
			// Services applicatifs
			// =========================
			services.AddScoped<ICdSetService, CdSetService>();

			return services;
		}
		#endregion
	}
}
