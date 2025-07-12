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
        private readonly IExercisesService _exercisesService;

        public ExercisesController(ILogger<ExercisesController> logger, IExercisesService exercisesService)
        {
            _logger = logger;
            _exercisesService = exercisesService;
        }

        [HttpGet("exercises")]
        public async Task<IActionResult> GetExercises(
            [FromQuery] List<string>? bodyPartTypes,
            [FromQuery] List<string>? bodyParts,
            [FromQuery] List<string>? exerciseTags)
        {

            var parsedBodyPartTypes = EnumParser.ParseEnums<BodyParts.BodyPartType>(bodyPartTypes);
            var parsedBodyParts = EnumParser.ParseEnums<BodyParts.BodyPart>(bodyParts);
            var parsedExerciseTags = EnumParser.ParseEnums<ExerciseType.ExerciseTypeTag>(exerciseTags);

            var exercises = await _exercisesService.GetExercisesAsync(
                parsedBodyPartTypes, parsedBodyParts, parsedExerciseTags);

            return Ok(exercises);
        }

        [HttpGet("exercises/{id}")]
        public async Task<IActionResult> GetExerciseById(int id)
        {
            return Ok();
        }
    }
}
