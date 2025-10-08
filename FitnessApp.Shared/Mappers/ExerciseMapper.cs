using FitnessApp.Shared.DTOs;
using FitnessApp.Shared.Enums;
using FitnessApp.Shared.Models;

namespace FitnessApp.Shared.Mappers
{
    public static class ExerciseMapper
    {
        public static ExerciseResponse ToResponse(this Exercise exercise)
        {
            return new ExerciseResponse
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Description = exercise.Description,
                BodyParts = exercise.ExerciseBodyParts.Select(eb => eb.BodyPart).ToList(),
                ExerciseTags = exercise.ExerciseTags.Select(et => et.Tag).ToList(),
                ImageUrls = exercise.ImageUrls,
                VideoUrls = exercise.VideoUrls
            };
        }
    }

}