// ============================================================================
// Projet                         DRD.Application
// Nom du fichier                 EntityMetadataDto.cs
// Type                           DTO
// Classe                         EntityMetadataDto
// Emplacement                    Popup/Models
// Entité(s) concernées           Audit système (toutes entités auditables)
// Créé le                        2025-12-12
//
// Description
//     Objet de transfert utilisé pour afficher les métadonnées système d’une
//     entité auditable dans les vues (Details, Edit, partial _SystemMetadata).
//
// Fonctionnalité
//     - Transporte les informations d’audit : dates, usagers, noms complets.
//     - Supporte l’état Actif/Inactif et le niveau de sécurité DRD.
//     - Utilisé conjointement avec EntityMetadataMapper dans Popup/Mappers.
//
// Modifications
//     2025-12-12    Création DRD v10 complète (header, XML, namespaces cohérents).
// ============================================================================

using System;

namespace DRD.Application.Popup.Models
{
	/// <summary>
	/// Représente les métadonnées d’audit d’une entité DRD,
	/// incluant dates, utilisateurs et état actif/inactif.
	/// </summary>
	public class EntityMetadataDto
	{
		/// <summary>
		/// Date de création de l'entité.
		/// </summary>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Identifiant de l'utilisateur ayant créé l'entité.
		/// </summary>
		public string? CreatedBy { get; set; }

		/// <summary>
		/// Nom complet de l'utilisateur ayant créé l'entité.
		/// </summary>
		public string? CreatedByName { get; set; }

		/// <summary>
		/// Date de dernière modification.
		/// </summary>
		public DateTime ModificationDate { get; set; }

		/// <summary>
		/// Identifiant de l'utilisateur ayant modifié l'entité.
		/// </summary>
		public string? UpdatedBy { get; set; }

		/// <summary>
		/// Nom complet de l'utilisateur ayant modifié l'entité.
		/// </summary>
		public string? UpdatedByName { get; set; }

		/// <summary>
		/// Indique si l'entité est active (true) ou désactivée (false).
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// Niveau de sécurité assigné à l'entité.
		/// </summary>
		public int SecurityLevel { get; set; }
	}
}
