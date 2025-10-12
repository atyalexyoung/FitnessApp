using FitnessApp.Shared.Models;

namespace FitnessApp.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
        Task<bool> AddAsync(User user, CancellationToken cancellationToken);
    }
}
