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
        [HttpGet(Name = "GetAllWorkouts")]
        public async Task<IActionResult> GetAllWorkouts()
        {
            var workouts = await _workoutsService.GetAllWorkoutsAsync(UserId);
            return workouts == null
                ? NotFound()
                : Ok(workouts);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetWorkoutById")]
        public async Task<IActionResult> GetWorkoutById(string id)
        {
            var workout = await _workoutsService.GetWorkoutByIdAsync(id, UserId);

            return workout == null
                ? NotFound()
                : Ok(workout);
        }

        [Authorize]
        [HttpPost(Name = "CreateWorkout")]
        public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutRequest workoutRequest)
        {
            var created = await _workoutsService.CreateWorkoutAsync(workoutRequest, UserId);
            return CreatedAtAction(nameof(GetWorkoutById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeleteWorkout")]
        public async Task<IActionResult> DeleteWorkout(string id)
        {
            return await _workoutsService.DeleteWorkoutAsync(id, UserId)
                ? NoContent()
                : NotFound();
        }

        [Authorize]
        [HttpGet("{workoutId}/exercises", Name = "GetExercisesByWorkout")]
        public async Task<IActionResult> GetExercisesByWorkout(string workoutId)
        {
            var workout = await _workoutsService.GetWorkoutByIdAsync(workoutId, UserId);
            if (workout == null) return NotFound();

            return Ok(workout.WorkoutExercises);
        }

        [Authorize]
        [HttpPost("{workoutId}/exercises")]
        public async Task<IActionResult> AddExerciseToWorkout(string workoutId, [FromBody] WorkoutExercise exercise)
        {
            if (exercise == null) return BadRequest();

            var success = await _workoutsService.AddExerciseToWorkoutAsync(workoutId, exercise, UserId);

            if (!success) { return NotFound(); }
            return CreatedAtAction(nameof(GetExerciseInWorkout), new { workoutId, exerciseId = exercise.Id }, exercise);
        }

        [Authorize]
        [HttpGet("{workoutId}/exercises/{exerciseId}", Name = "GetExerciseInWorkout")]
        public async Task<IActionResult> GetExerciseInWorkout(string workoutId, string exerciseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exercise = await _workoutsService.GetExerciseInWorkout(workoutId, exerciseId, UserId);
            if (exercise == null) return NotFound();
            return Ok(exercise);
        }

        [Authorize]
        [HttpDelete("{workoutId}/exercises/{exerciseId}", Name = "DeleteExerciseFromWorkout")]
        public async Task<IActionResult> DeleteExerciseFromWorkout(string workoutId, string exerciseId)
        {
            // try to get workout
            var success = await _workoutsService.RemoveExerciseFromWorkoutAsync(workoutId, exerciseId, UserId);

            if (!success) { return NotFound(exerciseId); }
            return NoContent();
        }
    }
}
