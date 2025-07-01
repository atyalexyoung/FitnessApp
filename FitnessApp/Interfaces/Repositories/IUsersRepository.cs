using FitnessApp.Shared.Models;

namespace FitnessApp.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> AddAsync(User user);
    }
}
