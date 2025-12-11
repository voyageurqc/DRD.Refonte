// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetSelectVC.cs
// Type                           Classe C#
// Classe                         CdSetSelectVC
// Emplacement                    Components/CdSetSelectVC
// Entités concernées             CdSet, CdSetSelectVM, CdSetSelectItemVM
// Créé le                        2025-12-11
//
// Description
//     Composant ViewComponent DRDv10 permettant d'afficher un champ <select>
//     basé sur les données d’un CdSet spécifique. Le composant génère
//     automatiquement :
//         - le label (localisé via asp-for)
//         - la liste déroulante Bootstrap 5 + input-3d
//         - la validation MVC
//         - un placeholder localisé
//         - les options "CODE - DescriptionLocalized"
//
// Fonctionnalité
//     - Injection ICdSetService (Clean Architecture)
//     - Mapping Domain → CdSetSelectItemVM
//     - Respect complet des règles DRDv10 (UI, localisation, architecture)
//     - Logging Serilog (niveau diagnostic)
//
// Modifications
//     2025-12-11    Ajustement emplacement + namespace selon architecture DRDv10.
//     2025-12-10    Version initiale DRDv10 — composant officiel CdSetSelect.
// ============================================================================

using DRD.Application.IServices.SystemTables;
using DRD.Web.Models.GrpSystemTables.CdSetVM;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DRD.Web.Components.CdSetSelectVC
{
	/// <summary>
	/// ViewComponent DRDv10 responsable de rendre un champ de sélection CdSet.
	/// </summary>
	public class CdSetSelectVC : ViewComponent
	{
		#region Services
		private readonly ICdSetService _cdSetService;
		private readonly Serilog.ILogger _logger;

		/// <summary>
		/// Constructeur : injection des services.
		/// </summary>
		public CdSetSelectVC(ICdSetService cdSetService)
		{
			_cdSetService = cdSetService;
			_logger = Log.ForContext<CdSetSelectVC>();
		}
		#endregion

		#region InvokeAsync
		/// <summary>
		/// Rend le composant avec un ViewModel CdSetSelectVM déjà préparé.
		/// </summary>
		/// <param name="vm">Vue-modèle du sélecteur.</param>
		public async Task<IViewComponentResult> InvokeAsync(CdSetSelectVM vm)
		{
			_logger.Information(
				"CdSetSelectVC invoked for TypeCode={TypeCode} Selected={Selected}",
				vm.TypeCode, vm.SelectedCode
			);

			// Charger les éléments du CdSet correspondant
			var items = await _cdSetService.GetByTypeCodeAsync(vm.TypeCode);

			vm.Items = items
				.OrderBy(x => x.Code)
				.Select(x => new CdSetSelectItemVM
				{
					Code = x.Code,
					DescriptionFr = x.DescriptionFr,
					DescriptionEn = x.DescriptionEn
				})
				.ToList();

			return View("Default", vm);
		}
		#endregion
	}
}
