// ============================================================================
// Projet:      DRD.Infrastructure
// Fichier:     CurrentUserService.cs
// Type:        Service technique
// Classe:      CurrentUserService
// Emplacement: Services/
// Entité(s):   (aucune)
// Créé le:     2025-12-12
//
// Description
//     Service technique permettant d’obtenir l’utilisateur courant à partir
//     du HttpContext. Utilisé notamment pour l’audit (CreatedBy / UpdatedBy)
//     dans ApplicationDbContext.
//
// Fonctionnalité
//     - Déterminer si un utilisateur est authentifié.
//     - Exposer l’identifiant utilisateur (UserId).
//     - Exposer une valeur stable et fiable pour l’audit (UserName),
//       basée sur les claims ASP.NET Identity.
//
// Modifications
//     2025-12-15    Lecture dynamique des claims (Email / NameIdentifier)
//                 Correction du problème "Anonymous".
// ============================================================================

using DRD.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DRD.Infrastructure.Services
{
	/// <summary>
	/// Fournit l’utilisateur courant à partir du HttpContext.
	/// </summary>
	public class CurrentUserService : ICurrentUserService
	{
		#region Champs privés

		private readonly IHttpContextAccessor _httpContextAccessor;

		#endregion

		#region Constructeur

		/// <summary>
		/// Initialise une nouvelle instance de <see cref="CurrentUserService"/>.
		/// </summary>
		/// <param name="httpContextAccessor">Accès au HttpContext courant.</param>
		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		#endregion

		#region Propriétés exposées

		/// <inheritdoc />
		public bool IsAuthenticated =>
			_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

		/// <inheritdoc />
		public string UserId =>
			_httpContextAccessor.HttpContext?.User?
				.FindFirstValue(ClaimTypes.NameIdentifier)
			?? "SYSTEM";

		/// <inheritdoc />
		public string UserName
		{
			get
			{
				var user = _httpContextAccessor.HttpContext?.User;

				if (user == null || user.Identity?.IsAuthenticated != true)
					return "SYSTEM";

				// 1️⃣ FullName personnalisé (si tu l’as ajouté)
				var fullName = user.FindFirstValue("FullName");
				if (!string.IsNullOrWhiteSpace(fullName))
					return fullName;

				// 2️⃣ Prénom + Nom (claims standards)
				var firstName = user.FindFirstValue(ClaimTypes.GivenName);
				var lastName = user.FindFirstValue(ClaimTypes.Surname);

				if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
					return $"{firstName} {lastName}";

				// 3️⃣ Email (fallback lisible)
				var email = user.FindFirstValue(ClaimTypes.Email);
				if (!string.IsNullOrWhiteSpace(email))
					return email;

				// 4️⃣ Ultime fallback
				return "SYSTEM";
			}
		}
		#endregion
	}
}
