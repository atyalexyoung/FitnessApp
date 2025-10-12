using FitnessApp.Shared.Models;

namespace FitnessApp.Interfaces.Repositories
{
    public interface IWorkoutsRepository
    {
        Task<IEnumerable<Workout>> GetAllAsync(string userId, CancellationToken cancellationToken);
        Task<Workout?> GetByIdAsync(string workoutId, string userId, CancellationToken cancellationToken);
        Task<Workout?> CreateAsync(Workout workout, CancellationToken cancellationToken);
        Task UpdateAsync(Workout workout, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(string workoutId, CancellationToken cancellationToken);
    }
}
