namespace FitnessApp.Shared.Models
{
    public class WorkoutExercise
    {
        public string Id { get; set; } = null!;

        public int WorkoutId { get; set; }
        public Workout? Workout { get; set; }

        public int ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }

        public int Order { get; set; }         // Position of this exercise in the workout

        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public double? Weight { get; set; }    // Use nullable if weight isn’t always relevant

        public string? Notes { get; set; }     // Optional user notes per exercise in workout
    }
}
