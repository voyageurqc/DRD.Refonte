// ============================================================================
// Projet:      DRD.Application
// Fichier:     DependencyInjection.cs
// Type:        Configuration
// Classe:      Static Class
// Emplacement: /
// Entité(s):   (Aucune pour l’instant)
// Créé le:     2025-12-12
//
// Description
//     Point d’enregistrement central de la couche Application. Cette classe
//     expose l’extension AddApplication() utilisée par DRD.Web afin de
//     configurer les services applicatifs. Dans l'état actuel du projet,
//     seules les interfaces existent dans Application ; les implémentations
//     concrètes résident dans Infrastructure.
//
// Fonctionnalité
//     - Fournir une méthode DI propre et évolutive.
//     - Conserver Program.cs léger et conforme Clean Architecture.
//     - Préparer l’ajout ultérieur de services applicatifs (Validation,
//       Médiation, Mapping, etc.).
//
// Modifications
//     2025-12-12    Création initiale DRD v10.
// ============================================================================

using Microsoft.Extensions.DependencyInjection;

namespace DRD.Application
{
	/// <summary>
	/// Fournit les méthodes d’enregistrement DI de la couche DRD.Application.
	/// </summary>
	public static class DependencyInjection
	{
		#region AddApplication
		/// <summary>
		/// Enregistre les composants applicatifs.  
		/// Actuellement vide car toutes les implémentations se trouvent
		/// dans DRD.Infrastructure.
		/// </summary>
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			// AUCUN service à enregistrer ici pour l’instant.
			// Conformité Clean Architecture :
			// - Interfaces dans Application
			// - Implémentations dans Infrastructure

			return services;
		}
		#endregion
	}
}
