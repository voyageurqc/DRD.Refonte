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
//     2025-12-11    DRD v10 : ajout commentaires XML (summary/param/returns) requis
//                   par les règles DRD (#90-#91) et suppression warning CS1591.
//     2025-12-11    DRD v10 : familles dans Create GET, NEW_OPTION, reload familles,
//                   normalisation Exists, logs Serilog supplémentaires.
//     2025-12-09    Harmonisation DRD v10 : régions & en-tête.
//     2025-12-08    Migration Toastr vers ressources strongly typed.
//     2025-12-07    Version initiale DRD v10.
// ============================================================================

using DRD.Application.Common.Interfaces;
using DRD.Resources.MessagesMetier.CdSet;
using DRD.Resources.ToastrMessages;
using DRD.Web.Models.GrpSystemTables.CdSetVM;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DRD.Web.Controllers.GrpSystemTables
{
	/// <summary>
	/// Contrôleur MVC gérant toutes les opérations CRUD du module CdSet,
	/// incluant l’affichage, la création, la modification, les détails
	/// et la suppression via modale universelle. Conforme DRD v10.
	/// </summary>
	public class CdSetController : Controller
	{
		// =====================================================================
		// DRD – Services
		// =====================================================================
		#region Services

		private readonly IUnitOfWork _unitOfWork;
		private readonly Serilog.ILogger _logger;

		/// <summary>
		/// Initialise le contrôleur CdSet avec le UnitOfWork et le logger Serilog.
		/// </summary>
		/// <param name="unitOfWork">Service d'accès aux dépôts métier.</param>
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

		/// <summary>
		/// Affiche la liste des CdSet avec filtres (TypeCode, recherche).
		/// </summary>
		/// <param name="typeCode">Filtre sur la famille.</param>
		/// <param name="search">Recherche textuelle dans descriptions ou code.</param>
		/// <returns>Vue Index avec liste filtrée.</returns>
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

		/// <summary>
		/// Affiche la vue de création d’un nouvel enregistrement CdSet.
		/// Injecte aussi les familles existantes + l’option NEW_OPTION.
		/// </summary>
		/// <param name="returnUrl">URL de retour après la création.</param>
		/// <returns>Vue Create initialisée.</returns>
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

		/// <summary>
		/// Traite la création d’un CdSet après soumission du formulaire.
		/// Valide ModelState, vérifie existence, appelle le Domain puis sauvegarde.
		/// </summary>
		/// <param name="vm">ViewModel contenant les données à créer.</param>
		/// <returns>Redirect vers ReturnUrl ou vue Create si invalide.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CdSetCreateVM vm)
		{
			// Toujours recharger les familles (DRD)
			var all = await _unitOfWork.CdSetRepository.GetAllAsync();
			var distinctFamilies = all
				.Select(x => x.TypeCode)
				.Distinct()
				.OrderBy(x => x)
				.ToList();
			vm.AvailableFamilies = new List<string> { CdSetCreateVM.NEW_OPTION }
				.Concat(distinctFamilies);

			if (!ModelState.IsValid)
			{
				_logger.Warning("CdSet - Create - ModelState invalide. Données : {@VM}", vm);
				TempData["ToastrError"] = GenericToastr.Error_InvalidForm;
				return View(vm);
			}

			var finalType = vm.TypeCodeFinal?.Trim() ?? "";
			var finalCode = vm.Code?.Trim() ?? "";

			var exists = await _unitOfWork.CdSetRepository.ExistsAsync(finalType, finalCode);
			if (exists)
			{
				TempData["ToastrError"] = CdSetMM.CdSetMM_Error_Duplicate;
				return View(vm);
			}

			var entity = vm.ToEntity();

			await _unitOfWork.CdSetRepository.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			TempData["ToastrSuccess"] = CdSetToastr.Success_EntityCreated;
			_logger.Information("CdSet créé : {TypeCode}|{Code}", entity.TypeCode, entity.Code);

			return Redirect(vm.ReturnUrl ?? Url.Action(nameof(Index))!);
		}

		#endregion

		// =====================================================================
		// DRD – Edit
		// =====================================================================
		#region Edit

		/// <summary>
		/// Charge un CdSet existant afin de permettre son édition.
		/// </summary>
		/// <param name="id">TypeCode composite (clé partie 1).</param>
		/// <param name="key2">Code composite (clé partie 2).</param>
		/// <param name="returnUrl">URL de retour après modification.</param>
		/// <returns>Vue Edit préremplie ou redirection si introuvable.</returns>
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

		/// <summary>
		/// Traite la modification d’un CdSet existant.
		/// </summary>
		/// <param name="vm">Données éditées provenant de la vue.</param>
		/// <returns>Vue Edit ou redirection vers ReturnUrl.</returns>
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

		/// <summary>
		/// Affiche les détails d’un enregistrement CdSet.
		/// </summary>
		/// <param name="id">TypeCode (clé 1).</param>
		/// <param name="key2">Code (clé 2).</param>
		/// <param name="returnUrl">URL de retour.</param>
		/// <returns>Vue Details ou redirection si introuvable.</returns>
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

		/// <summary>
		/// Supprime un enregistrement CdSet via modale universelle.
		/// </summary>
		/// <param name="entityId">Clé composite au format TypeCode|Code.</param>
		/// <returns>Redirection vers Index.</returns>
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
