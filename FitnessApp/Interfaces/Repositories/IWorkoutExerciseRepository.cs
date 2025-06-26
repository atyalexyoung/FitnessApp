using FitnessApp.Shared.Models;

namespace FitnessApp.Interfaces.Repositories
{
    public interface IWorkoutExerciseRepository
    {
        Task<bool> AddAsync(string workoutId, WorkoutExercise workoutExercise, string userId);
        Task<bool> RemoveAsync(string workoutId, string exerciseId, string userId);
        Task<IEnumerable<WorkoutExercise>> GetAllAsync(string workoutId, string userId);
        Task<WorkoutExercise?> GetByIdAsync(string workoutId, string exerciseId, string userId);
    }
}
