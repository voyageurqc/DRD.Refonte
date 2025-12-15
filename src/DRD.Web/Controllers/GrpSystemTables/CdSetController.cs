// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetController.cs
// Type                           Contrôleur MVC
// Classe                         CdSetController
// Emplacement                    Controllers/GrpSystemTables
// Entités concernées             CdSet, CdSetIndexVM, CdSetCreateVM, CdSetEditVM
// Créé le                        2025-12-07
//
// Description
//     Contrôleur MVC du module CdSet. Gère l’affichage de la liste via
//     DataTables, la création, l’édition, l’affichage des détails et la
//     suppression via la modale universelle. Utilise Serilog, Toastr,
//     ressources .resx fortement typées et le pattern UnitOfWork.
//
// Fonctionnalité
//     - Index : filtre par famille et recherche textuelle
//     - Create : création d’un code paramétrique
//     - Edit : modification (réactivation individuelle)
//     - Details : affichage en lecture seule
//     - Deactivate : désactivation individuelle
//     - DeactivateFamily : désactivation logique d’une famille complète
//     - ReactivateFamily : réactivation logique d’une famille complète
//     - Delete : suppression physique (cas exceptionnel)
//     - Gestion clé composite (TypeCode + Code)
//     - Utilisation ICdSetService + Serilog + Toastr
//
// Modifications
//     2025-12-15    Ajout ReactivateFamilyConfirmed (réactivation par TypeCode)
//     2025-12-15    Ajout DeactivateFamilyConfirmed (désactivation par TypeCode)
//     2025-12-14    Ajustement Create/Edit pour audit via CdSetService
//     2025-12-14    Ajout action GetMetadata (AJAX, PartialView métadonnées)
// ============================================================================

