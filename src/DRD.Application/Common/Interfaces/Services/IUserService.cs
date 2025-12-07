using System;
using System.Threading.Tasks;

namespace DRD.Application.Common.Interfaces.Services
{
    public interface IUserService
    {
        Task<string?> GetUserNameByIdAsync(string? userId);
    }
}