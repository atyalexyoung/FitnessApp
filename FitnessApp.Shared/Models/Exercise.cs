using FitnessApp.Shared.Enums;

namespace FitnessApp.Shared.Models
{
public class Exercise
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public List<string> VideoUrls { get; set; } = new();
    
    public ICollection<ExerciseBodyPart> ExerciseBodyParts { get; set; } = new List<ExerciseBodyPart>();
    public ICollection<ExerciseTag> ExerciseTags { get; set; } = new List<ExerciseTag>();
}
}
