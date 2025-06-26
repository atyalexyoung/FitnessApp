namespace FitnessApp.Shared.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Best to generate here
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Workout> Workouts { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Role { get; set; } = "User";
    }
}
