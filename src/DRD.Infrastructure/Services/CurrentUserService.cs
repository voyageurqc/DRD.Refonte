// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 CurrentUserService.cs
// Type de fichier                Classe C#
// Classe                         CurrentUserService
// Emplacement                    Services
// Entités concernées             (Service transversal - audit utilisateur)
// Créé le                        2025-12-07
//
// Description
//     Implémentation du service utilisateur permettant d'obtenir des informations
//     relatives à l'utilisateur courant via HttpContext. Utilisé par la couche
//     Application pour l'audit (CreatedBy/UpdatedBy) et pour les opérations
//     nécessitant l'identité de l'utilisateur connecté.
//
// Fonctionnalité
//     - Lire les informations d'identité depuis HttpContext.
//     - Fournir UserId et UserName conformément aux règles DRD.
//     - Retourner "Anonymous" si aucun utilisateur n'est connecté.
//     - Supporter la couche Application sans dépendance inverse.
//
// Modifications
//     2025-12-07    Version initiale DRD v10.
// ============================================================================

using DRD.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DRD.Infrastructure.Services
{
	/// <summary>
	/// Service permettant d'accéder aux informations de l'utilisateur courant.
	/// </summary>
	public class CurrentUserService : ICurrentUserService
	{
		#region Constructeur et initialisation

		/// <summary>
		/// Initialise une nouvelle instance du service utilisateur basé sur HttpContext.
		/// </summary>
		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			var principal = httpContextAccessor.HttpContext?.User;

			if (principal?.Identity?.IsAuthenticated == true)
			{
				IsAuthenticated = true;

				UserId = principal.FindFirstValue(ClaimTypes.NameIdentifier)
						 ?? "Anonymous";

				UserName = principal.Identity?.Name
						   ?? "Anonymous";
			}
			else
			{
				IsAuthenticated = false;
				UserId = "Anonymous";
				UserName = "Anonymous";
			}
		}
		#endregion

		#region Propriétés exposées

		/// <inheritdoc />
		public string UserId { get; }

		/// <inheritdoc />
		public string UserName { get; }

		/// <inheritdoc />
		public bool IsAuthenticated { get; }

		#endregion
	}
}
