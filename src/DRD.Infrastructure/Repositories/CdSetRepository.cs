// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 CdSetRepository.cs
// Type de fichier                Classe C#
// Classe                         CdSetRepository
// Emplacement                    Data/Repositories/GrpSystemTables
// Entités concernées             CdSet
// Créé le                        2025-12-07
//
// Description
//     Repository spécialisé pour l'entité CdSet, fournissant les opérations
//     basées sur la clé composite TypeCode + Code. Étend GenericRepository
//     pour inclure les méthodes propres au module CdSet.
//
// Fonctionnalité
//     - Récupération de tous les CdSet selon un TypeCode donné.
//     - Récupération d'un CdSet précis via (TypeCode + Code).
//     - Suppression physique d'un CdSet via sa clé composite.
//     - Journalisation Serilog via ILogger.
//
// Modifications
//     2025-12-07    Migration complète vers DRD v10. Correction des types,
//                   ajout null-checks, logging, et conformité interface.
// ============================================================================

using DRD.Application.Common.Interfaces.Repositories;
using DRD.Domain.Entities.GrpSystemTables;
using DRD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DRD.Infrastructure.Data.Repositories.GrpSystemTables
{
	/// <summary>
	/// Repository spécialisé pour la gestion des CdSet.
	/// Fournit les opérations additionnelles liées à la clé composite.
	/// </summary>
	public class CdSetRepository : GenericRepository<CdSet>, ICdSetRepository
	{
		#region Constructeur

		public CdSetRepository(
			ApplicationDbContext context,
			ILogger<CdSetRepository> logger)
			: base(context, logger)
		{
		}

		#endregion

		#region Méthodes spécialisées

		/// <inheritdoc />
		public async Task<IEnumerable<CdSet>> GetByTypeCodeAsync(string typeCode)
		{
			if (string.IsNullOrWhiteSpace(typeCode))
				throw new ArgumentNullException(nameof(typeCode));

			_logger.LogDebug("Récupération des CdSet pour TypeCode={TypeCode}", typeCode);

			return await _dbSet
				.AsNoTracking()
				.Where(e => e.TypeCode == typeCode && e.IsActive)
				.ToListAsync();
		}

		/// <inheritdoc />
		public async Task<CdSet?> GetByTypeCodeAndCodeAsync(string typeCode, string code)
		{
			if (string.IsNullOrWhiteSpace(typeCode))
				throw new ArgumentNullException(nameof(typeCode));
			if (string.IsNullOrWhiteSpace(code))
				throw new ArgumentNullException(nameof(code));

			_logger.LogDebug(
				"Récupération d'un CdSet pour TypeCode={TypeCode}, Code={Code}",
				typeCode, code);

			return await _dbSet.FindAsync(typeCode, code);
		}

		/// <inheritdoc />
		public async Task DeleteCdSetAsync(string typeCode, string code)
		{
			if (string.IsNullOrWhiteSpace(typeCode))
				throw new ArgumentNullException(nameof(typeCode));
			if (string.IsNullOrWhiteSpace(code))
				throw new ArgumentNullException(nameof(code));

			_logger.LogWarning(
				"Suppression d'un CdSet via TypeCode={TypeCode}, Code={Code}",
				typeCode, code);

			var entity = await _dbSet.FindAsync(typeCode, code);

			if (entity == null)
			{
				_logger.LogWarning(
					"Tentative de suppression d'un CdSet introuvable : {TypeCode}-{Code}",
					typeCode, code);
				return;
			}

			_dbSet.Remove(entity);
		}

		#endregion
	}
}
