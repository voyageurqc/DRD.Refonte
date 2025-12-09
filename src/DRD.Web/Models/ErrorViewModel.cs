// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 ErrorViewModel.cs
// Type de fichier                ViewModel (VM)
// Classe                         ErrorViewModel
// Emplacement                    Models
// Entités concernées             N/A (erreurs globales MVC)
// Créé le                        2025-12-09
//
// Description
//     Modèle utilisé par la vue d’erreur DRD (Error.cshtml). Fournit l’ID
//     unique de requête, le message d’erreur optionnel, l’URL concernée et
//     la date/heure de l’occurrence. Compatible avec UseExceptionHandler,
//     Serilog et le pipeline MVC.
//
// Fonctionnalité
//     - Expose RequestId (Activity ou TraceIdentifier).
//     - Permet l’affichage de détails optionnels en développement.
//     - Aide au traçage Serilog des erreurs utilisateur.
//
// Modifications
//     2025-12-09    Conversion complète au format DRD v10.
// ============================================================================

namespace DRD.Web.Models
{
    /// <summary>
    /// ViewModel utilisé pour l’affichage des erreurs générales
    /// via Home/Error et le middleware UseExceptionHandler.
    /// </summary>
    public class ErrorViewModel
    {
        // --------------------------------------------------------------------
        // REGIONS : Identification
        // --------------------------------------------------------------------
        #region Identification

        /// <summary>
        /// Identifiant unique pour tracer l'erreur (Activity ou TraceIdentifier).
        /// </summary>
        public string? RequestId { get; set; }

        #endregion

        // --------------------------------------------------------------------
        // REGION : Métadonnées optionnelles
        // --------------------------------------------------------------------
        #region Métadonnées

        /// <summary>
        /// Message d’erreur (affiché uniquement en environnement Development).
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Date/heure de l'erreur (UTC).
        /// </summary>
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// URL qui a déclenché l’erreur (si disponible).
        /// </summary>
        public string? Path { get; set; }

        #endregion

        // --------------------------------------------------------------------
        // REGION : Calculs
        // --------------------------------------------------------------------
        #region Calculs

        /// <summary>
        /// Indique si RequestId doit être affiché.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        #endregion
    }
}
