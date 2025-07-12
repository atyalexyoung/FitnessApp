namespace FitnessApp.Shared.DTOs.Requests
{
    /// <summary>
    /// Class for the Request to create a workout that contains only the neccessary information to create the workout.
    /// </summary>
    public class CreateWorkoutRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
