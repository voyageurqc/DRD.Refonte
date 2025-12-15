// ============================================================================
// Projet						DRD.Infrastructure
// Fichier						ApplicationUserClaimsPrincipalFactory.cs
// Type							Service Identity
// Classe						ApplicationUserClaimsPrincipalFactory
// Emplacement					Identity/
// Entité(s)					ApplicationUser
// Créé le						2025-12-15
//
// Description
//   Factory Identity personnalisée permettant d’enrichir le ClaimsPrincipal
//   avec des informations utilisateur supplémentaires (prénom, nom, nom
//   complet) à partir de l’entité ApplicationUser.
//
// Fonctionnalité
//   - Ajouter les claims GivenName, Surname et FullName.
//   - Rendre ces informations disponibles pour l’audit, l’UI et la journalisation.
//   - Centraliser la construction de l’identité utilisateur DRD.
//
// Modifications
//   2025-12-15		Création initiale – ajout des claims utilisateur (FullName).
// ============================================================================

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace DRD.Infrastructure.Identity
{
	public class ApplicationUserClaimsPrincipalFactory
		: UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
	{
		public ApplicationUserClaimsPrincipalFactory(
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager,
			IOptions<IdentityOptions> optionsAccessor)
			: base(userManager, roleManager, optionsAccessor)
		{
		}

		protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
		{
			var identity = await base.GenerateClaimsAsync(user);

			// Prénom
			if (!string.IsNullOrWhiteSpace(user.FirstName))
			{
				identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
			}

			// Nom
			if (!string.IsNullOrWhiteSpace(user.LastName))
			{
				identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
			}

			// FullName (recommandé)
			var fullName = $"{user.FirstName} {user.LastName}".Trim();
			if (!string.IsNullOrWhiteSpace(fullName))
			{
				identity.AddClaim(new Claim("FullName", fullName));
			}

			return identity;
		}
	}
}
