// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 CdSetRowVM.cs
// Type de fichier                Classe C#
// Classe                         CdSetRowVM
// Emplacement                    Models/GrpSystemTables/CdSetVM
// Entités concernées             CdSet (projection UI)
// Créé le                        2025-12-07
//
// Description
//     Représente une ligne de données CdSet destinée à l’affichage dans la vue
//     Index via DataTables. Contient les champs nécessaires ainsi qu’une
//     propriété DescriptionLocalized pour supporter la culture courante.
//
// Fonctionnalité
//     - Transporter les valeurs essentielles d’un CdSet.
//     - Fournir une description FR/EN basée sur la culture active.
//     - Servir de base au tableau DataTables dans Index.
//
// Modifications
//     2025-12-07    Version initiale DRD v10 (DisplayOrder retiré).
// ============================================================================

using System;
using System.Globalization;

namespace DRD.Web.Models.GrpSystemTables.CdSetVM
{
	/// <summary>
	/// Modèle représentant une ligne affichée dans l’index CdSet.
	/// </summary>
	public class CdSetRowVM
	{
		/// <summary>Type du code (clé 1).</summary>
		public string TypeCode { get; set; } = string.Empty;

		/// <summary>Code unique du CdSet (clé 2).</summary>
		public string Code { get; set; } = string.Empty;

		/// <summary>Description française.</summary>
		public string DescriptionFr { get; set; } = string.Empty;

		/// <summary>Description anglaise.</summary>
		public string? DescriptionEn { get; set; }

		/// <summary>Indique si le code est actif.</summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Description automatiquement localisée selon la culture courante.
		/// </summary>
		public string DescriptionLocalized =>
			CultureInfo.CurrentUICulture.Name.StartsWith("en", StringComparison.OrdinalIgnoreCase)
				? (DescriptionEn ?? DescriptionFr)
				: DescriptionFr;
	}
}
