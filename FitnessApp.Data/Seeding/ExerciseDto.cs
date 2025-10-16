using FitnessApp.Shared.Enums;
using FitnessApp.Shared.Models;

namespace FitnessApp.Data.Seeding
{
    public class ExerciseDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public List<string> VideoUrls { get; set; } = new();
        public List<string> BodyParts { get; set; } = new();
        public List<string> Tags { get; set; } = new();

        public Exercise ToExercise()
    {
        var exercise = new Exercise
        {
            Id = Id,
            Name = Name,
            Description = Description,
            ImageUrls = [.. ImageUrls],
            VideoUrls = [.. VideoUrls],
            ExerciseBodyParts = [.. BodyParts
                .Select(bp => new ExerciseBodyPart
                {
                    ExerciseId = Id,
                    BodyPart = Enum.Parse<BodyParts.BodyPart>(bp, ignoreCase: true)
                })],
            ExerciseTags = [.. Tags
                .Select(t => new ExerciseTag
                {
                    ExerciseId = Id,
                    Tag = Enum.Parse<ExerciseTypes.ExerciseTypeTag>(t, ignoreCase: true)
                })]
        };

        return exercise;
    }
    }
}