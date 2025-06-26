using FitnessApp.Shared.Models;

namespace FitnessApp.Interfaces.Repositories
{
    public interface IWorkoutsRepository
    {
        Task<IEnumerable<Workout>> GetAllAsync(string userId);
        Task<Workout?> GetByIdAsync(string workoutId, string userId);
        Task<Workout> CreateAsync(Workout workout, string userId);
        Task<bool> UpdateAsync(string workoutId, Workout workout, string userId);
        Task<bool> DeleteAsync(string workoutId, string userId);
    }
}