using DRD.Application.Common.Interfaces;
using DRD.Application.IServices.SystemTables;
using DRD.Application.Popup.Mappers;
using DRD.Resources.MessagesMetier.CdSet;
using DRD.Resources.ToastrMessages;
using DRD.Web.Models.GrpSystemTables.CdSetVM;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DRD.Web.Controllers.GrpSystemTables
{
	public class CdSetController : Controller
	{
		#region Services

		private readonly IUnitOfWork _unitOfWork;
		private readonly ICdSetService _cdSetService;
		private readonly Serilog.ILogger _logger;

		public CdSetController(
			IUnitOfWork unitOfWork,
			ICdSetService cdSetService)
		{
			_unitOfWork = unitOfWork;
			_cdSetService = cdSetService;
			_logger = Log.ForContext<CdSetController>();
		}

		#endregion


		#region Index

		public async Task<IActionResult> Index(string? typeCode, string? search)
		{
			_logger.Information(
				"CdSet - Index - Filtre TypeCode={TypeCode}, Search={Search}",
				typeCode,
				search);

			var all = await _unitOfWork.CdSetRepository.GetAllAsync();

			var allTypeCodes = all
				.Select(x => x.TypeCode)
				.Distinct()
				.OrderBy(x => x)
				.ToList();

			var filtered = all.AsQueryable();

			if (!string.IsNullOrWhiteSpace(typeCode))
				filtered = filtered.Where(x => x.TypeCode == typeCode);

			if (!string.IsNullOrWhiteSpace(search))
			{
				filtered = filtered.Where(x =>
					x.TypeCode.Contains(search, StringComparison.OrdinalIgnoreCase)
					|| x.Code.Contains(search, StringComparison.OrdinalIgnoreCase)
					|| x.DescriptionFr.Contains(search, StringComparison.OrdinalIgnoreCase)
					|| (x.DescriptionEn ?? "").Contains(search, StringComparison.OrdinalIgnoreCase)
				);
			}

			var rows = filtered
				.OrderBy(x => x.TypeCode)
				.ThenBy(x => x.Code)
				.Select(x => new CdSetRowVM
				{
					TypeCode = x.TypeCode,
					Code = x.Code,
					DescriptionFr = x.DescriptionFr,
					DescriptionEn = x.DescriptionEn,
					IsActive = x.IsActive
				})
				.ToList();

			var vm = new CdSetIndexVM
			{
				CdSets = rows,
				AvailableTypeCodes = allTypeCodes,
				SelectedTypeCode = typeCode,
				SearchQuery = search,
				CurrentUrl = Request.Path + Request.QueryString,
				ReferrerUrl = Request.Headers["Referer"].ToString()
			};

			return View(vm);
		}

		#endregion


		#region Deactivate (individuel)

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeactivateConfirmed(
			string id,
			string key2,
			string? returnUrl = null)
		{
			_logger.Information(
				"CdSet DeactivateConfirmed {TypeCode}|{Code}",
				id,
				key2);

			var entity = await _cdSetService
				.GetByTypeCodeAndCodeAsync(id, key2);

			if (entity == null)
			{
				TempData["ToastrError"] = string.Format(
					CdSetToastr.CdSet_NotFound,
					id,
					key2);

				return Redirect(returnUrl ?? Url.Action(nameof(Index))!);
			}

			await _cdSetService.DeactivateAsync(entity);

			TempData["ToastrSuccess"] = string.Format(
				CdSetToastr.Success_EntityDeactivate,
				entity.TypeCode,
				entity.Code);

			return Redirect(returnUrl ?? Url.Action(nameof(Index))!);
		}

		#endregion


		#region DeactivateFamily (famille complète)

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeactivateFamilyConfirmed(
			string typeCode,
			string? returnUrl = null)
		{
			_logger.Information(
				"CdSet DeactivateFamilyConfirmed {TypeCode}",
				typeCode);

			int affectedCount;

			try
			{
				affectedCount = await _cdSetService
					.DeactivateFamilyAsync(typeCode);
			}
			catch (Exception ex)
			{
				_logger.Error(
					ex,
					"Erreur désactivation famille CdSet {TypeCode}",
					typeCode);

				TempData["ToastrError"] = string.Format(
					CdSetToastr.DeactivateFamily_Error,
					typeCode);

				return Redirect(returnUrl ?? Url.Action(nameof(Index))!);
			}

			if (affectedCount == 0)
			{
				TempData["ToastrError"] = string.Format(
					CdSetToastr.DeactivateFamily_NotFound,
					typeCode);
			}
			else
			{
				TempData["ToastrSuccess"] = string.Format(
					CdSetToastr.DeactivateFamily_Success,
					typeCode);
			}

			return Redirect(returnUrl ?? Url.Action(nameof(Index))!);
		}

		#endregion


		#region ReactivateFamily (famille complète)

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ReactivateFamilyConfirmed(
			string typeCode,
			string? returnUrl = null)
		{
			_logger.Information(
				"CdSet ReactivateFamilyConfirmed {TypeCode}",
				typeCode);

			int affectedCount;

			try
			{
				affectedCount = await _cdSetService
					.ReactivateFamilyAsync(typeCode);
			}
			catch (Exception ex)
			{
				_logger.Error(
					ex,
					"Erreur réactivation famille CdSet {TypeCode}",
					typeCode);

				TempData["ToastrError"] = string.Format(
					CdSetToastr.ReactivateFamily_Error,
					typeCode);

				return Redirect(returnUrl ?? Url.Action(nameof(Index))!);
			}

			if (affectedCount == 0)
			{
				TempData["ToastrError"] = string.Format(
					CdSetToastr.ReactivateFamily_NotFound,
					typeCode);
			}
			else
			{
				TempData["ToastrSuccess"] = string.Format(
					CdSetToastr.ReactivateFamily_Success,
					typeCode);
			}

			return Redirect(returnUrl ?? Url.Action(nameof(Index))!);
		}

		#endregion


		#region Delete (physique)

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(
			string id,
			string key2,
			string? returnUrl = null)
		{
			var entity = await _unitOfWork.CdSetRepository
				.GetByTypeCodeAndCodeAsync(id, key2);

			if (entity == null)
			{
				TempData["ToastrError"] = string.Format(
					CdSetToastr.CdSet_NotFound,
					id,
					key2);

				return Redirect(returnUrl ?? Url.Action(nameof(Index))!);
			}

			await _unitOfWork.CdSetRepository.RemoveAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			TempData["ToastrSuccess"] = string.Format(
				CdSetToastr.Success_EntityDeleted,
				entity.TypeCode,
				entity.Code);

			_logger.Information(
				"CdSet supprimé : {TypeCode}|{Code}",
				id,
				key2);

			return Redirect(returnUrl ?? Url.Action(nameof(Index))!);
		}

		#endregion


		#region Metadata

		[HttpGet]
		public async Task<IActionResult> GetMetadata(string entityId)
		{
			if (string.IsNullOrWhiteSpace(entityId))
				return BadRequest();

			var parts = entityId.Split('|');
			if (parts.Length != 2)
				return BadRequest();

			var entity = await _unitOfWork.CdSetRepository
				.GetByTypeCodeAndCodeAsync(parts[0], parts[1]);

			if (entity == null)
				return NotFound();

			var dto = entity.ToMetadataDto();

			return PartialView("_SystemMetadataPartial", dto);
		}

		#endregion
	}
}
