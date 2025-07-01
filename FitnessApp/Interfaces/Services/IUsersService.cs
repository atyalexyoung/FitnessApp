using FitnessApp.Helpers;
using FitnessApp.Shared.Models;

namespace FitnessApp.Interfaces.Services
{
    public interface IUsersService
    {
        Task<Result<User?>> RegisterAsync(string username, string password);
        Task<Result<string?>> LoginAsync(string username, string password);
        Task<User?> GetByIdAsync(string id);
    }
}
