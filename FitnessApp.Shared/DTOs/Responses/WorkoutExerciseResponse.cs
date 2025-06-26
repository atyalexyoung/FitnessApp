namespace FitnessApp.Shared.DTOs.Responses
{
    public class WorkoutExerciseResponse
    {
        public string Id { get; set; } = null!;
        public int ExerciseId { get; set; }

        // Include basic exercise info if you want it in the response
        public string ExerciseName { get; set; } = "";
        public string? ExerciseDescription { get; set; }

        public int Order { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public double? Weight { get; set; }
        public string? Notes { get; set; }
    }
}
