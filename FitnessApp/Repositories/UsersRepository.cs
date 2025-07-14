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

        public async Task<bool> AddAsync(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error when adding user with id: {id} to database with exception: {ex}", user.Id, ex);
                return false;
            }
        }

        public async Task<User?> GetByIdAsync(string id)
            => await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> GetByUsernameAsync(string username)
           => await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}
