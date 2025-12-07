using DRD.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DRD.Application.Common.Interfaces.Services;

namespace DRD.Infrastructure.Common.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<ApplicationUser> userManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<string?> GetUserNameByIdAsync(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Attempted to find user with null or empty ID.");
                return null; // ou "Système" ou toute autre chaîne pour un utilisateur non trouvé/système
            }

            _logger.LogInformation("Attempting to find user with ID: {UserId}", userId);

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                _logger.LogWarning("User with ID: {UserId} not found.", userId);
                return null; // ou "Inconnu"
            }

            _logger.LogInformation("Found user: {FullName} for ID: {UserId}", user.FullName, userId);
            return user.FullName;
        }
    }
}