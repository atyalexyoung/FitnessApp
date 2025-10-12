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
        private readonly ILogger<WorkoutsService> _logger;
        private readonly IWorkoutsRepository _workoutsRepo;
        private readonly IWorkoutExerciseRepository _workoutExercisesRepo;

        public WorkoutsService(ILogger<WorkoutsService> logger, IWorkoutsRepository workoutsRepository, IWorkoutExerciseRepository workoutExerciseRepository)
        {
            _workoutsRepo = workoutsRepository;
            _workoutExercisesRepo = workoutExerciseRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<WorkoutResponse>>> GetAllWorkoutsAsync(string userId, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Getting all workouts for user with id: {user}", userId);
            var workouts = await _workoutsRepo.GetAllAsync(userId, cancellationToken);

            if (workouts == null)
            {
                _logger.LogWarning("Received null workouts. Returing Result.Fail with error not found.");
                return Result.Fail<IEnumerable<WorkoutResponse>>("Workouts returned as null.", ErrorType.NotFound);
            }

            var responses = workouts.Select(w => w.ToResponse());
            return Result.Ok(responses);
        }

        public async Task<Result<WorkoutResponse?>> GetWorkoutByIdAsync(string workoutId, string userId, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Getting workout with id: {workout}, for user with id: {user}", workoutId, userId);
            var workout = await _workoutsRepo.GetByIdAsync(workoutId, userId, cancellationToken);

            if (workout == null)
            {
                _logger.LogWarning("Null workout received when looking for workout with id: {workout}, for user with id: {user}", workoutId, userId);
                return Result.Fail<WorkoutResponse?>("No workout found.", ErrorType.NotFound);
            }

            var response = workout.ToResponse();
            return Result.Ok<WorkoutResponse?>(response);
        }

        public async Task<Result<WorkoutResponse>> CreateWorkoutAsync(CreateWorkoutRequest request, string userId, CancellationToken cancellationToken)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                _logger.LogWarning("Null request for creating workout or the name was null");
                return Result.Fail<WorkoutResponse>("Null request for creating workout or null/empty name.", ErrorType.Validation);
            }
            if (string.IsNullOrWhiteSpace(userId))
            {
                _logger.LogWarning("Null user ID when trying to create workout.");
                return Result.Fail<WorkoutResponse>("Null or blank user ID when trying to create workout", ErrorType.Validation);
            }

            _logger.LogDebug("Creating workout with name: {name} for user with id: {user}", request.Name, userId);

            var workout = new Workout
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
                WorkoutExercises = []
            };

            var returnedWorkout = await _workoutsRepo.CreateAsync(workout, cancellationToken);

            if (returnedWorkout == null)
            {
                return Result.Fail<WorkoutResponse>("Unexpected error when creating workout.", ErrorType.Internal);
            }

            var response = returnedWorkout.ToResponse();
            _logger.LogDebug("Created new workout with id: {id} and name: {name} for user: {user}", returnedWorkout.Id, returnedWorkout.Name, returnedWorkout.UserId);
            return Result.Ok(response);
        
        }

        public async Task<Result<IEnumerable<WorkoutExercise>>> GetExercisesInWorkoutAsync(string workoutId, string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(workoutId))
            {
                _logger.LogWarning("Null or empty workout ID provided to get exercises in workout for user: {user}", userId ?? "NULL USER ID");
                return Result.Fail<IEnumerable<WorkoutExercise>>("Null or blank workout ID provided.", ErrorType.Validation);
            }

            var exercises = await _workoutExercisesRepo.GetAllAsync(workoutId, userId, cancellationToken);

            if (exercises == null)
            {
                _logger.LogWarning("No exercises found for workout with id: {workout} for user with id: {user}", workoutId, userId);
                return Result.Fail<IEnumerable<WorkoutExercise>>("No exercises found for workout.", ErrorType.NotFound);
            }

            _logger.LogDebug("Returning exercises for workout with id: {workout} for user with ID of: {user}", workoutId, userId);
            return Result.Ok(exercises);
        }

        public async Task<Result<Workout>> UpdateWorkoutAsync(string workoutId, Workout workout, string userId, CancellationToken cancellationToken)
        {
            var existingWorkout = await _workoutsRepo.GetByIdAsync(workoutId, userId, cancellationToken);
            if (existingWorkout == null)
            {
                _logger.LogWarning("The workout being update with id: {workout} for user {user} could not be found.", workoutId, userId);
                return Result.Fail<Workout>("The workout being updated could not be found.", ErrorType.NotFound);
            }

            existingWorkout.UpdateFrom(workout);
            existingWorkout.LastModifiedAt = DateTime.UtcNow;

            await _workoutsRepo.UpdateAsync(existingWorkout, cancellationToken);
            return Result.Ok(existingWorkout);
        }

        public async Task<Result<bool>> DeleteWorkoutAsync(string workoutId, string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(workoutId) || string.IsNullOrWhiteSpace(userId))
            {
                _logger.LogWarning("Null or empty workout id or user id.");
                return Result.Fail<bool>("Null or empty workout ID or User ID provided", ErrorType.Validation);
            }

            var success = await _workoutsRepo.DeleteAsync(workoutId, cancellationToken);

            if (!success)
            {
                _logger.LogWarning("Failed to delete workout with id: {workout} for user with id: {user}", workoutId, userId);
                return Result.Fail<bool>("Unable to delete workout", ErrorType.Internal);
            }
            _logger.LogDebug("Returning a successful deletion of workout with id: {workout} for user {user}", workoutId, userId);
            return Result.Ok(success);
        }

        // ------------------------------------------------------------------------------ Workout Exercises

        public async Task<Result<WorkoutExercise>> GetWorkoutExerciseAsync(string workoutId, string workoutExerciseId, string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(workoutId) || string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(workoutExerciseId))
            {
                _logger.LogWarning("Null or empty workout id, workout exercise id, or user id.");
                return Result.Fail<WorkoutExercise>("Null or empty workout ID, workout exercise ID, or User ID provided", ErrorType.Validation);
            }

            var workoutExercise = await _workoutExercisesRepo.GetByIdAsync(workoutId, workoutExerciseId, userId, cancellationToken);
            if (workoutExercise == null)
            {
                _logger.LogWarning("Failed to get workout exercise with id: {workoutExercise} for workout with id: {workout} and user with id: {user}", workoutExerciseId, workoutId, userId);
                return Result.Fail<WorkoutExercise>("Unable to get workout exercise.", ErrorType.NotFound);
            }

            return Result.Ok(workoutExercise);
        }

        public async Task<Result<WorkoutExerciseResponse>> AddExerciseToWorkoutAsync(CreateWorkoutExerciseRequest workoutExerciseRequest, string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userId) || workoutExerciseRequest == null || workoutExerciseRequest.WorkoutId == null)
            {
                _logger.LogWarning("Null or empty workout id, workout exercise id, or user id.");
                return Result.Fail<WorkoutExerciseResponse>("Null or empty workout ID, workout exercise ID, or User ID provided", ErrorType.Validation);
            }

            var workout = await _workoutsRepo.GetByIdAsync(workoutExerciseRequest.WorkoutId, userId, cancellationToken);
            if (workout == null)
            {
                _logger.LogWarning("Failed to add exercise exercise to workout with id: {workout} and user with id: {user}", workoutExerciseRequest.WorkoutId, userId);
                return Result.Fail<WorkoutExerciseResponse>("Workout not found or does not belong to user", ErrorType.NotFound);
            }

            var workoutExercise = workoutExerciseRequest.ToEntity();

            var success = await _workoutExercisesRepo.AddAsync(workoutExercise, userId, cancellationToken);

            if (!success)
            {
                _logger.LogWarning("Failed to add exercise exercise with id: {workoutExercise} to workout with id: {workout} and user with id: {user}", workoutExercise.Id, workoutExercise.WorkoutId, userId);
                return Result.Fail<WorkoutExerciseResponse>("Unable to get workout exercise.", ErrorType.NotFound);
            }

            _logger.LogDebug("Successfully removed the workout exercise with id: {exercise} from workout with id: {workout} for user with id: {user}.", workoutExercise.Id, workoutExercise.WorkoutId, userId);
            var response = workoutExercise.ToResponse();
            return Result.Ok(response);
        }

        public async Task<Result<bool>> RemoveExerciseFromWorkoutAsync(string workoutId, string workoutExerciseId, string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(workoutId) || string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(workoutExerciseId))
            {
                _logger.LogWarning("Null or empty workout id, workout exercise id, or user id.");
                return Result.Fail<bool>("Null or empty workout ID, workout exercise ID, or User ID provided", ErrorType.Validation);
            }

            var success = await _workoutExercisesRepo.RemoveAsync(workoutId, workoutExerciseId, userId, cancellationToken);

            if (!success)
            {
                _logger.LogWarning("Failed to get workout exercise with id: {workoutExercise} for workout with id: {workout} and user with id: {user}", workoutExerciseId, workoutId, userId);
                return Result.Fail<bool>("Unable to get workout exercise.", ErrorType.NotFound);
            }

            _logger.LogDebug("Successfully removed the workout exercise with id: {exercise} from workout with id: {workout} for user with id: {user}.", workoutExerciseId, workoutId, userId);
            return Result.Ok(true);
        }
    }
}
