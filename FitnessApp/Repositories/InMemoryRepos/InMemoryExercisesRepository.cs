using FitnessApp.Interfaces.Repositories;
using FitnessApp.Shared.Enums;
using FitnessApp.Shared.Models;
using System.Linq;

namespace FitnessApp.Repositories.InMemoryRepos
{
    public class InMemoryExercisesRepository : IExerciseRepository
    {
        private readonly List<Exercise> _exercises = SampleData.GetAllExercises().ToList();

        public Task<IEnumerable<Exercise>> GetFilteredAsync(
            List<BodyParts.BodyPartType>? bodyPartTypes = null,
            List<BodyParts.BodyPart>? bodyParts = null,
            List<ExerciseTypes.ExerciseTypeTag>? exerciseTags = null)
        {
            var exercises = new List<Exercise>();
            return Task.FromResult<IEnumerable<Exercise>>(exercises);
        }

        public Task<Exercise?> GetByIdAsync(string id)
        {
            Exercise? result = null;
            return Task.FromResult(result);
        }
    }
}
