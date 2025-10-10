using FitnessApp.Shared.Enums;

namespace FitnessApp.Shared.Models
{
public class ExerciseBodyPart
{
    public string ExerciseId { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
    
    public BodyParts.BodyPart BodyPart { get; set; }
}
}