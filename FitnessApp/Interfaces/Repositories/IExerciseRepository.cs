using FitnessApp.Shared.Enums;
using FitnessApp.Shared.Models;

namespace FitnessApp.Interfaces.Repositories
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetFilteredAsync(
            CancellationToken cancellationToken,
            List<BodyParts.BodyPartType>? bodyPartTypes = null,
            List<BodyParts.BodyPart>? bodyParts = null,
            List<ExerciseTypes.ExerciseTypeTag>? exerciseTags = null);

        Task<Exercise?> GetByIdAsync(string id, CancellationToken cancellationToken);
    }
}
