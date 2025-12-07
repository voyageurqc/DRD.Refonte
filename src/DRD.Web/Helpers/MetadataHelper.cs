// ============================================================================
// Projet                         DRD.Web
// Nom du fichier                 MetadataHelper.cs
// Type de fichier                Classe utilitaire
// Classe                         MetadataHelper
// Emplacement                    Controllers/Helpers
// Entités concernées             EntityMetadataDto
// Créé le                        2025-12-08
//
// Description
//     Méthodes utilitaires permettant de construire un DTO de métadonnées
//     à partir des informations d’audit d’une entité (dates + utilisateurs).
//     Utilisé par les vues Edit et Details pour alimenter les modales Metadata.
//
// Fonctionnalité
//     - Obtenir le nom complet d’un utilisateur via IUserService.
//     - Composer un EntityMetadataDto complet et bilingue.
//     - Centraliser la logique d’accès aux métadonnées afin d’éviter la duplication.
//
// Modifications
//     2025-12-08    Version DRD v10 – ajout entête + documentation + régions.
// ============================================================================

using System;
using System.Threading.Tasks;
using DRD.Application.Common.Interfaces.Services;
using DRD.Application.Popup.Models;

namespace DRD.Web.Controllers.Helpers
{
	/// <summary>
	/// Fournit des méthodes utilitaires pour construire un DTO de métadonnées
	/// (EntityMetadataDto) destiné aux vues et modales.
	/// </summary>
	public static class MetadataHelper
	{
		// ============================================================================
		#region Construction DTO
		/// <summary>
		/// Construit un <see cref="EntityMetadataDto"/> en interrogeant
		/// le service utilisateur afin d’obtenir les noms complets.
		/// </summary>
		/// <param name="userService">Service utilisateur pour résolution des noms.</param>
		/// <param name="creationDate">Date de création.</param>
		/// <param name="createdBy">Identifiant du créateur.</param>
		/// <param name="modificationDate">Date de dernière modification.</param>
		/// <param name="updatedBy">Identifiant du dernier modificateur.</param>
		/// <returns>Un DTO complet contenant les métadonnées d’une entité.</returns>
		public static async Task<EntityMetadataDto> BuildMetadataAsync(
			IUserService userService,
			DateTime creationDate,
			string? createdBy,
			DateTime modificationDate,
			string? updatedBy)
		{
			if (userService == null)
				throw new ArgumentNullException(nameof(userService));

			// Nom du créateur
			string createdByName = "Inconnu";
			if (!string.IsNullOrEmpty(createdBy))
			{
				createdByName = await userService.GetUserNameByIdAsync(createdBy)
										?? "Inconnu";
			}

			// Nom du modificateur
			string updatedByName = "Inconnu";
			if (!string.IsNullOrEmpty(updatedBy))
			{
				updatedByName = await userService.GetUserNameByIdAsync(updatedBy)
										?? "Inconnu";
			}

			return new EntityMetadataDto
			{
				CreationDate = creationDate,
				CreatedBy = createdBy,
				CreatedByName = createdByName,
				ModificationDate = modificationDate,
				UpdatedBy = updatedBy,
				UpdatedByName = updatedByName
			};
		}
		#endregion
		// ============================================================================
	}
}
