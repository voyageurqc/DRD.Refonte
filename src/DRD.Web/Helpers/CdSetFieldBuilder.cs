// ============================================================================
// Projet:        DRD.Web
// Fichier:       CdSetFieldBuilder.cs
// Type:          Helper statique
// Classe:        CdSetFieldBuilder
// Emplacement:   Helpers/
// Entité(s):     CdSet
// Créé le:       2025-07-17
//
// Description:
//      Classe utilitaire statique permettant de générer dynamiquement des
//      ViewModel CdSetSelectVM pour l'affichage de champs de sélection
//      basés sur les données d'une table CdSet.
//
// Fonctionnalité:
//      - Génération d'un CdSetSelectVM à partir d'un TypeCode unique.
//      - Génération multiple via une map clé -> (TypeCode, Label).
//      - Détection automatique de la culture courante (fr/en).
//      - Calcul automatique de la largeur d'affichage du champ.
//      - Réduction des appels redondants via cache local.
// ============================================================================

using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using DRD.Application.Common.Interfaces.Services;
using DRD.Web.Models.ConfigurationTables;

namespace DRD.Web.Helpers
{
    public static class CdSetFieldBuilder
    {
        public static async Task<CdSetSelectVM> BuildAsync(
            string fieldName,
            string label,
            string typeCode,
            string? selectedValue,
            ICdSetService cdSetService,
            string? id = null,
            string? extraCssClasses = null,
            IEnumerable<SelectListItem>? preFetchedItems = null)
        {
            bool isFrench = CultureInfo.CurrentCulture.Name.StartsWith("fr");

            var items = preFetchedItems?.ToList()
                ?? (await cdSetService.GetByTypeCodeAsync(typeCode))
                    .Select(cd => new SelectListItem
                    {
                        Value = cd.Code,
                        Text = isFrench
                            ? $"{cd.Code} - {cd.Description}"
                            : $"{cd.Code} - {cd.DescriptionAnglaise}",
                        Selected = cd.Code == selectedValue
                    })
                    .OrderBy(i => i.Text)
                    .ToList();

            int maxWidth = items.Any()
                ? items.Max(i => i.Text.Length)
                : 0;

            int pixelWidth = Math.Max((maxWidth * 8) + 30, 120);

            return new CdSetSelectVM
            {
                FieldName = fieldName,
                Label = label,
                TypeCode = typeCode,
                SelectedValue = selectedValue ?? "",
                Items = items,
                Classes = extraCssClasses ?? "form-select form-select-sm",
                Id = id ?? fieldName,
                CalculatedWidthStyle = $"width: {pixelWidth}px !important;"
            };
        }

        public static async Task<IDictionary<string, CdSetSelectVM>> BuildMultipleAsync(
            object sourceObject,
            Dictionary<string, (string typeCode, string label)> fieldMap,
            ICdSetService cdSetService,
            string? extraCssClasses = null)
        {
            var result = new Dictionary<string, CdSetSelectVM>();
            var fetched = new Dictionary<string, List<SelectListItem>>();
            bool isFrench = CultureInfo.CurrentCulture.Name.StartsWith("fr");

            foreach (var kvp in fieldMap)
            {
                var propertyName = kvp.Key;
                var (typeCode, label) = kvp.Value;

                if (!fetched.ContainsKey(typeCode))
                {
                    var fetchedItems = (await cdSetService.GetByTypeCodeAsync(typeCode))
                        .Select(cd => new SelectListItem
                        {
                            Value = cd.Code,
                            Text = isFrench
                                ? $"{cd.Code} - {cd.Description}"
                                : $"{cd.Code} - {cd.DescriptionAnglaise}",
                            Selected = false
                        })
                        .OrderBy(i => i.Text)
                        .ToList();

                    fetched[typeCode] = fetchedItems;
                }

                var propInfo = sourceObject.GetType().GetProperty(propertyName);
                var selectedValue = propInfo?.GetValue(sourceObject)?.ToString() ?? string.Empty;

                var vm = await BuildAsync(
                    fieldName: propertyName,
                    label: label,
                    typeCode: typeCode,
                    selectedValue: selectedValue,
                    cdSetService: cdSetService,
                    id: propertyName,
                    extraCssClasses: extraCssClasses,
                    preFetchedItems: fetched[typeCode]);

                result[propertyName] = vm;
            }

            return result;
        }
    }
}
