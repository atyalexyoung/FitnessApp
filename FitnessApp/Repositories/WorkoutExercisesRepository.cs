using FitnessApp.Data;
using FitnessApp.Interfaces.Repositories;
using FitnessApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Repositories
{
    public class WorkoutExercisesReposity : IWorkoutExerciseRepository
    {
        private readonly ILogger _logger;
        private readonly FitnessAppDbContext _dbContext;

        public WorkoutExercisesReposity(ILogger<WorkoutsReposity> logger, FitnessAppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(WorkoutExercise workoutExercise, string userId, CancellationToken cancellationToken)
        {
            _dbContext.WorkoutExercises.Add(workoutExercise);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<WorkoutExercise>> GetAllAsync(string workoutId, string userId, CancellationToken cancellationToken)
        {
            return await _dbContext.WorkoutExercises
                .Include(w => w.Workout)  // eager load Workout to avoid null
                .Where(w => w.Workout != null && w.WorkoutId == workoutId && w.Workout.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<WorkoutExercise?> GetByIdAsync(string workoutId, string workoutExerciseId, string userId, CancellationToken cancellationToken)
        {
            return await _dbContext.WorkoutExercises
                .Include(w => w.Workout)
                .FirstOrDefaultAsync(w =>
                    w.Id == workoutExerciseId &&
                    w.WorkoutId == workoutId &&
                    w.Workout != null &&
                    w.Workout.UserId == userId, cancellationToken);
        }

        public async Task<bool> RemoveAsync(string workoutId, string workoutExerciseId, string userId, CancellationToken cancellationToken)
        {
            var workoutExercise = await _dbContext.WorkoutExercises
                .Include(w => w.Workout)
                .FirstOrDefaultAsync(w =>
                    w.Id == workoutExerciseId &&
                    w.WorkoutId == workoutId &&
                    w.Workout != null &&
                    w.Workout.UserId == userId, cancellationToken);

            if (workoutExercise == null) { return false; }
            _dbContext.WorkoutExercises.Remove(workoutExercise);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
