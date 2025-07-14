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

        public async Task<bool> AddAsync(WorkoutExercise workoutExercise, string userId)
        {
            try
            {
                _dbContext.WorkoutExercises.Add(workoutExercise);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error when adding workout exercise: {workoutExercise} to database with exception: {ex}", workoutExercise, ex);
                return false;
            }
        }

        public async Task<IEnumerable<WorkoutExercise>> GetAllAsync(string workoutId, string userId)
        {
            try
            {
                return await _dbContext.WorkoutExercises
                    .Include(w => w.Workout)  // eager load Workout to avoid null
                    .Where(w => w.Workout != null && w.WorkoutId == workoutId && w.Workout.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error when getting all workout exercises for workout with id: {workout} for user with id: {user} from database with exception: {ex}", workoutId, userId, ex);
                return [];
            }
        }

        public async Task<WorkoutExercise?> GetByIdAsync(string workoutId, string workoutExerciseId, string userId)
        {
            try
            {
                return await _dbContext.WorkoutExercises
                    .Include(w => w.Workout)
                    .FirstOrDefaultAsync(w =>
                        w.Id == workoutExerciseId &&
                        w.WorkoutId == workoutId &&
                        w.Workout != null &&
                        w.Workout.UserId == userId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error when getting workout exercise with id: {workoutExerciseId} for workout id: {workoutId} and user id: {userId}. Exception: {ex}",
                    workoutExerciseId, workoutId, userId, ex);
                return null;
            }
        }

        public async Task<bool> RemoveAsync(string workoutId, string workoutExerciseId, string userId)
        {
            try
            {
                var workoutExercise = await _dbContext.WorkoutExercises
                    .Include(w => w.Workout)
                    .FirstOrDefaultAsync(w =>
                        w.Id == workoutExerciseId &&
                        w.WorkoutId == workoutId &&
                        w.Workout != null &&
                        w.Workout.UserId == userId);

                if (workoutExercise == null) { return false; }
                _dbContext.WorkoutExercises.Remove(workoutExercise);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error when deleting workout with id: {id} from database with exception: {ex}", workoutId, ex);
                return false;
            }
            throw new NotImplementedException();
        }
    }
}
