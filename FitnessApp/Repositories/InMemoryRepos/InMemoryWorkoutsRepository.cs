using FitnessApp.Interfaces.Repositories;
using FitnessApp.Shared.Models;

namespace FitnessApp.Repositories.InMemoryRepos
{
    public class InMemoryWorkoutsRepository : IWorkoutsRepository
    {
        public Task<IEnumerable<Workout>> GetAllAsync(string userId)
        {
            var workouts = SampleData.GetAllWorkouts().Where(w => w.UserId == userId);
            return Task.FromResult(workouts);
        }

        public Task<Workout?> GetByIdAsync(string workoutId, string userId)
        {
            var workout = SampleData.GetWorkout(workoutId);
            return Task.FromResult(workout?.UserId == userId ? workout : null);
        }

        public Task<Workout> CreateAsync(Workout workout, string userId)
        {
            workout.UserId = userId;
            var created = SampleData.AddWorkout(workout);
            return Task.FromResult(created);
        }

        public Task<bool> UpdateAsync(string workoutId, Workout workout, string userId)
        {
            // Assume this updates in your in-memory store
            // Add actual logic as needed
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(string workoutId, string userId)
        {
            return Task.FromResult(SampleData.DeleteWorkout(workoutId));
        }
    }
}
