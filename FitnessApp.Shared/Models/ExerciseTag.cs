using FitnessApp.Shared.Enums;

namespace FitnessApp.Shared.Models
{
public class ExerciseTag
{
    public string ExerciseId { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
    
    public ExerciseTypes.ExerciseTypeTag Tag { get; set; }
}
}