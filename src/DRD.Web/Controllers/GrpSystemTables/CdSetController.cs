// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetController.cs
// Type de fichier                Contrôleur MVC
// Classe                         CdSetController
// Emplacement                    Controllers/GrpSystemTables
// Entités concernées             CdSet, CdSetIndexVM, CdSetCreateVM, CdSetEditVM
// Créé le                        2025-12-07
//
// Description
//     Contrôleur MVC du module CdSet. Gère l’affichage de la liste via
//     DataTables, la création, l’édition, l’affichage des détails et la
//     suppression (via la modale universelle). Utilise Serilog, Toastr,
//     ressources .resx et le pattern UnitOfWork.
//
// Fonctionnalité
//     - Index : filtre par famille et recherche textuelle.
//     - Create : création d’un code de référence.
//     - Edit : modification (réactivation possible).
//     - Details : affichage en lecture seule.
//     - Delete : suppression via modale universelle.
//     - Gestion clé composite (TypeCode + Code).
//     - Utilisation IUnitOfWork + Serilog + Toastr.
//
// Modifications
//     2025-12-07    Version initiale DRD v10.
// ============================================================================

using DRD.Application.Common.Interfaces;
using DRD.Domain.Entities.GrpSystemTables;
using DRD.Resources;
using DRD.Web.Models.GrpSystemTables.CdSetVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Serilog;

namespace DRD.Web.Controllers.GrpSystemTables
{
	public class CdSetController : Controller
	{
		// --------------------------------------------------------------------
		// REGION : Services
		// --------------------------------------------------------------------
		#region Services

		private readonly IUnitOfWork _unitOfWork;
		private readonly IStringLocalizer<ToastrMessages> _toastr;
		private readonly IStringLocalizer<SystemTables> _systemLocalizer;
		private readonly ILogger _logger;

		public CdSetController(
			IUnitOfWork unitOfWork,
			IStringLocalizer<ToastrMessages> toastr,
			IStringLocalizer<SystemTables> systemLocalizer)
		{
			_unitOfWork = unitOfWork;
			_toastr = toastr;
			_systemLocalizer = systemLocalizer;

			// Serilog logger instancié pour cette classe
			_logger = Log.ForContext<CdSetController>();
		}

		#endregion

		// --------------------------------------------------------------------
		// REGION : Index
		// --------------------------------------------------------------------
		#region Index

