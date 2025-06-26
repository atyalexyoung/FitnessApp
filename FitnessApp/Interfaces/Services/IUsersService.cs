using FitnessApp.Shared.Models;

namespace FitnessApp.Interfaces.Services
{
    public interface IUsersService
    {
        Task<User?> RegisterAsync(string username, string password);
        Task<string?> LoginAsync(string username, string password);
        Task<User?> GetByIdAsync(string id);
    }
}
