// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 EntityActionButtonsVM.cs
// Type de fichier                ViewModel (VM)
// Classe                         EntityActionButtonsVM
// Emplacement                    Models/Shared
// Entités concernées             Générique (toutes les entités affichées dans un tableau)
// Créé le                        2025-10-24
//
// Description
//     ViewModel générique utilisé par la vue partielle _EntityActionButtons.
//     Fournit l’ensemble des informations nécessaires pour générer un groupe
//     standardisé de boutons d’action (Détails, Modifier, Désactiver, Supprimer),
//     avec prise en charge des clés simples ou concaténées, et messages localisés.
//
// Fonctionnalité
//     - Support des clés simples, doubles ou multiples (concaténées par “|”).
//     - Titre et message localisés pour les actions Désactiver et Supprimer.
//     - Configuration flexible : activer/désactiver certains boutons.
//     - Gestion des callbacks JavaScript pour mise à jour dynamique.
//     - Standardisation des actions de toutes les tables DRD.
//
// Modifications
//     2025-12-07    Conversion au format DRD v10 (en-tête, régions, nomenclature).
//     2025-10-29    Adaptation universelle pour clés multiples et callbacks JS.
//     2025-10-24    Version initiale.
// ============================================================================

using DRD.Resources;
using DRD.Resources.Popup;

namespace DRD.Web.Models.Shared
{
	/// <summary>
	/// ViewModel utilisé pour le composant partiel _EntityActionButtons.
	/// Permet de générer dynamiquement les boutons Voir, Modifier,
	/// Désactiver et Supprimer avec messages localisés.
	/// </summary>
	public class EntityActionButtonsVM
	{
		#region Templates internes
		/// <summary>
		/// Modèle du message de désactivation (avant localisation dynamique).
		/// </summary>
		private readonly string _deactivateMessageTemplate;

		/// <summary>
		/// Modèle du message de suppression (avant localisation dynamique).
		/// </summary>
		private readonly string _deleteMessageTemplate;
		#endregion

		#region Configuration principale
		/// <summary>
		/// Nom du contrôleur cible (ex.: "CdSet", "Client").
		/// </summary>
		public string ControllerName { get; set; }

		/// <summary>
		/// Identifiant unique de l’entité, pouvant être composite (concaténé par "|").
		/// </summary>
		public string EntityId { get; set; }

		/// <summary>
		/// Nom de l’entité, affiché dans les modales (ex.: "Institution", "Famille").
		/// </summary>
		public string EntityName { get; set; }

		/// <summary>
		/// Identifiant humain de l’entité (nom, code, numéro), utilisé dans les modales.
		/// </summary>
		public string EntityIdentifier { get; set; }

		/// <summary>
		/// URL de retour après une action.
		/// </summary>
		public string? ReturnUrl { get; set; }
		#endregion

		#region Affichage des boutons
		public bool ShowDetails { get; set; } = true;
		public bool ShowEdit { get; set; } = true;
		public bool ShowDeactivate { get; set; } = true;
		public bool ShowDelete { get; set; } = true;
		#endregion

		#region Callback JavaScript
		/// <summary>
		/// Fonction JavaScript à appeler en cas de suppression réussie.
		/// </summary>
		public string? DeleteModalSuccessCallback { get; set; }
		#endregion

		#region Titres & messages localisés
		/// <summary>
		/// Titre localisé de la modale de désactivation.
		/// </summary>
		public string DeactivateTitle =>
			Popups.Confirm_Deactivate_Title_Entity
				.Replace("{0}", EntityIdentifier)
				.Replace("{1}", EntityName);

		/// <summary>
		/// Titre localisé de la modale de suppression.
		/// </summary>
		public string DeleteTitle =>
			Popups.Confirm_Delete_Title_Entity
				.Replace("{0}", EntityIdentifier)
				.Replace("{1}", EntityName);

		/// <summary>
		/// Message localisé de la modale de désactivation.
		/// </summary>
		public string DeactivateMessage =>
			_deactivateMessageTemplate
				.Replace("{0}", EntityIdentifier)
				.Replace("{1}", EntityName);

		/// <summary>
		/// Message localisé de la modale de suppression.
		/// </summary>
		public string DeleteMessage =>
			_deleteMessageTemplate
				.Replace("{0}", EntityIdentifier)
				.Replace("{1}", EntityName);
		#endregion

		#region Constructeurs
		/// <summary>
		/// Constructeur complet.
		/// </summary>
		public EntityActionButtonsVM(
			string controllerName,
			string entityId,
			string entityName,
			string entityIdentifier,
			string deactivateMessageTemplate,
			string deleteMessageTemplate,
			string? deleteModalSuccessCallback = null)
		{
			ControllerName = controllerName;
			EntityId = entityId;
			EntityName = entityName;
			EntityIdentifier = entityIdentifier;
			_deactivateMessageTemplate = deactivateMessageTemplate;
			_deleteMessageTemplate = deleteMessageTemplate;
			DeleteModalSuccessCallback = deleteModalSuccessCallback;
		}

		/// <summary>
		/// Constructeur simplifié lorsque seule une action Supprimer est utilisée.
		/// </summary>
		public EntityActionButtonsVM(
			string controllerName,
			string entityId,
			string entityName,
			string entityIdentifier,
			string deleteMessageTemplate)
			: this(controllerName, entityId, entityName, entityIdentifier, string.Empty, deleteMessageTemplate)
		{
		}
		#endregion
	}
}
