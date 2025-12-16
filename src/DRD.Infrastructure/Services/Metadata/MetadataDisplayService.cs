// ============================================================================
// Projet                         DRD.Application
// Nom du fichier                 MetadataDisplayService.cs
// Type                           Classe
// Classe                         MetadataDisplayService
// Emplacement                    Popup/Services/Metadata
// Entité(s) concernées           EntityMetadataDto, IAuditableEntity, ApplicationUser
// Créé le                        2025-12-16
//
// Description
//     Implémentation du service applicatif responsable de la construction
//     des métadonnées système destinées à l’affichage.
//
// Fonctionnalité
//     - Résout les utilisateurs (Identity) à partir des identifiants techniques.
//     - Applique le format d’affichage DRD : « Prénom Nom (email) ».
//     - Délègue le mapping final à EntityMetadataMapper.
//     - Ne contient aucune logique UI.
//
// Modifications
//     2025-12-16    Implémentation initiale (AXE 1 – Métadonnées).
// ============================================================================

using DRD.Application.Popup.Mappers;
using DRD.Application.Popup.Models;
using DRD.Application.Popup.Services.Metadata;
using DRD.Domain.Common;
using DRD.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace DRD.Infrastructure.Services.Metadata
{
	/// <summary>
	/// Service applicatif chargé de préparer les métadonnées système
	/// prêtes à afficher pour les entités auditables.
	/// </summary>
	public class MetadataDisplayService : IMetadataDisplayService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public MetadataDisplayService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		/// <inheritdoc />
		public async Task<EntityMetadataDto> BuildAsync(IAuditableEntity entity)
		{
			var createdUser = await ResolveUserAsync(entity.CreatedBy);
			var updatedUser = await ResolveUserAsync(entity.UpdatedBy);

			var createdDisplay = BuildDisplay(createdUser, entity.CreatedBy);
			var updatedDisplay = BuildDisplay(updatedUser, entity.UpdatedBy);

			return entity.ToMetadataDto(
				createdByName: createdDisplay,
				createdByDisplay: createdDisplay,
				updatedByName: updatedDisplay,
				updatedByDisplay: updatedDisplay
			);
		}

		#region Helpers

		private async Task<ApplicationUser?> ResolveUserAsync(string? email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return null;

			return await _userManager.FindByEmailAsync(email);
		}
		private static string? BuildDisplay(ApplicationUser? user, string? email)
		{
			if (user != null)
			{
				var firstName = user.FirstName?.Trim();
				var lastName = user.LastName?.Trim();

				var fullName = string.Join(" ",
					new[] { firstName, lastName }
					.Where(x => !string.IsNullOrWhiteSpace(x)));

				if (!string.IsNullOrWhiteSpace(fullName))
				{
					if (!string.IsNullOrWhiteSpace(email))
						return $"{fullName} ({email})";

					return fullName;
				}
			}

			return email;
		}

		#endregion
	}
}
