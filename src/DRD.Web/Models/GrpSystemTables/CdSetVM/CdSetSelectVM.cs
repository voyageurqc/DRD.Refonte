// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetSelectVM.cs
// Type                           Classe C#
// Classe                         CdSetSelectVM
// Emplacement                    Models/GrpSystemTables/CdSetVM
// Entités concernées             CdSet (sélection)
// Créé le                        2025-12-11
//
// Description
//     ViewModel utilisé pour alimenter les composants de sélection CdSet.
//     Permet de préciser la famille (TypeCode) ciblée, l'item sélectionné,
//     et la liste des items à afficher.
//
// Fonctionnalité
//     - Gère la sélection d'un type particulier (ex. Province, AccessType).
//     - Fournit les items au composant de sélection avec DisplayText formaté.
//     - Option de valeur par défaut (placeholder).
//
// Modifications
//     2025-12-11    Version initiale DRDv10.
// ============================================================================

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DRD.Resources.Common;

namespace DRD.Web.Models.GrpSystemTables.CdSetVM
{
	/// <summary>
	/// ViewModel utilisé pour un sélecteur CdSet typé (liste déroulante).
	/// </summary>
	public class CdSetSelectVM
	{
		/// <summary>
		/// Nom interne de la famille CdSet (ex.: "Province", "AccessType").
		/// </summary>
		[Required(ErrorMessageResourceName = nameof(Common.Validation_Required),
				  ErrorMessageResourceType = typeof(Common))]
		public string TypeCode { get; set; } = string.Empty;

		/// <summary>
		/// Code actuellement sélectionné dans le sélecteur.
		/// </summary>
		public string? SelectedCode { get; set; }

		/// <summary>
		/// Liste des items affichés (code + descriptions bilingues).
		/// </summary>
		public List<CdSetSelectItemVM> Items { get; set; } = new();

		/// <summary>
		/// Texte de l’option par défaut dans le dropdown (ex.: "Sélectionner...").
		/// </summary>
		public string? Placeholder { get; set; }
	}
}
