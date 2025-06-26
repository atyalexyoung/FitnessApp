using FitnessApp.Shared.Enums;
using FitnessApp.Shared.Models;

namespace FitnessApp.Interfaces.Services
{
    public interface IExercisesService
    {
        /// <summary>
        /// Will get all the exercises that match based on the filters passed in.
        /// </summary>
        /// <param name="bodyPartTypes">The body part type(s) of the exercises to get.</param>
        /// <param name="bodyParts">The body part(s) of the exercises to get.</param>
        /// <param name="ExerciseTags">The types/tags of the exercises to get.</param>
        /// <returns>A task of enumerable <see cref="Exercise"/> objects.</returns>
        public Task<IEnumerable<Exercise>> GetExercisesAsync(
            List<BodyParts.BodyPartType>? bodyPartTypes = null,
            List<BodyParts.BodyPart>? bodyParts = null,
            List<ExerciseType.ExerciseTypeTag>? ExerciseTags = null);

        /// <summary>
        /// Will get a specific exercise based on it's id
        /// </summary>
        /// <param name="id">The id of the exercise to get.</param>
        /// <returns>A task of a <see cref="Exercise"/> object.</returns>
        public Task<Exercise?> GetExerciseByIdAsync(int id);
    }
}
