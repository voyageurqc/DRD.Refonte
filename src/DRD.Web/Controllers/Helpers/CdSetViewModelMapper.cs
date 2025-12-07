// ============================================================================
// Projet:        DRD.Web
// Fichier:       CdSetViewModelMapper.cs
// Type:          Helper statique
// Classe:        CdSetViewModelMapper
// Emplacement:   Controllers/Helpers/
// Entité(s):     CdSet
// Créé le:       2025-07-17
//
// Description:
//      Classe utilitaire pour mapper les objets CdSetItemDto (couche Application)
//      vers les objets CdSetSelectVM (couche Web) utilisés pour l'affichage.
//
// Fonctionnalité:
//      - Conversion d’un seul DTO vers un ViewModel.
//      - Conversion d’un dictionnaire complet de DTOs vers un dictionnaire de ViewModels.
//      - Utilise une méthode d’extension pour simplifier la transformation vers SelectListItem.
//
// Modifications:
//      2025-07-17: Création initiale avec prise en charge du mapping via extension ToSelectList().
// ============================================================================

using DRD.Application.DTOs.ConfigurationTables;
using DRD.Web.Extensions;
using DRD.Web.Models.ConfigurationTables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DRD.Web.Controllers.Helpers
{
    public static class CdSetViewModelMapper
    {
        /// <summary>
        /// Convertit un seul DTO (CdSetItemDto) vers le ViewModel CdSetSelectVM
        /// </summary>
        public static CdSetSelectVM MapToViewModel(CdSetItemDto dto)
        {
            return new CdSetSelectVM
            {
                FieldName = dto.FieldName,
                Label = dto.Label,
                SelectedValue = dto.SelectedValue,
                Items = dto.ToSelectList()
            };
        }

        /// <summary>
        /// Convertit un dictionnaire complet de DTO vers un dictionnaire de ViewModels
        /// </summary>
        public static Dictionary<string, CdSetSelectVM> MapAll(Dictionary<string, CdSetItemDto> dtoDict)
        {
            return dtoDict.ToDictionary(
                entry => entry.Key,
                entry => MapToViewModel(entry.Value)
            );
        }
    }
}
