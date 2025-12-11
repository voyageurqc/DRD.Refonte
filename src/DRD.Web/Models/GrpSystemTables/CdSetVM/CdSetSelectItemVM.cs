// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetSelectItemVM.cs
// Type                           Classe C#
// Classe                         CdSetSelectItemVM
// Emplacement                    Models/GrpSystemTables/CdSetVM
// Entités concernées             CdSet (Item de sélection)
// Créé le                        2025-12-11
//
// Description
//     Représente un élément individuel affiché dans un dropdown de sélection
//     CdSet. Inclut le code, la description bilingue et la version localisée.
//
// Fonctionnalité
//     - Fournit une chaîne d'affichage normalisée : "Code – DescriptionLocalized".
//     - S'adapte automatiquement à la culture active (fr-CA / en-CA).
//
// Modifications
//     2025-12-11    Version initiale DRDv10.
// ============================================================================

using System.Globalization;

namespace DRD.Web.Models.GrpSystemTables.CdSetVM
{
	/// <summary>
	/// Élément individuel utilisé pour afficher un item de CdSet dans un sélecteur.
	/// </summary>
	public class CdSetSelectItemVM
	{
		/// <summary>
		/// Code unique dans le groupe CdSet.
		/// </summary>
		public string Code { get; set; } = string.Empty;

		/// <summary>
		/// Description française.
		/// </summary>
		public string DescriptionFr { get; set; } = string.Empty;

		/// <summary>
		/// Description anglaise.
		/// </summary>
		public string? DescriptionEn { get; set; }

		/// <summary>
		/// Version localisée de la description selon la culture active.
		/// </summary>
		public string DescriptionLocalized =>
			CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("en", StringComparison.OrdinalIgnoreCase)
				? (DescriptionEn ?? DescriptionFr)
				: DescriptionFr;

		/// <summary>
		/// Texte complet affiché dans le dropdown : "CODE – DescriptionLocalized".
		/// </summary>
		public string DisplayText => $"{Code} – {DescriptionLocalized}";
	}
}
