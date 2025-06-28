namespace FitnessApp.Shared.DTOs.Responses
{
    public class WorkoutResponse
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public List<WorkoutExerciseResponse> WorkoutExercises { get; set; } = [];
    }
}
