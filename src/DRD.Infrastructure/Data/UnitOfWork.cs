// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 UnitOfWork.cs
// Type de fichier                Classe C#
// Classe                         UnitOfWork
// Emplacement                    Data
// Entités concernées             CdSet
// Créé le                        2025-12-07
//
// Description
//     Implémentation du pattern Unit of Work. Coordonne l'accès aux repositories
//     et encapsule le DbContext pour assurer l'atomicité des opérations.
//     Fournit les instances de repositories et la méthode SaveChangesAsync.
//
// Fonctionnalité
//     - Centraliser les repositories utilisés par la couche Application.
//     - Fournir une unique méthode SaveChangesAsync pour valider les modifications.
//     - Instancier les repositories à la demande (lazy instantiation).
//     - Gérer la durée de vie du DbContext via Dispose().
//
// Modifications
//     2025-12-07    Version DRD v10 minimaliste incluant uniquement CdSetRepository.
//                   Migration et modernisation à partir du UnitOfWork v9.
// ============================================================================

using DRD.Application.Common.Interfaces;
using DRD.Application.Common.Interfaces.Repositories;
using DRD.Infrastructure.Repositories.SystemTables;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DRD.Infrastructure.Data
{
	/// <summary>
	/// Implémentation du pattern Unit of Work permettant d'orchestrer
	/// les accès aux repositories et la gestion des transactions.
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		#region Champs privés

		private readonly ApplicationDbContext _context;
		private readonly ILoggerFactory _loggerFactory;

		private ICdSetRepository? _cdSetRepository;

		private bool _disposed;

		#endregion

		#region Constructeur

		/// <summary>
		/// Initialise une nouvelle instance de UnitOfWork.
		/// </summary>
		public UnitOfWork(
			ApplicationDbContext context,
			ILoggerFactory loggerFactory)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
		}

		#endregion

		#region Repositories exposés

		/// <inheritdoc />
		public ICdSetRepository CdSetRepository =>
			_cdSetRepository ??= new CdSetRepository(
				_context,
				_loggerFactory.CreateLogger<CdSetRepository>());

		#endregion

		#region Méthodes publiques

		/// <inheritdoc />
		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}

		#endregion

		#region Dispose Pattern

		/// <summary>
		/// Libère les ressources utilisées par le context EF Core.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Libère les ressources managées et non managées.
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}

				_disposed = true;
			}
		}

		#endregion
	}
}
