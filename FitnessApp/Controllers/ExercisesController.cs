using FitnessApp.Extensions;
using FitnessApp.Helpers;
using FitnessApp.Interfaces.Services;
using FitnessApp.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly ILogger<ExercisesController> _logger;
        private readonly IExerciseService _exercisesService;

        public ExercisesController(ILogger<ExercisesController> logger, IExerciseService exercisesService)
        {
            _logger = logger;
            _exercisesService = exercisesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExercises(
            [FromQuery] List<string>? bodyPartTypes,
            [FromQuery] List<string>? bodyParts,
            [FromQuery] List<string>? exerciseTags)
        {

            var parsedBodyPartTypes = EnumParser.ParseEnums<BodyParts.BodyPartType>(bodyPartTypes);
            var parsedBodyParts = EnumParser.ParseEnums<BodyParts.BodyPart>(bodyParts);
            var parsedExerciseTags = EnumParser.ParseEnums<ExerciseTypes.ExerciseTypeTag>(exerciseTags);

            var result = await _exercisesService.GetExercisesAsync(
                parsedBodyPartTypes, parsedBodyParts, parsedExerciseTags);

            if (result == null)
            {
                _logger.LogWarning("Result from service was null in endpoint handler.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred.");
            }

            if (result.IsFailure)
            {
                _logger.LogWarning("Could not get the exercises with filters: with filters of:" +
                    "{ bodyPartTypes}, {bodyParts}, {exerciseTags}, \n with error: {error}", bodyPartTypes, bodyParts, exerciseTags, result.Error);
                return result.ToActionResult();
            }

            var exercises = result.Value;
            if (exercises == null) // shouldn't be since service checks, but doesn't hurt
            {
                _logger.LogWarning("No exercises found with filters of: {bodyPartTypes}, {bodyParts}, {exerciseTags}", bodyPartTypes, bodyParts, exerciseTags);
                return NotFound("No exercises found.");
            }

            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExerciseById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "provided invalid exercise ID");
            }

            var result = await _exercisesService.GetExerciseByIdAsync(id);

            if (result == null)
            {
                _logger.LogWarning("Result from service was null in endpoint handler.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred.");
            }

            if (result.IsFailure)
            {
                _logger.LogWarning("Could not get the exercises with id: {exerciseId} with error: {error}", id, result.Error);
                return result.ToActionResult();
            }

            var exercise = result.Value;
            if (exercise == null) // shouldn't be since service checks, but doesn't hurt
            {
                _logger.LogWarning("No exercises found with id: {id}", id);
                return NotFound("No exercise found with provided ID.");
            }

            return Ok(exercise);
        }
    }
}
