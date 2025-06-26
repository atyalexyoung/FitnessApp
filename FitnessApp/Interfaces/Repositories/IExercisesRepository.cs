using FitnessApp.Shared.Enums;
using FitnessApp.Shared.Models;

namespace FitnessApp.Interfaces.Repositories
{
    public interface IExercisesRepository
    {
        Task<IEnumerable<Exercise>> GetFilteredAsync(
            List<BodyParts.BodyPartType>? bodyPartTypes = null,
            List<BodyParts.BodyPart>? bodyParts = null,
            List<ExerciseType.ExerciseTypeTag>? exerciseTags = null);

        Task<Exercise?> GetByIdAsync(int id);

    }
}
