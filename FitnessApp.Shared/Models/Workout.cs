namespace FitnessApp.Shared.Models
{
    public class Workout
    {
        public string Id { get; set; } = null!;               // Primary key
        public string Name { get; set; } = "";    // Workout name/title
        public string? Description { get; set; }  // Optional description

        public string UserId { get; set; } = null!;// Foreign key to User who owns it
        public User? User { get; set; }            // Navigation property (optional)

        public List<WorkoutExercise> WorkoutExercises { get; set; } = new();

        // Optional tracking info
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; set; }
    }
}
