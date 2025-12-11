// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetDisplayVC.cs
// Type de fichier                Classe C#
// Classe                         CdSetDisplayVC
// Emplacement                    Components/CdSetDisplayVC
// Entités concernées             CdSet
// Créé le                        2025-12-11
//
// Description
//     ViewComponent permettant d’afficher un champ readonly basé sur un CodeSet.
//     Utilisé dans les pages Details ou dans tout écran où un code paramétrique
//     doit être affiché sous la forme : « CODE — DescriptionLocale ».
//
// Fonctionnalité
//     - Résolution automatique FR/EN via ICdSetService.
//     - Affichage sécurisé pour codes manquants.
//     - Support des clés TypeCode + Code.
//     - Vue Razor : Default.cshtml.
//
// Modifications
//     2025-12-11    Version initiale conforme DRD v10.
// ============================================================================

using DRD.Application.IServices.SystemTables;
using Microsoft.AspNetCore.Mvc;

namespace DRD.Web.Components.CdSetDisplayVC
{
	/// <summary>
	/// ViewComponent permettant d'afficher un CodeSet sous forme lisible.
	/// </summary>
	public class CdSetDisplayVC : ViewComponent
	{
		// --------------------------------------------------------------------
		// REGION : Services
		// --------------------------------------------------------------------
		#region Services
		/// <summary>
		/// Service applicatif pour récupérer les descriptions CdSet.
		/// </summary>
		private readonly ICdSetService _cdSetService;

		/// <summary>
		/// Constructeur : injection du service CdSet.
		/// </summary>
		public CdSetDisplayVC(ICdSetService cdSetService)
		{
			_cdSetService = cdSetService;
		}
		#endregion

		// --------------------------------------------------------------------
		// REGION : InvokeAsync
		// --------------------------------------------------------------------
		#region InvokeAsync
		/// <summary>
		/// Retourne une vue readonly contenant « Code — Description ».
		/// Si le code ou la description est manquant, la vue affiche une chaîne vide.
		/// </summary>
		/// <param name="typeCode">Famille CdSet (ex. Province, CultureCode).</param>
		/// <param name="code">Code interne (ex. NB, EN).</param>
		public async Task<IViewComponentResult> InvokeAsync(string typeCode, string code)
		{
			if (string.IsNullOrWhiteSpace(typeCode) || string.IsNullOrWhiteSpace(code))
				return View("Default", string.Empty);

			var description = await _cdSetService.GetDescriptionAsync(typeCode, code);

			// Sécurité : si rien trouvé → afficher seulement code
			string display =
				string.IsNullOrWhiteSpace(description)
				? code
				: $"{code} — {description}";

			return View("Default", display);
		}
		#endregion
	}
}
