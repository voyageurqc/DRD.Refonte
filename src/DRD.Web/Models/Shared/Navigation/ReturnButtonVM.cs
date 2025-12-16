// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 ReturnButtonVM.cs
// Type                           ViewModel
// Classe                         ReturnButtonVM
// Emplacement                    Models/Shared/Navigation
// Entité(s) concernées           Navigation (bouton Retour)
// Créé le                        2025-12-16
//
// Description
//     ViewModel dédié au bouton Retour DRD.
//     Centralise les informations nécessaires à la navigation de retour
//     sans dépendre des ViewModels d’écran.
//
// Fonctionnalité
//     - Transporte l’URL de retour explicite.
//     - Définit le contrôleur de fallback.
//     - Utilisé par le partial _ReturnButton.cshtml.
//
// Modifications
//     2025-12-16    Création initiale DRD v10.
// ============================================================================

namespace DRD.Web.Models.Shared.Navigation
{
	/// <summary>
	/// ViewModel utilisé par le bouton Retour DRD.
	/// </summary>
	public class ReturnButtonVM
	{
		/// <summary>
		/// URL explicite de retour vers l’écran appelant.
		/// </summary>
		public string? ReturnUrl { get; set; }

		/// <summary>
		/// Nom du contrôleur utilisé en fallback si ReturnUrl est absent.
		/// </summary>
		public string ControllerName { get; set; } = string.Empty;
	}
}
