using FitnessApp.Shared.Enums;

namespace FitnessApp.Shared.DTOs
{
    public class ExerciseResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<ExerciseTypes.ExerciseTypeTag> ExerciseTags { get; set; } = [];
        public List<BodyParts.BodyPart> BodyParts { get; set; } = [];
        public List<string> ImageUrls { get; set; } = [];
        public List<string> VideoUrls { get; set; } = [];
    }
}