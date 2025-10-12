using FitnessApp.Data;
using FitnessApp.Interfaces.Repositories;
using FitnessApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ILogger _logger;
        private readonly FitnessAppDbContext _dbContext;

        public UsersRepository(ILogger<WorkoutsReposity> logger, FitnessAppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(User user, CancellationToken cancellationToken)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken)
            => await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
           => await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username, cancellationToken);
    }
}
