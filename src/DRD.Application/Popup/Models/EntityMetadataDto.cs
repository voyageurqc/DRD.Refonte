// ============================================================================
// Projet                         DRD.Application
// Nom du fichier                 EntityMetadataDto.cs
// Type de fichier                Classe C# (DTO)
// Classe                         EntityMetadataDto
// Emplacement                    Popup/Models
// Entités concernées             IAuditableEntity
// Créé le                        2025-12-08
//
// Description
//     Modèle de données destiné à transporter les métadonnées d’une entité
//     auditable vers la couche Web (modales, vues partielles, etc.).
//     Contient les informations d’audit, les identifiants des utilisateurs
//     et des champs additionnels pour l’état et la sécurité.
//
// Fonctionnalité
//     - Transporter les dates et auteurs de création et modification.
//     - Offrir des variantes (Name) pour l’affichage UI.
//     - Supporter l’état actif/inactif.
//     - Supporter les niveaux de sécurité.
//
// Modifications
//     2025-12-08    Ajout entête DRD v10 et organisation en régions.
// ============================================================================

using System;

namespace DRD.Application.Popup.Models
{
	/// <summary>
	/// DTO contenant l'ensemble des métadonnées d'une entité auditable.
	/// </summary>
	public class EntityMetadataDto
	{
		// ============================================================================
		#region Création
		/// <summary>
		/// Date UTC de création de l'entité.
		/// </summary>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Identifiant technique de l’utilisateur ayant créé l'entité.
		/// </summary>
		public string? CreatedBy { get; set; }

		/// <summary>
		/// Nom complet de l’utilisateur ayant créé l'entité.
		/// </summary>
		public string? CreatedByName { get; set; }
		#endregion
		// ============================================================================

		// ============================================================================
		#region Dernière modification
		/// <summary>
		/// Date UTC de la dernière modification.
		/// </summary>
		public DateTime ModificationDate { get; set; }

		/// <summary>
		/// Identifiant technique du dernier modificateur.
		/// </summary>
		public string? UpdatedBy { get; set; }

		/// <summary>
		/// Nom complet du dernier modificateur.
		/// </summary>
		public string? UpdatedByName { get; set; }
		#endregion
		// ============================================================================

		// ============================================================================
		#region État et sécurité
		/// <summary>
		/// Indique si l'entité est active.
		/// </summary>
		public bool IsActive { get; set; }
		#endregion
		// ============================================================================
	}
}
