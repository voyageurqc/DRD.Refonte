// ============================================================================
// Projet                         DRD.Application
// Nom du fichier                 IGenericRepository.cs
// Type de fichier                Interface
// Classe                         IGenericRepository
// Emplacement                    Common/Interfaces/Repositories
// Entités concernées             Toutes les entités à clé simple
// Créé le                        2025-12-07
//
// Description
//     Interface générique fournissant les opérations CRUD standard pour
//     les entités à clé simple du domaine DRD. Conçue pour servir de base
//     aux repositories spécialisés dans la couche Infrastructure.
//
// Fonctionnalité
//     - Récupérer toutes les entités d'un type donné (GetAllAsync).
//     - Récupérer une entité par clé primaire simple (GetByIdAsync).
//     - Effectuer un filtrage conditionnel (GetWhereAsync).
//     - Fournir un IQueryable pour requêtes avancées.
//     - Ajouter des entités (AddAsync).
//     - Mettre à jour des entités (UpdateAsync).
//     - Supprimer des entités physiquement (RemoveAsync).
//
// Modifications
//     2025-12-07    Version DRD v10 finale. Renommage FindAsync → GetWhereAsync.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DRD.Application.Common.Interfaces.Repositories
{
	/// <summary>
	/// Définit les opérations CRUD standard pour les entités à clé simple.
	/// Fournit un socle commun aux repositories spécialisés dans la couche Infrastructure.
	/// </summary>
	/// <typeparam name="T">Type de l'entité du domaine.</typeparam>
	public interface IGenericRepository<T> where T : class
	{
		#region Lecture

		/// <summary>
		/// Récupère toutes les entités sans tracking.
		/// </summary>
		Task<List<T>> GetAllAsync();

		/// <summary>
		/// Recherche une entité par clé primaire (simple).
		/// </summary>
		/// <param name="id">Clé primaire de l'entité.</param>
		Task<T?> GetByIdAsync(object id);

		/// <summary>
		/// Applique une condition de filtrage et retourne les résultats (sans tracking).
		/// </summary>
		/// <param name="predicate">Expression LINQ représentant la condition Where.</param>
		Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);

		/// <summary>
		/// Retourne un IQueryable permettant d'effectuer des requêtes avancées.
		/// </summary>
		IQueryable<T> AsQueryable();

		#endregion

		#region Écriture

		/// <summary>
		/// Ajoute une nouvelle entité au contexte.
		/// </summary>
		Task AddAsync(T entity);

		/// <summary>
		/// Marque une entité comme modifiée.
		/// </summary>
		Task UpdateAsync(T entity);

		/// <summary>
		/// Supprime physiquement une entité.
		/// </summary>
		Task RemoveAsync(T entity);

		#endregion
	}
}
