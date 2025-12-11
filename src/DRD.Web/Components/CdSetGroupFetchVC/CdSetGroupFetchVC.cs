// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetGroupRetrievalVC.cs
// Type de fichier                Classe C#
// Classe                         CdSetGroupRetrievalVC
// Emplacement                    Components/CdSetGroupRetrievalVC
// Entités concernées             CdSet, CdSetGroupVM, CdSetSelectItemVM
// Créé le                        2025-12-11
//
// Description
//     Composant ViewComponent dont le rôle est de récupérer un groupe complet
//     de CdSet (basé sur TypeCode) et de le fournir à une vue d'affichage.
//     Ce composant NE génère PAS de champ <select> : il est conçu pour les
//     écrans de consultation, panneaux d’information, modales, etc.
//
// Fonctionnalité
//     - Injection ICdSetService (Clean Architecture).
//     - Récupération d’un groupe complet selon un TypeCode.
//     - Mapping Domain → CdSetSelectItemVM.
//     - Logging Serilog.
//     - Ne gère aucune logique UI.
//
// Modifications
//     2025-12-11    Version initiale DRDv10 — composant officiel retrieving-group.
//     2025-12-11    Ajustement emplacement + namespace final DRDv10.
// ============================================================================

using DRD.Application.IServices.SystemTables;
using DRD.Web.Models.GrpSystemTables.CdSetVM;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DRD.Web.Components.CdSetGroupFetchVC
{
	/// <summary>
	/// ViewComponent responsable de récupérer et exposer un groupe CdSet complet.
	/// </summary>
	public class CdSetGroupFetchVC : ViewComponent
	{
		// --------------------------------------------------------------------
		// REGION : Services
		// --------------------------------------------------------------------
		#region Services
		/// <summary>
		/// Service applicatif permettant de récupérer les données CdSet.
		/// </summary>
		private readonly ICdSetService _cdSetService;

		/// <summary>
		/// Logger Serilog fortement typé pour diagnostic du composant.
		/// </summary>
		private readonly Serilog.ILogger _logger;

		/// <summary>
		/// Constructeur : injection du service CdSet.
		/// </summary>
		public CdSetGroupFetchVC(ICdSetService cdSetService)
		{
			_cdSetService = cdSetService;
			_logger = Log.ForContext<CdSetGroupFetchVC>();
		}
		#endregion

		// --------------------------------------------------------------------
		// REGION : InvokeAsync
		// --------------------------------------------------------------------
		#region InvokeAsync
		/// <summary>
		/// Récupère un groupe complet de CdSet et le mappe dans un ViewModel.
		/// </summary>
		/// <param name="typeCode">Famille (clé principale du groupe).</param>
		public async Task<IViewComponentResult> InvokeAsync(string typeCode)
		{
			_logger.Information("CdSetGroupRetrievalVC invoked for TypeCode={TypeCode}", typeCode);

			var items = await _cdSetService.GetByTypeCodeAsync(typeCode);

			var vm = new CdSetGroupVM
			{
				GroupName = typeCode,
				Items = items
					.OrderBy(x => x.Code)
					.Select(x => new CdSetGroupItemVM
					{
						Code = x.Code,
						DescriptionLocalized =
							$"{x.Code} – {(string.IsNullOrWhiteSpace(x.DescriptionFr) ? x.DescriptionEn : x.DescriptionFr)}"
					})
					.ToList()
			};

			return View("Default", vm);
		}
		#endregion
	}
}
