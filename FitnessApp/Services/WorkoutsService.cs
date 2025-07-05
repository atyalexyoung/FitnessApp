using FitnessApp.Helpers;
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
        public async Task<Result<IEnumerable<WorkoutResponse>>> GetAllWorkoutsAsync(string userId)
        {
            var workouts = await _workoutsRepo.GetAllAsync(userId);
            if (workouts == null)
            {
                return Result.Fail<IEnumerable<WorkoutResponse>>("Workouts returned as null.", ErrorType.NotFound);
            }
            var responses = workouts.Select(w => w.ToResponse());
            return Result.Ok(responses);
        }

        public async Task<WorkoutResponse?> GetWorkoutByIdAsync(string workoutId, string userId)
        {
            var workout = await _workoutsRepo.GetByIdAsync(workoutId, userId);
            if (workout == null)
            {
                return null;
            }
            var response = workout.ToResponse();
            return response;
        }

        public async Task<WorkoutResponse> CreateWorkoutAsync(CreateWorkoutRequest request, string userId)
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

            var returnedWorkout = await _workoutsRepo.CreateAsync(workout, userId);
            var response = returnedWorkout.ToResponse();
            return response;

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
