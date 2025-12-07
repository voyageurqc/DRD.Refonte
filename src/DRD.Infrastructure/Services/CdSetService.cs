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
//     Fournit les opérations de consultation, de validation, et d'affichage
//     bilingue utilisées par les écrans, les composants de sélection et les
//     fonctionnalités internes du projet DRD.
//
// Fonctionnalité
//     - Obtenir la liste de tous les CdSet.
//     - Obtenir tous les CdSet d’un TypeCode donné.
//     - Obtenir un CdSet précis via TypeCode + Code.
//     - Vérifier l’existence d’un CdSet.
//     - Extraire la description FR/EN avec fallback automatique.
//     - Construire un dictionnaire clé-valeur pour les composants UI.
//     - (À venir) Construire une liste SelectListItem (méthode commentée).
//
// Modifications
//     2025-12-07    Version DRD v10 finale. Migration complète depuis v9 avec
//                   support bilingue, null-checks, logs, et alignement Domain v10.
// ============================================================================

using Microsoft.Extensions.Logging;
using DRD.Application.Common.Interfaces.Repositories;
using DRD.Application.IServices.SystemTables;
using DRD.Domain.Entities.GrpSystemTables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DRD.Application.Services.SystemTables
{
	/// <summary>
	/// Implémentation du service de gestion des codes système (CdSet).
	/// </summary>
	public class CdSetService : ICdSetService
	{
		#region Champs privés

		private readonly ICdSetRepository _cdSetRepository;
		private readonly ILogger<CdSetService> _logger;

		#endregion

		#region Constructeur

		public CdSetService(
			ICdSetRepository cdSetRepository,
			ILogger<CdSetService> logger)
		{
			_cdSetRepository = cdSetRepository ?? throw new ArgumentNullException(nameof(cdSetRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		#endregion

		#region Méthodes principales

		/// <inheritdoc />
		public async Task<IEnumerable<CdSet>> GetAllAsync()
		{
			_logger.LogDebug("Récupération de tous les CdSet (GetAllAsync)");
			return await _cdSetRepository.GetAllAsync();
		}

		/// <inheritdoc />
		public async Task<IEnumerable<CdSet>> GetByTypeCodeAsync(string typeCode)
		{
			if (string.IsNullOrWhiteSpace(typeCode))
				throw new ArgumentNullException(nameof(typeCode));

			_logger.LogDebug("Récupération des CdSet pour TypeCode={TypeCode}", typeCode);
			return await _cdSetRepository.GetByTypeCodeAsync(typeCode);
		}

		/// <inheritdoc />
		public async Task<CdSet?> GetByTypeCodeAndCodeAsync(string typeCode, string code)
		{
			if (string.IsNullOrWhiteSpace(typeCode))
				throw new ArgumentNullException(nameof(typeCode));
			if (string.IsNullOrWhiteSpace(code))
				throw new ArgumentNullException(nameof(code));

			_logger.LogDebug("Récupération d'un CdSet pour TypeCode={TypeCode}, Code={Code}", typeCode, code);
			return await _cdSetRepository.GetByTypeCodeAndCodeAsync(typeCode, code);
		}

		/// <inheritdoc />
		public async Task<bool> ExistsAsync(string typeCode, string code)
		{
			_logger.LogDebug("Vérification existence CdSet : {TypeCode}-{Code}", typeCode, code);
			var entity = await GetByTypeCodeAndCodeAsync(typeCode, code);
			return entity != null;
		}

		/// <inheritdoc />
		public async Task<string> GetDescriptionAsync(string typeCode, string code, string? culture = null)
		{
			if (string.IsNullOrWhiteSpace(typeCode))
				throw new ArgumentNullException(nameof(typeCode));
			if (string.IsNullOrWhiteSpace(code))
				throw new ArgumentNullException(nameof(code));

			_logger.LogDebug("Extraction description pour CdSet {TypeCode}-{Code}", typeCode, code);

			var entity = await GetByTypeCodeAndCodeAsync(typeCode, code);
			if (entity == null)
				return $"{code}";

			// Détection culturelle : FR par défaut, EN si demandé
			var cultureToUse = (culture ?? CultureInfo.CurrentUICulture.Name)
				.ToLowerInvariant();

			bool isEnglish = cultureToUse.StartsWith("en");

			return isEnglish
				? (entity.DescriptionEn ?? entity.DescriptionFr)
				: entity.DescriptionFr;
		}

		/// <inheritdoc />
		public async Task<Dictionary<string, string>> GetCdSetKeyValueListAsync(
			string typeCode,
			string? culture = null)
		{
			_logger.LogDebug("Construction dictionnaire clé-valeur CdSet pour {TypeCode}", typeCode);

			var items = await GetByTypeCodeAsync(typeCode);

			var dict = new Dictionary<string, string>();

			var cultureToUse = (culture ?? CultureInfo.CurrentUICulture.Name)
				.ToLowerInvariant();
			bool isEnglish = cultureToUse.StartsWith("en");

			foreach (var cd in items)
			{
				string text = isEnglish
					? (cd.DescriptionEn ?? cd.DescriptionFr)
					: cd.DescriptionFr;

				dict[cd.Code] = text;
			}

			return dict;
		}

		#endregion

		#region Méthodes futures (UI SelectListItem)

		// Cette méthode sera réactivée lorsque tu intégreras les dropdowns CdSet.
		/*
        public async Task<List<SelectListItem>> GetCdSetSelectListAsync(
            string typeCode,
            string? selectedValue = null,
            string? culture = null)
        {
            // À implémenter plus tard selon besoins UI.
        }
        */

		#endregion
	}
}
