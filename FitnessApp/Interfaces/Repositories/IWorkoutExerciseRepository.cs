using FitnessApp.Shared.Models;

namespace FitnessApp.Interfaces.Repositories
{
    public interface IWorkoutExerciseRepository
    {
        Task<bool> AddAsync(WorkoutExercise workoutExercise, string userId, CancellationToken cancellationToken);
        Task<bool> RemoveAsync(string workoutId, string exerciseId, string userId, CancellationToken cancellationToken);
        Task<IEnumerable<WorkoutExercise>> GetAllAsync(string workoutId, string userId, CancellationToken cancellationToken);
        Task<WorkoutExercise?> GetByIdAsync(string workoutId, string exerciseId, string userId, CancellationToken cancellationToken);
    }
}
