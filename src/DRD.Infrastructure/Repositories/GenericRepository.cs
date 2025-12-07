// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 GenericRepository.cs
// Type de fichier                Classe C#
// Classe                         GenericRepository
// Emplacement                    Data/Repositories
// Entités concernées             Toutes les entités à clé simple
// Créé le                        2025-12-07
//
// Description
//     Implémentation générique des opérations CRUD standard pour les entités
//     à clé simple du domaine DRD. Sert de base aux repositories spécialisés
//     de la couche Infrastructure.
//
// Fonctionnalité
//     - Fournir les opérations CRUD de base : GetAll, GetById, GetWhere,
//       Add, Update, Remove.
//     - Exposer IQueryable pour des requêtes avancées dans les services.
//     - Utiliser AsNoTracking pour optimiser les lectures.
//     - S'intégrer au UnitOfWork DRD v10.
//     - Journaliser les opérations via ILogger.
//
// Modifications
//     2025-12-07    Version DRD v10 finale. Ajout GetByIdAsync, AsQueryable,
//                   renommage FindAsync → GetWhereAsync, alignement interface v10.
// ============================================================================

using DRD.Application.Common.Interfaces.Repositories;
using DRD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DRD.Infrastructure.Data.Repositories
{
	/// <summary>
	/// Implémentation générique des opérations CRUD standard pour les entités
	/// à clé simple, conformément aux conventions DRD.
	/// </summary>
	/// <typeparam name="T">Type de l'entité.</typeparam>
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		#region Champs protégés

		protected readonly ApplicationDbContext _context;
		protected readonly DbSet<T> _dbSet;
		protected readonly ILogger<GenericRepository<T>> _logger;

		#endregion

		#region Constructeur

		public GenericRepository(
			ApplicationDbContext context,
			ILogger<GenericRepository<T>> logger)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			_dbSet = _context.Set<T>();
		}

		#endregion

		#region Lecture

		/// <inheritdoc />
		public async Task<List<T>> GetAllAsync()
		{
			_logger.LogDebug("Récupération de toutes les entités {Entity}", typeof(T).Name);
			return await _dbSet.AsNoTracking().ToListAsync();
		}

		/// <inheritdoc />
		public async Task<T?> GetByIdAsync(object id)
		{
			_logger.LogDebug("Recherche par clé primaire pour {Entity}", typeof(T).Name);
			return await _dbSet.FindAsync(id);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
		{
			_logger.LogDebug("Filtrage conditionnel sur {Entity}", typeof(T).Name);
			return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
		}

		/// <inheritdoc />
		public IQueryable<T> AsQueryable()
		{
			return _dbSet.AsNoTracking();
		}

		#endregion

		#region Écriture

		/// <inheritdoc />
		public async Task AddAsync(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			_logger.LogInformation("Ajout d'une entité dans {Entity}", typeof(T).Name);
			await _dbSet.AddAsync(entity);
		}

		/// <inheritdoc />
		public Task UpdateAsync(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			_logger.LogInformation("Mise à jour d'une entité dans {Entity}", typeof(T).Name);
			_dbSet.Update(entity);
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task RemoveAsync(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			_logger.LogInformation("Suppression d'une entité dans {Entity}", typeof(T).Name);
			_dbSet.Remove(entity);
			return Task.CompletedTask;
		}

		#endregion
	}
}
