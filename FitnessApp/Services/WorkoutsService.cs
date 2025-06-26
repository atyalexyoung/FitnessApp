using FitnessApp.Interfaces.Repositories;
using FitnessApp.Interfaces.Services;
using FitnessApp.Shared.DTOs.Requests;
using FitnessApp.Shared.DTOs.Responses;
using FitnessApp.Shared.Mappers;
using FitnessApp.Shared.Models;

namespace FitnessApp.Services
{
    public class WorkoutsService : IWorkoutsService
    {
        private readonly IWorkoutsRepository _workoutsRepo;
        private readonly IWorkoutExerciseRepository _workoutExercises;

        public WorkoutsService(IWorkoutsRepository workoutsRepository, IWorkoutExerciseRepository workoutExerciseRepository)
        {
            _workoutsRepo = workoutsRepository;
            _workoutExercises = workoutExerciseRepository;
        }

        // ------------------------------------------------------------------------------------ Workouts
        public async Task<IEnumerable<WorkoutResponse>> GetAllWorkoutsAsync(string userId)
        {
            var workouts = await _workoutsRepo.GetAllAsync(userId);
            return workouts.Select(w => w.ToResponse());
        }

        public Task<Workout?> GetWorkoutByIdAsync(string workoutId, string userId) =>
            _workoutsRepo.GetByIdAsync(workoutId, userId);

        public Task<Workout> CreateWorkoutAsync(CreateWorkoutRequest request, string userId)
        {
            var workout = new Workout
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
                WorkoutExercises = new List<WorkoutExercise>()
            };

            return _workoutsRepo.CreateAsync(workout, userId);
        }
        public Task<bool> UpdateWorkoutAsync(string workoutId, Workout workout, string userId) =>
            _workoutsRepo.UpdateAsync(workoutId, workout, userId);
        public Task<bool> DeleteWorkoutAsync(string workoutId, string userId) =>
            _workoutsRepo.DeleteAsync(workoutId, userId);

        // ------------------------------------------------------------------------------ Workout Exercises

        public Task<IEnumerable<WorkoutExercise>> GetExerciesFromWorkout(string workoutId, string userId) =>
            _workoutExercises.GetAllAsync(workoutId, userId);
        public Task<WorkoutExercise?> GetExerciseInWorkout(string workoutId, string exerciseId, string userId) =>
            _workoutExercises.GetByIdAsync(workoutId, exerciseId, userId);
        public Task<bool> AddExerciseToWorkoutAsync(string workoutId, WorkoutExercise workoutExercise, string userId) =>
            _workoutExercises.AddAsync(workoutId, workoutExercise, userId);
        public Task<bool> RemoveExerciseFromWorkoutAsync(string workoutId, string exerciseId, string userId) =>
            _workoutExercises.RemoveAsync(workoutId, exerciseId, userId);
    }
}
