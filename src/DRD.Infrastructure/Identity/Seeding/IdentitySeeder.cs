// ============================================================================
// Projet                         DRD.Infrastructure
// Nom du fichier                 IdentitySeeder.cs
// Type de fichier                Identity Seeding
// Nature C#                      Static Class
// Emplacement                    Identity/Seeding
// Auteur                         Michel Gariépy
// Créé le                        2025-12-02
//
// Description
//     Classe responsable de créer les rôles, le type d’accès AdminSystem et
//     l’utilisateur administrateur système. Le seeding s’effectue uniquement
//     lorsque SeedAsync() est appelé au démarrage.
//
// Fonctionnalité
//     - Créer les rôles Identity DRD (AdminSystem, Admin, User).
//     - Créer le type d’accès ADMIN_SYS (table AccessType).
//     - Créer l’utilisateur administrateur initial selon SD.cs.
//     - Vérifier chaque élément pour éviter les doublons.
//     - Compatible avec IdentityDbContext<ApplicationUser>.
//
// Modifications
//     2025-12-02    Version finale unifiée SD.cs + IdentitySeeder.
// ============================================================================

using System;
using System.Threading.Tasks;
using DRD.Infrastructure.Data;
using DRD.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DRD.Infrastructure.Identity.Seeding
{
	/// <summary>
	/// Fournit le seeding initial pour Identity :
	/// rôles, type d’accès et administrateur système.
	/// </summary>
	public static class IdentitySeeder
	{
		/// <summary>
		/// Applique le seeding Identity :
		/// - rôles
		/// - AccessType
		/// - administrateur système
		/// </summary>
		public static async Task SeedAsync(
			RoleManager<IdentityRole> roleManager,
			UserManager<ApplicationUser> userManager,
			ApplicationDbContext context)
		{
			// -------------------------------------------------------------
			// 1. RÔLES IDENTITY
			// -------------------------------------------------------------
			if (!await roleManager.RoleExistsAsync(SD.Role_AdminSystem))
				await roleManager.CreateAsync(new IdentityRole(SD.Role_AdminSystem));

			if (!await roleManager.RoleExistsAsync(SD.Role_Admin))
				await roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));

			if (!await roleManager.RoleExistsAsync(SD.Role_User))
				await roleManager.CreateAsync(new IdentityRole(SD.Role_User));


			// -------------------------------------------------------------
			// 2. ACCESS TYPE (table AccessType)
			// -------------------------------------------------------------
			var accessType = await context.AccessTypes
				.FirstOrDefaultAsync(a => a.AccessTypeCode == SD.AccessType_AdminSystem);

			if (accessType == null)
			{
				var newAccessType = new AccessType(
					SD.AccessType_AdminSystem,
					"Administrateur système",
					"System administrator"
				);

				context.AccessTypes.Add(newAccessType);
				await context.SaveChangesAsync();
			}


			// -------------------------------------------------------------
			// 3. ADMIN USER
			// -------------------------------------------------------------
			var adminUser = await userManager.FindByEmailAsync(SD.Admin_Email);

			if (adminUser == null)
			{
				adminUser = new ApplicationUser
				{
					UserName = SD.Admin_Email,
					Email = SD.Admin_Email,
					FirstName = SD.Admin_FirstName,
					LastName = SD.Admin_LastName,
					AccessTypeCode = SD.AccessType_AdminSystem,
					SectorCode = "SYS",
					DefaultPrinter = "",
					LaserPrinter = "",
					IsActive = true,
					CreationDate = DateTime.UtcNow,
					ModificationDate = DateTime.UtcNow
				};

				var result = await userManager.CreateAsync(adminUser, SD.Admin_Password);

				if (!result.Succeeded)
				{
					var message = string.Join("; ", result.Errors);
					throw new Exception($"Échec de la création de l'utilisateur AdminSystem : {message}");
				}

				// Attribution du rôle AdminSystem
				await userManager.AddToRoleAsync(adminUser, SD.Role_AdminSystem);
			}
		}
	}
}
