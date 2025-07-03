using FitnessApp.Shared.Enums;
using FitnessApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FitnessApp.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(FitnessAppDbContext db)
        {
            if (await db.Users.AnyAsync())
                return;

            // get hashed password
            // THIS HASHING IS FOR DEVELOPMENT PURPOSES ONLY.
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes("testPassword"));
            var newPassword = Convert.ToBase64String(bytes);

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "demoUser",
                Email = "demo@fitnessapp.com",
                PasswordHash = newPassword,
                CreatedAt = DateTime.UtcNow
            };

            // Create base exercises
            var exercises = new List<Exercise>
            {
                new() { Id = Guid.NewGuid().ToString(), Name = "Bench Press", BodyPart = [BodyParts.BodyPart.Chest] },
                new() { Id = Guid.NewGuid().ToString(), Name = "Incline Dumbbell Press", BodyPart = [BodyParts.BodyPart.Chest] },
                new() { Id = Guid.NewGuid().ToString(), Name = "Pull-Ups", BodyPart = [BodyParts.BodyPart.Lats, BodyParts.BodyPart.Rhomboids] },
                new() { Id = Guid.NewGuid().ToString(), Name = "Barbell Rows", BodyPart = [BodyParts.BodyPart.Lats, BodyParts.BodyPart.Rhomboids] },
                new() { Id = Guid.NewGuid().ToString(), Name = "Overhead Press", BodyPart = [BodyParts.BodyPart.Deltoids] },
                new() { Id = Guid.NewGuid().ToString(), Name = "Lateral Raises", BodyPart = [BodyParts.BodyPart.Deltoids] },
                new() { Id = Guid.NewGuid().ToString(), Name = "Barbell Curl", BodyPart = [BodyParts.BodyPart.Biceps] },
                new() { Id = Guid.NewGuid().ToString(), Name = "Tricep Pushdown", BodyPart = [BodyParts.BodyPart.Triceps] },
                new() { Id = Guid.NewGuid().ToString(), Name = "Squats", BodyPart = [BodyParts.BodyPart.Quads, BodyParts.BodyPart.Glutes, BodyParts.BodyPart.Hamstrings] },
                new() { Id = Guid.NewGuid().ToString(), Name = "Leg Press", BodyPart = [BodyParts.BodyPart.Quads, BodyParts.BodyPart.Glutes, BodyParts.BodyPart.Hamstrings] }
            };

            db.Exercises.AddRange(exercises);

            // Helper to create a workout
            Workout CreateWorkout(string name, string desc, List<string> exerciseNames)
            {
                var workoutId = Guid.NewGuid().ToString();

                var workout = new Workout
                {
                    Id = workoutId,
                    Name = name,
                    Description = desc,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    LastModifiedAt = DateTime.UtcNow,
                    WorkoutExercises = exerciseNames
                        .Select((exName, i) =>
                        {
                            var ex = exercises.First(e => e.Name == exName);
                            return new WorkoutExercise
                            {
                                Id = Guid.NewGuid().ToString(),
                                WorkoutId = workoutId,
                                ExerciseId = ex.Id,
                                Order = i + 1,
                                Sets = 3,
                                Reps = 10,
                                Weight = 50 + i * 5
                            };
                        }).ToList()
                };

                return workout;
            }

            // Add workouts for the user
            var workouts = new List<Workout>
            {
                CreateWorkout("Chest Day", "Focus on pushing movements", ["Bench Press", "Incline Dumbbell Press"]),
                CreateWorkout("Back Day", "Pull and row based", ["Pull-Ups", "Barbell Rows"]),
                CreateWorkout("Shoulder Day", "Overhead and side delts", ["Overhead Press", "Lateral Raises"]),
                CreateWorkout("Arms Day", "Biceps and triceps", ["Barbell Curl", "Tricep Pushdown"]),
                CreateWorkout("Leg Day", "Heavy compound leg work", ["Squats", "Leg Press"])
            };

            user.Workouts.AddRange(workouts);
            db.Users.Add(user);

            await db.SaveChangesAsync();
        }
    }
}
