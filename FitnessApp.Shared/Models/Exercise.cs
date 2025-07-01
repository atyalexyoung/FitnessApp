using FitnessApp.Shared.Enums;

namespace FitnessApp.Shared.Models
{
    public class Exercise
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<ExerciseType.ExerciseTypeTag> ExerciseTags { get; set; } = [];
        public List<BodyParts.BodyPart> BodyPart { get; set; } = [];
        public List<string> ImageUrls { get; set; } = [];
        public List<string> VideoUrls { get; set; } = [];
    }
}
