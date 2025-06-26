using FitnessApp.Interfaces.Repositories;
using FitnessApp.Shared.Enums;
using FitnessApp.Shared.Models;
using System.Linq;

namespace FitnessApp.Repositories.InMemoryRepos
{
    public class InMemoryExercisesRepository : IExercisesRepository
    {
        private readonly List<Exercise> _exercises = SampleData.GetAllExercises().ToList();

        public Task<IEnumerable<Exercise>> GetFilteredAsync(
            List<BodyParts.BodyPartType>? bodyPartTypes = null,
            List<BodyParts.BodyPart>? bodyParts = null,
            List<ExerciseType.ExerciseTypeTag>? exerciseTags = null)
        {
            var filtered = _exercises.AsEnumerable();

            // Expand bodyPartTypes into actual body parts
            if (bodyPartTypes != null && bodyPartTypes.Count > 0)
            {
                var expandedParts = bodyPartTypes
                    .Where(t => BodyParts.Groups.ContainsKey(t))
                    .SelectMany(t => BodyParts.Groups[t])
                    .Distinct()
                    .ToList();

                filtered = filtered.Where(e =>
                    e.BodyPart.Any(bp => expandedParts.Contains(bp)));
            }

            if (bodyParts != null && bodyParts.Count > 0)
            {
                filtered = filtered.Where(e =>
                    e.BodyPart.Any(bp => bodyParts.Contains(bp)));
            }

            if (exerciseTags != null && exerciseTags.Count > 0)
            {
                filtered = filtered.Where(e =>
                    e.ExerciseTags.Any(tag => exerciseTags.Contains(tag)));
            }

            return Task.FromResult(filtered);
        }

        public Task<Exercise?> GetByIdAsync(int id)
        {
            var result = _exercises.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(result);
        }
    }
}