		/// <summary>
		/// Affiche la liste des CdSet avec filtres (Famille + recherche).
		/// </summary>
		public async Task<IActionResult> Index(string? typeCode, string? search)
		{
			_logger.Information("CdSet - Index - Filtre TypeCode={TypeCode}, Search={Search}", typeCode, search);

			var all = await _unitOfWork.CdSetRepository.GetAllAsync();

			// Extraction unique de toutes les familles (TypeCodes)
			var allTypeCodes = all
				.Select(x => x.TypeCode)
				.Distinct()
				.OrderBy(x => x)
				.ToList();

			// Filtrage
			var filtered = all.AsQueryable();

			if (!string.IsNullOrWhiteSpace(typeCode))
			{
				filtered = filtered.Where(x => x.TypeCode == typeCode);
			}

			if (!string.IsNullOrWhiteSpace(search))
			{
				filtered = filtered.Where(x =>
					x.TypeCode.Contains(search, StringComparison.OrdinalIgnoreCase)
					|| x.Code.Contains(search, StringComparison.OrdinalIgnoreCase)
					|| x.DescriptionFr.Contains(search, StringComparison.OrdinalIgnoreCase)
					|| (x.DescriptionEn ?? "").Contains(search, StringComparison.OrdinalIgnoreCase)
				);
			}

			// Projection en RowVM
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

		// --------------------------------------------------------------------
		// REGION : CREATE
		// --------------------------------------------------------------------
		#region Create

		[HttpGet]
		public IActionResult Create(string? returnUrl)
		{
			var vm = new CdSetCreateVM
			{
				ReturnUrl = returnUrl ?? Url.Action(nameof(Index))
			};

			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CdSetCreateVM vm)
		{
			if (!ModelState.IsValid)
			{
				_logger.Warning("CdSet - Create - Validation échouée");
				TempData["ToastrError"] = _toastr["Error_InvalidForm"];
				return View(vm);
			}

			// Vérifier duplication
			var exists = await _unitOfWork.CdSetRepository.ExistsAsync(vm.TypeCodeFinal, vm.Code);
			if (exists)
			{
				ModelState.AddModelError("", _systemLocalizer["CdSet_Error_Duplicate"]);
				TempData["ToastrError"] = _systemLocalizer["CdSet_Error_Duplicate"];
				return View(vm);
			}

			var entity = vm.ToEntity();

			await _unitOfWork.CdSetRepository.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			TempData["ToastrSuccess"] = _toastr["Success_EntityCreated", entity.DescriptionFr];

			_logger.Information("CdSet créé : {TypeCode}|{Code}", entity.TypeCode, entity.Code);

			return Redirect(vm.ReturnUrl ?? Url.Action(nameof(Index))!);
		}

		#endregion

		// --------------------------------------------------------------------
		// REGION : EDIT
		// --------------------------------------------------------------------
		#region Edit

		[HttpGet]
		public async Task<IActionResult> Edit(string id, string key2, string? returnUrl)
		{
			var entity = await _unitOfWork.CdSetRepository.GetAsync(id, key2);

			if (entity == null)
			{
				TempData["ToastrError"] = _toastr["Error_NotFound"];
				return RedirectToAction(nameof(Index));
			}

			var vm = new CdSetEditVM
			{
				TypeCode = entity.TypeCode,
				Code = entity.Code,
				DescriptionFr = entity.DescriptionFr,
				DescriptionEn = entity.DescriptionEn,
				IsActive = entity.IsActive,
				ReturnUrl = returnUrl ?? Url.Action(nameof(Index))
			};

			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(CdSetEditVM vm)
		{
			if (!ModelState.IsValid)
			{
				TempData["ToastrError"] = _toastr["Error_InvalidForm"];
				return View(vm);
			}

			var entity = await _unitOfWork.CdSetRepository.GetAsync(vm.TypeCode, vm.Code);
			if (entity == null)
			{
				TempData["ToastrError"] = _toastr["Error_NotFound"];
				return RedirectToAction(nameof(Index));
			}

			// Mise à jour
			entity.ModifyFields(vm.DescriptionFr, vm.DescriptionEn, vm.IsActive);

			await _unitOfWork.CdSetRepository.UpdateAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			TempData["ToastrSuccess"] = _toastr["Success_EntityUpdated", entity.DescriptionFr];

			_logger.Information("CdSet modifié : {TypeCode}|{Code}", entity.TypeCode, entity.Code);

			return Redirect(vm.ReturnUrl ?? Url.Action(nameof(Index))!);
		}

		#endregion

		// --------------------------------------------------------------------
		// REGION : DETAILS
		// --------------------------------------------------------------------
		#region Details

		[HttpGet]
		public async Task<IActionResult> Details(string id, string key2, string? returnUrl)
		{
			var entity = await _unitOfWork.CdSetRepository.GetAsync(id, key2);

			if (entity == null)
			{
				TempData["ToastrError"] = _toastr["Error_NotFound"];
				return RedirectToAction(nameof(Index));
			}

			var vm = new CdSetDetailsVM
			{
				TypeCode = entity.TypeCode,
				Code = entity.Code,
				DescriptionFr = entity.DescriptionFr,
				DescriptionEn = entity.DescriptionEn,
				IsActive = entity.IsActive,
				CreationDate = entity.CreationDate,
				ModificationDate = entity.ModificationDate,
				CreatedBy = entity.CreatedBy,
				UpdatedBy = entity.UpdatedBy,
				ReturnUrl = returnUrl ?? Url.Action(nameof(Index))
			};

			return View(vm);
		}

		#endregion

		// --------------------------------------------------------------------
		// REGION : DELETE (modal universelle)
		// --------------------------------------------------------------------
		#region Delete

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(string entityId)
		{
			if (string.IsNullOrWhiteSpace(entityId))
			{
				TempData["ToastrError"] = _toastr["Error_NotFound"];
				return RedirectToAction(nameof(Index));
			}

			var parts = entityId.Split('|');
			if (parts.Length < 2)
			{
				TempData["ToastrError"] = _toastr["Error_NotFound"];
				return RedirectToAction(nameof(Index));
			}

			string typeCode = parts[0];
			string code = parts[1];

			var entity = await _unitOfWork.CdSetRepository.GetAsync(typeCode, code);

			if (entity == null)
			{
				TempData["ToastrError"] = _toastr["Error_NotFound"];
				return RedirectToAction(nameof(Index));
			}

			await _unitOfWork.CdSetRepository.RemoveAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			TempData["ToastrSuccess"] = _toastr["Success_EntityDeleted", entity.DescriptionFr];

			_logger.Information("CdSet supprimé : {TypeCode}|{Code}", typeCode, code);

			return RedirectToAction(nameof(Index));
		}

		#endregion
	}
}
