// ============================================================================
// Projet                         DRD.Application
// Nom du fichier                 EntityMetadataMapper.cs
// Type                           Classe utilitaire (Mapper)
// Classe                         EntityMetadataMapper
// Emplacement                    Popup/Mappers
// Entité(s) concernées           EntityMetadataDto, IAuditableEntity
// Créé le                        2025-12-12
//
// Description
//     Convertit une entité auditable Domain en DTO EntityMetadataDto,
//     utilisé dans les vues Details/Edit et la modale _SystemMetadataPartial.
//
// Fonctionnalité
//     - Mappe les informations d’audit (dates, identifiants techniques).
//     - Mappe les valeurs d’affichage déjà résolues côté service.
//     - Ne contient aucune logique de résolution utilisateur.
//
// Modifications
//     2025-12-16    Suppression du TODO ; clarification des responsabilités.
//                   Le mapper reçoit des valeurs déjà prêtes à afficher
//                   (AXE 1 – Métadonnées).
// ============================================================================

using DRD.Application.Popup.Models;
using DRD.Domain.Common;

namespace DRD.Application.Popup.Mappers
{
	/// <summary>
	/// Mapper utilitaire permettant de convertir un objet <see cref="IAuditableEntity"/>
	/// en <see cref="EntityMetadataDto"/> pour affichage dans les vues DRD.
	/// </summary>
	public static class EntityMetadataMapper
	{
		/// <summary>
		/// Convertit l'entité auditable en DTO contenant les métadonnées système.
		/// Les valeurs d'affichage utilisateur doivent déjà être résolues.
		/// </summary>
		/// <param name="entity">Entité auditable source.</param>
		/// <param name="createdByName">Nom complet du créateur.</param>
		/// <param name="createdByDisplay">Valeur prête à afficher pour le créateur.</param>
		/// <param name="updatedByName">Nom complet du modificateur.</param>
		/// <param name="updatedByDisplay">Valeur prête à afficher pour le modificateur.</param>
		/// <returns>DTO prêt pour affichage dans la modale système.</returns>
		public static EntityMetadataDto ToMetadataDto(
			this IAuditableEntity entity,
			string? createdByName,
			string? createdByDisplay,
			string? updatedByName,
			string? updatedByDisplay)
		{
			return new EntityMetadataDto
			{
				CreationDate = entity.CreationDate,
				CreatedBy = entity.CreatedBy,
				CreatedByName = createdByName,
				CreatedByDisplay = createdByDisplay,

				ModificationDate = entity.ModificationDate,
				UpdatedBy = entity.UpdatedBy,
				UpdatedByName = updatedByName,
				UpdatedByDisplay = updatedByDisplay,

				IsActive = entity.IsActive
			};
		}
	}
}
