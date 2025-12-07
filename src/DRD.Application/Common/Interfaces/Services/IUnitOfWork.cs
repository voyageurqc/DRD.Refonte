// ============================================================================
// Projet                         DRD.Application
// Nom du fichier                 IUnitOfWork.cs
// Type de fichier                Interface
// Classe                         IUnitOfWork
// Emplacement                    Common/Interfaces
// Entités concernées             CdSet
// Créé le                        2025-12-07
//
// Description
//     Interface représentant le pattern Unit of Work dans la couche Application.
//     Coordonne l’accès aux repositories et garantit l’atomicité des opérations
//     de persistance via SaveChangesAsync.
//
// Fonctionnalité
//     - Exposer le repository CdSet via une propriété typée.
//     - Fournir la méthode SaveChangesAsync pour valider les opérations.
//     - Servir de contrat pour l’implémentation dans DRD.Infrastructure.
//
// Modifications
//     2025-12-07    Version DRD v10 minimaliste. Migration à partir de v9.
//                   Réduction aux éléments requis pour le module CdSet.
// ============================================================================

using DRD.Application.Common.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace DRD.Application.Common.Interfaces
{
	/// <summary>
	/// Définit le contrat du pattern Unit of Work permettant de regrouper les 
	/// opérations de persistance et de coordonner les repositories.
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		#region Repositories

		/// <summary>
		/// Repository dédié à l’entité CdSet.
		/// </summary>
		ICdSetRepository CdSetRepository { get; }

		#endregion

		#region Méthodes

		/// <summary>
		/// Valide toutes les opérations effectuées sur le DbContext sous-jacent.
		/// </summary>
		/// <returns>Nombre d’entrées affectées en base de données.</returns>
		Task<int> SaveChangesAsync();

		#endregion
	}
}
