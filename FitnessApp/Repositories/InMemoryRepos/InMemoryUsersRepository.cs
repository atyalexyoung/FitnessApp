using FitnessApp.Interfaces.Repositories;
using FitnessApp.Shared.Models;

namespace FitnessApp.Repositories.InMemoryRepos
{
    public class InMemoryUsersRepository : IUsersRepository
    {
        private readonly List<User> _users = [];
        private readonly ILogger<InMemoryExercisesRepository> _logger;

        public InMemoryUsersRepository(ILogger<InMemoryExercisesRepository> logger)
        {
            _logger = logger;
        }

        public Task<User?> GetByIdAsync(string id)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.UserName == username));
        }

        public Task<bool> AddAsync(User user)
        {
            try
            {
                _users.Add(user);
                _logger.LogTrace("User: {username}, successfully added in {class} at {time}", user.UserName, nameof(AddAsync), DateTime.UtcNow);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogTrace("Failed to add user: {username} with exception: {ex}}", user.UserName, ex);
                return Task.FromResult(false);
            }
        }
    }
}
