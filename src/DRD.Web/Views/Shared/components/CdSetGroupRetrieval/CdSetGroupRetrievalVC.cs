// ============================================================================
// 💻 Projet           : DRD.Web
// 📄 Nom du fichier     : CdSetGroupRetrievalViewComponent.cs
// 📄 Classe du fichier   : ViewComponent
// 📍 Emplacement         : ViewComponents/
// 🏛️ Entité(s) touchée(s) : (Aucune directement, utilise ICdSetService)
// 📅 Créé le            : 2025-11-26
//
// 📌 Description :
//      Component de vue pour la récupération d'un groupe de Codeset
//      (CdSet) en mode lecture seule pour les écrans 'Details'.
//
// 🎯 Fonctionnalité :
//      - Reçoit une liste de TypeCodes et de valeurs sélectionnées + la culture + taille de colonne.
//      - Utilise ICdSetService pour récupérer la description FR/EN du code sélectionné.
//      - Isole la logique de service de la couche Web.
//
// 🛠️ Modifications :
//      2025-11-26 : Création initiale.
//      2025-11-26 : Signature InvokeAsync mise à jour pour Dictionary<string, object>.
//      2025-11-26 : Implémentation complète de la gestion des null, du bilinguisme 
//                   et du passage du paramètre de mise en page 'colSize' via ViewBag.
// ============================================================================

using Microsoft.AspNetCore.Mvc;
using DRD.Application.Common.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DRD.Web.ViewComponents
{
    public class CdSetGroupRetrievalViewComponent : ViewComponent
    {
        private const string DefaultCulture = "fr-CA"; // Culture par défaut
        private readonly ICdSetService _cdSetService;

        public CdSetGroupRetrievalViewComponent(ICdSetService cdSetService)
        {
            _cdSetService = cdSetService;
        }

        /// <summary>
        /// Reçoit un dictionnaire de paramètres incluant les TypeCode/valeur, la culture et la taille de colonne (colSize).
        /// </summary>
        /// <returns>
        /// Un dictionnaire contenant, pour chaque TypeCode, le code sélectionné
        /// et sa description correspondante (FR/EN).
        /// </returns>
        public async Task<IViewComponentResult> InvokeAsync(
            Dictionary<string, object> parameters)
        {
            var result = new Dictionary<string, (string Code, string Description)>();

            // 1. GESTION DES PARAMÈTRES DE CONTRÔLE

            // ⭐ 1a. Récupérer et passer la taille de colonne à la vue
            if (parameters.TryGetValue("colSize", out object? colSizeObject) && colSizeObject is int colSize)
            {
                ViewBag.ColSize = colSize; // Passé à Default.cshtml
                parameters.Remove("colSize");
            }

            // ⭐ 1b. Récupérer la culture
            string currentCulture = DefaultCulture;
            if (parameters.TryGetValue("culture", out object? cultureObject) && cultureObject is string cultureString)
            {
                currentCulture = cultureString;
                parameters.Remove("culture");
            }

            // 2. Traiter les TypeCodes restants
            foreach (var kvp in parameters)
            {
                string typeCode = kvp.Key;
                // Gère les valeurs nulles ou vides envoyées par le ViewModel
                string selected = kvp.Value?.ToString() ?? string.Empty;

                if (string.IsNullOrEmpty(selected))
                {
                    // La valeur est non sélectionnée. On ajoute un affichage d'erreur/vide
                    // pour la vue sans faire d'appel inutile au service.
                    result[typeCode] = (string.Empty, $"⚠️ {typeCode} (Non défini)");
                    continue;
                }

                // 3. Récupération de la description FR/EN du code sélectionné
                // La culture est passée pour assurer le bilinguisme
                string description =
                    await _cdSetService.GetDescriptionAsync(typeCode, selected, currentCulture);

                // 4. Stockage du résultat (Code - Description)
                result[typeCode] = (selected, description);
            }

            return View(result);
        }
    }
}
