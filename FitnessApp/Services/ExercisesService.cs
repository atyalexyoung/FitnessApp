using FitnessApp.Helpers;
using FitnessApp.Interfaces.Repositories;
using FitnessApp.Interfaces.Services;
using FitnessApp.Shared.Enums;
using FitnessApp.Shared.Models;

namespace FitnessApp.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly ILogger<ExerciseService> _logger;
        private readonly IExerciseRepository _exerciseRepo;

        public ExerciseService(ILogger<ExerciseService> logger, IExerciseRepository exercisesRepository)
        {
            _logger = logger;
            _exerciseRepo = exercisesRepository;
        }

        /// <summary>
        /// Will get all the exercises that match based on the filters passed in.
        /// </summary>
        /// <param name="bodyPartTypes">The body part type(s) of the exercises to get.</param>
        /// <param name="bodyParts">The body part(s) of the exercises to get.</param>
        /// <param name="exerciseTags">The types/tags of the exercises to get.</param>
        /// <returns>A task of enumerable <see cref="Exercise"/> objects.</returns>
        public async Task<Result<IEnumerable<Exercise>>> GetExercisesAsync(
            List<BodyParts.BodyPartType>? bodyPartTypes = null,
            List<BodyParts.BodyPart>? bodyParts = null,
            List<ExerciseTypes.ExerciseTypeTag>? exerciseTags = null)
        {
            try
            {
                _logger.LogDebug("Getting all exercises.");
                var exercises = await _exerciseRepo.GetFilteredAsync(bodyPartTypes, bodyParts, exerciseTags);

                if (exercises == null)
                {
                    _logger.LogError("Recieved null exercises. Returning Result.Fail with error not found.");
                    return Result.Fail<IEnumerable<Exercise>>("Exercises returned as null", ErrorType.NotFound);
                }

                return Result.Ok(exercises);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when getting filtered exercises with exception: {ex}", ex);
                return Result.Fail<IEnumerable<Exercise>>("Unexpected error when getting exercises", ErrorType.Internal);
            }
        }

        /// <summary>
        /// Will get a specific exercise based on it's id
        /// </summary>
        /// <param name="id">The id of the exercise to get.</param>
        /// <returns>A task of a <see cref="Exercise"/> object.</returns>
        public async Task<Result<Exercise>> GetExerciseByIdAsync(string id)
        {
            try
            {
                _logger.LogDebug("Getting all exercises.");
                var exercise = await _exerciseRepo.GetByIdAsync(id);

                if (exercise == null)
                {
                    _logger.LogError("Recieved null exercise for exercise with id: {}. Returning Result.Fail with error not found.", id);
                    return Result.Fail<Exercise>("Exercises returned as null", ErrorType.NotFound);
                }

                return Result.Ok(exercise);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when getting exercise by id of: {id} with exception: {ex}", id, ex);
                return Result.Fail<Exercise>("Unexpected error when getting exercises", ErrorType.Internal);
            }
        }
    }
}
