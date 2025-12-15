// ============================================================================
// Projet                         DRD.Application
// Nom du fichier                 CdSetService.cs
// Type de fichier                Classe C#
// Classe                         CdSetService
// Emplacement                    Services/SystemTables
// Entités concernées             CdSet
// Créé le                        2025-12-07
//
// Description
//     Service applicatif responsable de la gestion des codes système (CdSet).
//
// Fonctionnalité
//     - Lecture des CdSet
//     - Création avec audit
//     - Modification avec audit
//     - Désactivation individuelle
//     - Désactivation complète d’une famille (TypeCode)
//     - Réactivation complète d’une famille (TypeCode)
//
// Modifications
//     2025-12-15    Ajout ReactivateFamilyAsync (réactivation logique par TypeCode).
//     2025-12-15    Ajout DeactivateFamilyAsync (désactivation logique par TypeCode).
//     2025-12-13    Ajout Create / Update + audit utilisateur et dates.
// ============================================================================

using DRD.Application.Common.Interfaces;
using DRD.Application.Common.Interfaces.Repositories;
using DRD.Application.Common.Interfaces.Services;
using DRD.Application.IServices.SystemTables;
using DRD.Domain.Entities.GrpSystemTables;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DRD.Application.Services.SystemTables
{
	public class CdSetService : ICdSetService
	{
		#region Champs privés

		private readonly ICdSetRepository _cdSetRepository;
		private readonly ICurrentUserService _currentUserService;
		private readonly ILogger<CdSetService> _logger;
		private readonly IUnitOfWork _unitOfWork;

		#endregion

		#region Constructeur

		public CdSetService(
			ICdSetRepository cdSetRepository,
			IUnitOfWork unitOfWork,
			ICurrentUserService currentUserService,
			ILogger<CdSetService> logger)
		{
			_cdSetRepository = cdSetRepository;
			_unitOfWork = unitOfWork;
			_currentUserService = currentUserService;
			_logger = logger;
		}

		#endregion

		#region Lit CdSet par TypeCode et Code

		public async Task<Dictionary<string, string>> GetCdSetKeyValueListAsync(
			string typeCode,
			string? culture = null)
		{
			var items = await _cdSetRepository.GetByTypeCodeAsync(typeCode);

			var result = new Dictionary<string, string>();

			var cultureToUse = (culture ?? CultureInfo.CurrentUICulture.Name)
				.ToLowerInvariant();

			bool isEnglish = cultureToUse.StartsWith("en");

			foreach (var cd in items.Where(x => x.IsActive))
			{
				var text = isEnglish
					? (cd.DescriptionEn ?? cd.DescriptionFr)
					: cd.DescriptionFr;

				result[cd.Code] = text;
			}

			return result;
		}

		#endregion

		#region Create / Update

		public async Task CreateAsync(CdSet entity)
		{
			entity.CreationDate = DateTime.UtcNow;
			entity.ModificationDate = DateTime.UtcNow;

			entity.CreatedBy = _currentUserService.UserName;
			entity.UpdatedBy = _currentUserService.UserName;

			await _cdSetRepository.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task UpdateAsync(CdSet entity)
		{
			entity.ModificationDate = DateTime.UtcNow;
			entity.UpdatedBy = _currentUserService.UserName;

			await _unitOfWork.SaveChangesAsync();
		}

		#endregion

		#region Désactivation individuelle

		public async Task DeactivateAsync(CdSet entity)
		{
			entity.IsActive = false;
			entity.ModificationDate = DateTime.UtcNow;
			entity.UpdatedBy = _currentUserService.UserName;

			await _unitOfWork.SaveChangesAsync();
		}

		#endregion

		#region Désactivation famille complète (TypeCode)

		public async Task<int> DeactivateFamilyAsync(string typeCode)
		{
			if (string.IsNullOrWhiteSpace(typeCode))
				throw new ArgumentException(nameof(typeCode));

			var items = await _cdSetRepository.GetByTypeCodeAsync(typeCode);

			var activeItems = items.Where(x => x.IsActive).ToList();

			if (!activeItems.Any())
			{
				_logger.LogWarning(
					"[CdSet] Aucune entrée active trouvée pour la famille {TypeCode}",
					typeCode);

				return 0;
			}

			foreach (var cd in activeItems)
			{
				cd.IsActive = false;
				cd.ModificationDate = DateTime.UtcNow;
				cd.UpdatedBy = _currentUserService.UserName;
			}

			await _unitOfWork.SaveChangesAsync();

			_logger.LogInformation(
				"[CdSet] Désactivation de la famille {TypeCode} ({Count} codes)",
				typeCode,
				activeItems.Count);

			return activeItems.Count;
		}

		#endregion

		#region Réactivation famille complète (TypeCode)

		public async Task<int> ReactivateFamilyAsync(string typeCode)
		{
			if (string.IsNullOrWhiteSpace(typeCode))
				throw new ArgumentException(nameof(typeCode));

			var items = await _cdSetRepository.GetByTypeCodeAsync(typeCode);

			var inactiveItems = items.Where(x => !x.IsActive).ToList();

			if (!inactiveItems.Any())
			{
				_logger.LogWarning(
					"[CdSet] Aucune entrée inactive trouvée pour la famille {TypeCode}",
					typeCode);

				return 0;
			}

			foreach (var cd in inactiveItems)
			{
				cd.IsActive = true;
				cd.ModificationDate = DateTime.UtcNow;
				cd.UpdatedBy = _currentUserService.UserName;
			}

			await _unitOfWork.SaveChangesAsync();

			_logger.LogInformation(
				"[CdSet] Réactivation de la famille {TypeCode} ({Count} codes)",
				typeCode,
				inactiveItems.Count);

			return inactiveItems.Count;
		}

		#endregion

		#region Méthodes de lecture (inchangées)

		public async Task<IEnumerable<CdSet>> GetAllAsync()
			=> await _cdSetRepository.GetAllAsync();

		public async Task<IEnumerable<CdSet>> GetByTypeCodeAsync(string typeCode)
			=> await _cdSetRepository.GetByTypeCodeAsync(typeCode);

		public async Task<CdSet?> GetByTypeCodeAndCodeAsync(string typeCode, string code)
			=> await _cdSetRepository.GetByTypeCodeAndCodeAsync(typeCode, code);

		public async Task<bool> ExistsAsync(string typeCode, string code)
			=> await GetByTypeCodeAndCodeAsync(typeCode, code) != null;

		public async Task<string> GetDescriptionAsync(
			string typeCode,
			string code,
			string? culture = null)
		{
			var entity = await GetByTypeCodeAndCodeAsync(typeCode, code);
			if (entity == null) return code;

			var cultureToUse =
				(culture ?? CultureInfo.CurrentUICulture.Name).ToLowerInvariant();

			bool isEnglish = cultureToUse.StartsWith("en");

			return isEnglish
				? (entity.DescriptionEn ?? entity.DescriptionFr)
				: entity.DescriptionFr;
		}

		#endregion
	}
}
