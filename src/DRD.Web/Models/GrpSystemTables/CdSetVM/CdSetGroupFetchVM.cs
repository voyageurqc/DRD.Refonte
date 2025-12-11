// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetGroupRetrievalVM.cs
// Type                           Classe C# (ViewModel)
// Classe                         CdSetGroupRetrievalVM
// Emplacement                    Models/GrpSystemTables/CdSetVM
// Entités concernées             CdSet (projection UI)
// Créé le                        2025-12-11
//
// Description
//     ViewModel destiné au ViewComponent CdSetGroupRetrievalVC. Permet d'afficher
//     une collection de groupes contenant chacun une liste de codes (CdSet).
//     Supporte la localisation, les titres dynamiques et les messages "aucun
//     résultat" adaptés.
//
// Fonctionnalité
//     - Contient la liste des groupes à afficher (nom + items).
//     - Définit le message localisé pour les groupes vides.
//     - Fournit une structure simple et réutilisable pour d’autres modules.
//
// Modifications
//     2025-12-11    Version initiale DRDv10.
// ============================================================================

using System.Collections.Generic;

namespace DRD.Web.Models.GrpSystemTables.CdSetVM
{
	/// <summary>
	/// ViewModel principal utilisé pour afficher une liste de groupes
	/// de CdSet (familles regroupées ou sections logiques).
	/// </summary>
	public class CdSetGroupFetchVM
	{
		#region Groupe(s)
		/// <summary>
		/// Liste des groupes affichés dans la vue. Chaque groupe contient un nom
		/// et une liste d’éléments CdSet spécifiques à ce regroupement.
		/// </summary>
		public List<CdSetGroupVM> Groups { get; set; } = new();
		#endregion

		#region Messages
		/// <summary>
		/// Message affiché lorsqu’un groupe ne contient aucun élément.
		/// Ce texte devrait être fourni via les ressources localisées.
		/// </summary>
		public string NoItemsMessage { get; set; } = string.Empty;
		#endregion
	}

	/// <summary>
	/// Représente un groupe contenant plusieurs CdSet.
	/// </summary>
	public class CdSetGroupVM
	{
		#region Propriétés du groupe

		/// <summary>
		/// Nom ou titre du groupe (localisé par le contrôleur ou le service).
		/// </summary>
		public string GroupName { get; set; } = string.Empty;

		/// <summary>
		/// Liste d’éléments affichés dans le groupe.
		/// </summary>
		public List<CdSetGroupItemVM> Items { get; set; } = new();
		#endregion
	}

	/// <summary>
	/// Élément individuel (ligne) d’un groupe CdSet.
	/// </summary>
	public class CdSetGroupItemVM
	{
		#region Propriétés item

		/// <summary>
		/// Code interne (clé unique dans un TypeCode).
		/// </summary>
		public string Code { get; set; } = string.Empty;

		/// <summary>
		/// Description localisée (FR/EN). Calculée avant d’être envoyée au VC.
		/// </summary>
		public string DescriptionLocalized { get; set; } = string.Empty;

		#endregion
	}
}
