using FitnessApp.Extensions;
using FitnessApp.Interfaces.Services;
using FitnessApp.Shared.DTOs.Requests;
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
            try
            {
                _logger.LogInformation("GET request for all workouts by {user}", UserId);

                var result = await _workoutsService.GetAllWorkoutsAsync(UserId);

                if (result == null)
                {
                    _logger.LogError("Got null result in controller for getting workouts for user with id: {user}", UserId ?? "NULL USER ID");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error when getting workouts.");
                }

                if (result.IsFailure)
                {
                    _logger.LogWarning("Failed to get workouts for user with id: {user} with error: {error}", UserId, result.Error);
                    return result.ToActionResult();
                }

                var response = result.Value;

                if (response == null) // shouldn't happen since service should take care of this, but doesn't hurt to check
                {
                    _logger.LogWarning("Null value for result when returning all workouts to user: {user}", UserId);
                    return NotFound("Null workouts.");
                }


                _logger.LogInformation("Responding to GET request for all workouts from user: {user} with {quantity} workouts", UserId, response.Count());
                return Ok(result.Value);
            
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when getting all workouts for user with id: {user}, with exception: {ex}", UserId, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error when getting workouts.");
            }
        }

        [Authorize]
        [HttpGet("workouts/{id}")]
        public async Task<IActionResult> GetWorkoutById(string id)
        {
            try
            {
                _logger.LogInformation("GET workout by ID of: {workout}, for {user}", id, UserId);

                if (string.IsNullOrWhiteSpace(id))
                {
                    _logger.LogWarning("Null or blank workout id for getting workout by id for user with id: {user}", UserId ?? "NULL USER ID");
                    return BadRequest("Null or blank workout ID");
                }

                var result = await _workoutsService.GetWorkoutByIdAsync(id, UserId);

                if (result == null)
                {
                    _logger.LogWarning("Could not find workout by id: {id} for user: {user}", id, UserId);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred.");
                }

                if (result.IsFailure)
                {
                    _logger.LogWarning("Failed to get workout by id: {}, for user: {user} with error: {error}", id, UserId, result.Error);
                    return result.ToActionResult();
                }

                var workout = result.Value;

                if (workout == null) // should never be null since checked in service, but doesn't hurt
                {
                    _logger.LogWarning("Received workout as null not get workout of id of: {id} from GET workout by ID for {user}", id, UserId);
                    return NotFound();
                }

                _logger.LogInformation("Returning workout with id of: {id} from GET workout by ID for {user}", workout.Id, UserId);
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when getting workout by id with workout id: {workout} for user with id: {user}, with exception: {ex}",id, UserId, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error when getting workouts.");
            }
        }

        [Authorize]
        [HttpPost("workouts")]
        public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutRequest workoutRequest)
        {
            try
            {
                _logger.LogInformation("POST request to create a workout with name {workoutRequestName} by user: {user}", workoutRequest.Name, UserId);

                if (workoutRequest == null)
                {
                    _logger.LogWarning("Null or blank workout information when requesting to create workout from user with id: {user}", UserId ?? "NULL USER ID");
                    return BadRequest("Null or empty workout when requesting workout creation.");
                }

                var result = await _workoutsService.CreateWorkoutAsync(workoutRequest, UserId);

                if (result == null)
                {
                    _logger.LogError("Null result for response to POST to create workout with name: {workout} for user with id: {user}", workoutRequest?.Name ?? "NULL NAME", UserId ?? "NULL USER ID");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error when creating workout");
                }

                if (result.IsFailure)
                {
                    _logger.LogWarning("Failed to create workout with name: {name}, for user: {user} with error: {error}", workoutRequest?.Name ?? "NULL NAME", UserId ?? "NULL USER ID", result.Error ?? "NULL RESULT ERROR");
                    return result.ToActionResult();
                }

                var response = result.Value;

                if (response == null)
                {
                    _logger.LogWarning("Received workout as null from POST workout with name: {name} for user with id: {user}", workoutRequest?.Name ?? "NULL NAME", UserId ?? "NULL USER ID");
                    return NotFound();
                }

                return CreatedAtAction(nameof(GetWorkoutById), new { id = response.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when creating workout with name: {name} for user: {user} with exception: {ex}", UserId ?? "NULL USER ID", workoutRequest?.Name ?? "NULL WORKOUT NAME", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error when creating workout");
            }
        }

        [Authorize]
        [HttpDelete("workouts/{id}")]
        public async Task<IActionResult> DeleteWorkout(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    _logger.LogWarning("Null or empty workout id provided to delete workout.");
                    return BadRequest("Null or empty workout id provided to delete workout");
                }

                var result = await _workoutsService.DeleteWorkoutAsync(id, UserId);

                if (result == null)
                {
                    _logger.LogWarning("Null result from deleting workout request from endpoint with workout with id: {workout} for user with id: {user}", id, UserId);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error when deleting workout");
                }

                if (result.IsFailure)
                {
                    _logger.LogWarning("Failed to delete workout with id: {workout}, for user: {user} with error: {error}", id, UserId, result.Error ?? "NULL RESULT ERROR");
                    return result.ToActionResult();
                }

                var response = result.Value;

                if (!response)
                {
                    _logger.LogWarning("Failed to delete workout with id: {workout}, for user: {user} with error: {error}", id, UserId, result.Error ?? "NULL RESULT ERROR");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete workout.");
                }

                return NoContent();                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when deleting workout with id: {workout} for user with id: {user} with exception {ex}", id ?? "NULL WORKOUT ID", UserId ?? "NULL USER ID", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error when deleting workout");
            }
        }

        [Authorize]
        [HttpGet("workouts/{workoutId}/exercises")]
        public async Task<IActionResult> GetExercisesByWorkout(string workoutId)
        {
            try
            {
                _logger.LogInformation("GET request for exercises in workout of id: {id} for user of id: {user}", workoutId, UserId);

                if (string.IsNullOrWhiteSpace(workoutId))
                {
                    _logger.LogError("Null or blank workout ID provided to get all exercises for workout for user: {user}", UserId ?? "NULL ID");
                    return BadRequest("Null or blank workout ID provided");
                }

                var result = await _workoutsService.GetExercisesInWorkoutAsync(workoutId, UserId);

                if (result == null)
                {
                    _logger.LogWarning("Result from service was null in endpoint handler.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred.");
                }

                if (result.IsFailure)
                {
                    _logger.LogWarning("Could not get the exercises for workout with id: {workout}, for user with id: {user} with error: {error}", workoutId, UserId, result.Error);
                    return result.ToActionResult();
                }

                var exercises = result.Value;
                if (exercises == null) // shouldn't be since service checks, but doesn't hurt
                {
                    _logger.LogWarning("No exercises found with workout id: {id} and user id: {id}", workoutId, UserId);
                    return NotFound("No exercises found.");
                }

                _logger.LogInformation("Responding to GET for exercises for workout with id: {workout}, for user with id: {user} \nTotal exercises returned: {count}", workoutId, UserId, exercises.Count());
                return Ok(exercises);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when getting exercises for workout with id: {workout} for user with id: {user} with exception: \n{ex}", workoutId ?? "NULL WORKOUT ID", UserId, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error when getting exercises for workout.");
            }
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
            var exercise = await _workoutsService.GetWorkoutExerciseAsync(workoutId, exerciseId, UserId);
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
