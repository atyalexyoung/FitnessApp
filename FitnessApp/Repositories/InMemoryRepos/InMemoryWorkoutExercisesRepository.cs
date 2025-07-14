using FitnessApp.Interfaces.Repositories;
using FitnessApp.Shared.Models;

namespace FitnessApp.Repositories.InMemoryRepos
{
    public class InMemoryWorkoutExerciseRepository : IWorkoutExerciseRepository
    {
        public Task<bool> AddAsync(WorkoutExercise workoutExercise, string userId)
        {
            var workout = SampleData.GetWorkout(workoutExercise.WorkoutId);
            if (workout == null || workout.UserId != userId) return Task.FromResult(false);

            workout.WorkoutExercises.Add(workoutExercise);
            return Task.FromResult(true);
        }

        public Task<bool> RemoveAsync(string workoutId, string exerciseId, string userId)
        {
            var workout = SampleData.GetWorkout(workoutId);
            if (workout == null || workout.UserId != userId) return Task.FromResult(false);

            var toRemove = workout.WorkoutExercises.FirstOrDefault(e => e.Id == exerciseId);
            if (toRemove == null) return Task.FromResult(false);

            workout.WorkoutExercises.Remove(toRemove);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<WorkoutExercise>> GetAllAsync(string workoutId, string userId)
        {
            var workout = SampleData.GetWorkout(workoutId);
            if (workout == null || workout.UserId != userId)
                return Task.FromResult(Enumerable.Empty<WorkoutExercise>());

            return Task.FromResult(workout.WorkoutExercises.AsEnumerable());
        }

        public Task<WorkoutExercise?> GetByIdAsync(string workoutId, string exerciseId, string userId)
        {
            var workout = SampleData.GetWorkout(workoutId);
            if (workout == null || workout.UserId != userId)
                return Task.FromResult<WorkoutExercise?>(null);

            var exercise = workout.WorkoutExercises.FirstOrDefault(e => e.Id == exerciseId);
            return Task.FromResult(exercise);
        }
    }
}
