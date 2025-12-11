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
//     - Extrait les dates et identifiants de création/modification.
//     - Mappe IsActive et SecurityLevel si disponibles.
//     - Prépare la structure pour un futur enrichissement (noms complets).
//
// Modifications
//     2025-12-12    Version DRD v10 complète : header, XML, mappage intégral,
//                   ajout TODO pour récupération des noms utilisateurs.
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
		/// </summary>
		/// <param name="entity">Entité auditable source.</param>
		/// <returns>DTO prêt pour affichage dans la modale système.</returns>
		public static EntityMetadataDto ToMetadataDto(this IAuditableEntity entity)
		{
			var dto = new EntityMetadataDto
			{
				CreationDate = entity.CreationDate,
				CreatedBy = entity.CreatedBy,
				ModificationDate = entity.ModificationDate,
				UpdatedBy = entity.UpdatedBy,

				// DRD v10 — valeurs additionnelles
				IsActive = entity.IsActive,
			};

			// ==================================================================
			// TODO DRD — Récupérer les noms complets CreatedByName/UpdatedByName
			//
			// Requiert :
			// - ICurrentUserService
			// - Ou UserRepository.GetUserNameById(...)
			//
			// dto.CreatedByName = ...
			// dto.UpdatedByName = ...
			// ==================================================================

			return dto;
		}
	}
}
