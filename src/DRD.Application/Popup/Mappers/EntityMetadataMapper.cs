// ============================================================================
// Projet                         DRD.Application
// Nom du fichier                 EntityMetadataMapper.cs
// Type de fichier                Classe utilitaire
// Classe                         EntityMetadataMapper
// Emplacement                    Popup/Mappers
// Entités concernées             IAuditableEntity, EntityMetadataDto
// Créé le                        2025-12-08
//
// Description
//     Fournit une extension permettant de convertir une entité auditable
//     en un DTO de métadonnées destiné à l'affichage (Modals ou Partials).
//
// Fonctionnalité
//     - Extraire les champs d’audit standard (Created/Modified).
//     - Transformer une entité métier en DTO UI (EntityMetadataDto).
//     - Faciliter l’appel dans les contrôleurs via une simple extension.
//
// Modifications
//     2025-12-08    Ajout entête DRD v10 + organisation en régions.
// ============================================================================

using DRD.Application.Popup.Models;
using DRD.Domain.Common;

namespace DRD.Application.Popup.Mappers
{
	/// <summary>
	/// Fournit une méthode d’extension pour produire un DTO contenant
	/// les métadonnées d’une entité auditable.
	/// </summary>
	public static class EntityMetadataMapper
	{
		// ============================================================================
		#region Conversion
		/// <summary>
		/// Convertit une entité implémentant <see cref="IAuditableEntity"/>
		/// vers un <see cref="EntityMetadataDto"/> utilisé par les vues et modales.
		/// </summary>
		/// <param name="entity">Entité auditable source.</param>
		/// <returns>DTO contenant les informations d’audit.</returns>
		/// <exception cref="ArgumentNullException">Si l’entité est nulle.</exception>
		public static EntityMetadataDto ToMetadataDto(this IAuditableEntity entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			return new EntityMetadataDto
			{
				CreationDate = entity.CreationDate,
				CreatedBy = entity.CreatedBy,
				ModificationDate = entity.ModificationDate,
				UpdatedBy = entity.UpdatedBy
			};
		}
		#endregion
		// ============================================================================
	}
}
