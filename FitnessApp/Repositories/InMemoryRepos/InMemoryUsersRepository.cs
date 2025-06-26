using FitnessApp.Interfaces.Repositories;
using FitnessApp.Shared.Models;

namespace FitnessApp.Repositories.InMemoryRepos
{
    public class InMemoryUsersRepository : IUsersRepository
    {
        private readonly List<User> _users = [];

        public Task<User?> GetByIdAsync(string id)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.UserName == username));
        }

        public Task AddAsync(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }
    }
}
