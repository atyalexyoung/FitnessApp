using FitnessApp.Data;
using FitnessApp.Interfaces.Repositories;
using FitnessApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Repositories
{
    public class WorkoutsReposity : IWorkoutsRepository
    {
        private readonly ILogger _logger;
        private readonly FitnessAppDbContext _dbContext;

        public WorkoutsReposity(ILogger<WorkoutsReposity> logger, FitnessAppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        
        public async Task<Workout?> CreateAsync(Workout workout, string userId)
        {
            try
            {
                _dbContext.Workouts.Add(workout);
                await _dbContext.SaveChangesAsync();
                return workout;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error when creatign workout with id: {id} from database with exception: {ex}", workout.Id, ex);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(string workoutId, string userId)
        {
            try
            {
                var workout = await _dbContext.Workouts.FirstOrDefaultAsync(w => w.Id == workoutId);
                if (workout == null) { return false; }
                _dbContext.Workouts.Remove(workout);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error when deleting workout with id: {id} from database with exception: {ex}", workoutId, ex);
                return false;
            }
        }

        public async Task<IEnumerable<Workout>> GetAllAsync(string userId)
        {
            try
            {
                return await _dbContext.Workouts.Where(w => w.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error when getting all workout for user with id: {user} from database with exception: {ex}", userId, ex);
                return [];
            }
        }

        public async Task<Workout?> GetByIdAsync(string workoutId, string userId)
            => await _dbContext.Workouts.FirstOrDefaultAsync(w => w.Id == workoutId && w.UserId == userId);

        public async Task UpdateAsync(Workout workout)
        {
            _dbContext.Workouts.Update(workout);
            await _dbContext.SaveChangesAsync();
        }
    }
}
