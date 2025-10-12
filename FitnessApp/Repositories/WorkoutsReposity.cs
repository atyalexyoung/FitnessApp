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
        
        public async Task<Workout?> CreateAsync(Workout workout, CancellationToken cancellationToken)
        {
            _dbContext.Workouts.Add(workout);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return workout;
        }

        public async Task<bool> DeleteAsync(string workoutId, CancellationToken cancellationToken)
        {
            var workout = await _dbContext.Workouts.FirstOrDefaultAsync(w => w.Id == workoutId, cancellationToken);
            if (workout == null) { return false; }
            _dbContext.Workouts.Remove(workout);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<Workout>> GetAllAsync(string userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Workouts.Where(w => w.UserId == userId).ToListAsync(cancellationToken);
        }

        public async Task<Workout?> GetByIdAsync(string workoutId, string userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Workouts.FirstOrDefaultAsync(w => w.Id == workoutId && w.UserId == userId, cancellationToken);
        }

        public async Task UpdateAsync(Workout workout, CancellationToken cancellationToken)
        {
            _dbContext.Workouts.Update(workout);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
