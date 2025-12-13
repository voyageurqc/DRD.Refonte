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
//     - Edit : modification (réactivation)
//     - Details : affichage en lecture seule
//     - Delete : suppression via modale universelle
//     - Gestion clé composite (TypeCode + Code)
//     - Utilisation IUnitOfWork + Serilog + Toastr
//
// Modifications
//     2025-12-12    DRD v10 : correctif ToastrSuccess (finalType/finalCode)
//     2025-12-11    DRD v10 : commentaires XML, familles Create GET, logs.
//     2025-12-09    Harmonisation DRD v10 : régions & en-tête.
//     2025-12-08    Migration Toastr vers ressources strongly typed.
//     2025-12-07    Version initiale.
// ============================================================================

using DRD.Application.Common.Interfaces;
using DRD.Resources.MessagesMetier.CdSet;
using DRD.Resources.ToastrMessages;
using DRD.Web.Models.GrpSystemTables.CdSetVM;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DRD.Web.Controllers.GrpSystemTables
{
	public class CdSetController : Controller
	{
		// =====================================================================
		// DRD – Services
		// =====================================================================
		#region Services

		private readonly IUnitOfWork _unitOfWork;
		private readonly Serilog.ILogger _logger;

		public CdSetController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_logger = Log.ForContext<CdSetController>();
		}

		#endregion


		// =====================================================================
		// DRD – Index
		// =====================================================================
		#region Index

		public async Task<IActionResult> Index(string? typeCode, string? search)
		{
			_logger.Information("CdSet - Index - Filtre TypeCode={TypeCode}, Search={Search}", typeCode, search);

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


		// =====================================================================
		// DRD – Create
		// =====================================================================
		#region Create

		[HttpGet]
		public async Task<IActionResult> Create(string? returnUrl)
		{
			var vm = new CdSetCreateVM
			{
				ReturnUrl = returnUrl ?? Url.Action(nameof(Index))
			};

			var all = await _unitOfWork.CdSetRepository.GetAllAsync();

			var distinctFamilies = all
				.Select(x => x.TypeCode)
				.Distinct()
				.OrderBy(x => x)
				.ToList();

			vm.AvailableFamilies = new List<string> { CdSetCreateVM.NEW_OPTION }
				.Concat(distinctFamilies);

			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CdSetCreateVM vm)
		{
			// Détection du bouton Save & Continue
			bool saveAndContinue = Request.Form.ContainsKey("continue");

			// Toujours recharger les familles
			var all = await _unitOfWork.CdSetRepository.GetAllAsync();
			vm.AvailableFamilies = new List<string> { CdSetCreateVM.NEW_OPTION }
				.Concat(all.Select(x => x.TypeCode).Distinct().OrderBy(x => x));

			if (!ModelState.IsValid)
			{
				TempData["ToastrError"] = GenericToastr.Error_InvalidForm;
				return View(vm);
			}

			var finalType = vm.TypeCodeFinal?.Trim() ?? "";
			var finalCode = vm.Code?.Trim() ?? "";

			if (await _unitOfWork.CdSetRepository.ExistsAsync(finalType, finalCode))
			{
				TempData["ToastrError"] = CdSetMM.CdSetMM_Error_Duplicate;
				return View(vm);
			}

			var entity = vm.ToEntity();
			await _unitOfWork.CdSetRepository.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			// Message success
			TempData["ToastrSuccess"] = string.Format(
				CdSetToastr.Success_EntityCreated,
				finalType,
				finalCode
			);

			_logger.Information("CdSet créé : {TypeCode}|{Code}", entity.TypeCode, entity.Code);

			// ---------- SAVE & CONTINUE ----------
			if (saveAndContinue)
			{
				return RedirectToAction(nameof(Create), new { returnUrl = vm.ReturnUrl });
			}

			// ---------- SAVE NORMAL ----------
			return Redirect(vm.ReturnUrl ?? Url.Action(nameof(Index))!);
		}
		#endregion


		// =====================================================================
		// DRD – Edit
		// =====================================================================
		#region Edit

		[HttpGet]
		public async Task<IActionResult> Edit(string id, string key2, string? returnUrl)
		{
			var entity = await _unitOfWork.CdSetRepository.GetByTypeCodeAndCodeAsync(id, key2);

			if (entity == null)
			{
				TempData["ToastrError"] = GenericToastr.Error_NotFound;
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
				TempData["ToastrError"] = GenericToastr.Error_InvalidForm;
				return View(vm);
			}

			var entity = await _unitOfWork.CdSetRepository.GetByTypeCodeAndCodeAsync(vm.TypeCode, vm.Code);
			if (entity == null)
			{
				TempData["ToastrError"] = GenericToastr.Error_NotFound;
				return RedirectToAction(nameof(Index));
			}

			entity.ModifyFields(vm.DescriptionFr, vm.DescriptionEn, vm.IsActive);

			await _unitOfWork.CdSetRepository.UpdateAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			TempData["ToastrSuccess"] = CdSetToastr.Success_EntityUpdated;
			_logger.Information("CdSet modifié : {TypeCode}|{Code}", entity.TypeCode, entity.Code);

			return Redirect(vm.ReturnUrl ?? Url.Action(nameof(Index))!);
		}

		#endregion


		// =====================================================================
		// DRD – Details
		// =====================================================================
		#region Details

		[HttpGet]
		public async Task<IActionResult> Details(string id, string key2, string? returnUrl)
		{
			var entity = await _unitOfWork.CdSetRepository.GetByTypeCodeAndCodeAsync(id, key2);

			if (entity == null)
			{
				TempData["ToastrError"] = GenericToastr.Error_NotFound;
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


		// =====================================================================
		// DRD – Delete
		// =====================================================================
		#region Delete

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(string entityId)
		{
			if (string.IsNullOrWhiteSpace(entityId))
			{
				TempData["ToastrError"] = GenericToastr.Error_NotFound;
				return RedirectToAction(nameof(Index));
			}

			var parts = entityId.Split('|');
			if (parts.Length < 2)
			{
				TempData["ToastrError"] = GenericToastr.Error_NotFound;
				return RedirectToAction(nameof(Index));
			}

			string typeCode = parts[0];
			string code = parts[1];

			var entity = await _unitOfWork.CdSetRepository.GetByTypeCodeAndCodeAsync(typeCode, code);

			if (entity == null)
			{
				TempData["ToastrError"] = GenericToastr.Error_NotFound;
				return RedirectToAction(nameof(Index));
			}

			await _unitOfWork.CdSetRepository.RemoveAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			TempData["ToastrSuccess"] = CdSetToastr.Success_EntityDeleted;
			_logger.Information("CdSet supprimé : {TypeCode}|{Code}", typeCode, code);

			return RedirectToAction(nameof(Index));
		}

		#endregion
	}
}
