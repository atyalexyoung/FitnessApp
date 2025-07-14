namespace FitnessApp.Shared.DTOs.Responses
{
    public class WorkoutExerciseResponse
    {
        public string Id { get; set; } = null!;
        public string ExerciseId { get; set; } = null!;
        public string WorkoutId { get; set; } = null!;

        // Include basic exercise info if you want it in the response
        public string ExerciseName { get; set; } = "";
        public string? ExerciseDescription { get; set; }

        public int Order { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public double? Weight { get; set; }
        public TimeSpan? Duration { get; set; }
        public string? Notes { get; set; }
    }
}
