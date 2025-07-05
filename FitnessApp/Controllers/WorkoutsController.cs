using FitnessApp.Interfaces.Services;
using FitnessApp.Shared.DTOs.Requests;
using FitnessApp.Shared.DTOs.Responses;
using FitnessApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutsController : BaseController
    {
        private readonly ILogger<WorkoutsController> _logger;
        private readonly IWorkoutsService _workoutsService;

        public WorkoutsController(ILogger<WorkoutsController> logger, IWorkoutsService workoutsService)
        {
            _logger = logger;
            _workoutsService = workoutsService;
        }

        [Authorize]
        [HttpGet("workouts")]
        public async Task<IActionResult> GetAllWorkouts()
        {
            _logger.LogInformation("GET request for all workouts by {user}", UserId);

            var result = await _workoutsService.GetAllWorkoutsAsync(UserId);
            if (result.IsFailure)
            {
                _logger.LogWarning("GET for all workouts from user: {user}, was unable to be successfully retrieved with error of: {error}", UserId, result.Error);
                return NotFound(result.Error);
            }
            else
            {
                if (result.Value == null)
                {
                    _logger.LogWarning("Null value for result when returning all workouts to user: {user}", UserId);
                    return NotFound("Null workouts.");
                }
                else
                {
                    if (result.Value is IEnumerable<WorkoutResponse> responses)
                    {
                        _logger.LogDebug("Responding to GET request for all workouts from user: {user} with {quantity} workouts", UserId, responses.Count());
                        return Ok(result.Value);
                    }
                    else
                    {
                        _logger.LogError("Unexpected type for result.Value in GetAllWorkouts. Actual type: {type}", result.Value.GetType());
                        return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred.");
                    }
                }
            }
        }

        [Authorize]
        [HttpGet("workouts/{id}")]
        public async Task<IActionResult> GetWorkoutById(string id)
        {
            _logger.LogInformation("GET workout by ID of: {workout}, for {user}", id, UserId);

            var workout = await _workoutsService.GetWorkoutByIdAsync(id, UserId);

            if (workout == null)
            {
                _logger.LogWarning("Could not find workout by id: {id} for user: {user}", id, UserId);
                return NotFound();
            }
            else
            {
                _logger.LogDebug("Returning workout with id of: {id} from GET workout by ID for {user}", workout.Id, UserId);
                return Ok(workout);
            }
        }

        [Authorize]
        [HttpPost("workouts")]
        public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutRequest workoutRequest)
        {
            _logger.LogDebug("POST request to create a workout with name {workoutRequestName} by user: {user}", workoutRequest.Name, UserId);

            var created = await _workoutsService.CreateWorkoutAsync(workoutRequest, UserId);
            return CreatedAtAction(nameof(GetWorkoutById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpDelete("workouts/{id}")]
        public async Task<IActionResult> DeleteWorkout(string id)
        {
            if (await _workoutsService.DeleteWorkoutAsync(id, UserId))
            {
                _logger.LogDebug("Workout with id: {id} was successfully deleted.", id);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("workouts/{workoutId}/exercises")]
        public async Task<IActionResult> GetExercisesByWorkout(string workoutId)
        {
            var workout = await _workoutsService.GetWorkoutByIdAsync(workoutId, UserId);
            if (workout == null) return NotFound();

            return Ok(workout.WorkoutExercises);
        }

        [Authorize]
        [HttpPost("workouts/{workoutId}/exercises")]
        public async Task<IActionResult> AddExerciseToWorkout(string workoutId, [FromBody] WorkoutExercise exercise)
        {
            if (exercise == null) return BadRequest();

            var success = await _workoutsService.AddExerciseToWorkoutAsync(workoutId, exercise, UserId);

            if (!success) { return NotFound(); }
            return CreatedAtAction(nameof(GetExerciseInWorkout), new { workoutId, exerciseId = exercise.Id }, exercise);
        }

        [Authorize]
        [HttpGet("workouts/{workoutId}/exercises/{exerciseId}")]
        public async Task<IActionResult> GetExerciseInWorkout(string workoutId, string exerciseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exercise = await _workoutsService.GetExerciseInWorkout(workoutId, exerciseId, UserId);
            if (exercise == null) return NotFound();
            return Ok(exercise);
        }

        [Authorize]
        [HttpDelete("workouts/{workoutId}/exercises/{exerciseId}")]
        public async Task<IActionResult> DeleteExerciseFromWorkout(string workoutId, string exerciseId)
        {
            // try to get workout
            var success = await _workoutsService.RemoveExerciseFromWorkoutAsync(workoutId, exerciseId, UserId);

            if (!success) { return NotFound(exerciseId); }
            return NoContent();
        }
    }
}
