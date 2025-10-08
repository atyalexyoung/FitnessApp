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
            // ALWAYS add exercises if they don't exist (dev and prod)
            if (!await db.Exercises.AnyAsync())
            {
                db.Exercises.AddRange(ExerciseSeedData.Exercises);
                await db.SaveChangesAsync();
            }

            // ONLY seed test data if no users exist (dev/testing only)
            if (await db.Users.AnyAsync())
                return;

            await SeedTestDataAsync(db);
        }

        private static async Task SeedTestDataAsync(FitnessAppDbContext db)
        {
            // Get hashed password
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

            // Get the already-seeded exercises from the database
            var allExercises = await db.Exercises
                .Include(e => e.ExerciseBodyParts)
                .Include(e => e.ExerciseTags)
                .ToListAsync();

            // Add workouts for the demo user
            var workouts = new List<Workout>
            {
                CreateWorkout(user.Id, "Chest Day", "Focus on pushing movements", allExercises, ["Bench Press", "Incline Dumbbell Press"]),
                CreateWorkout(user.Id, "Back Day", "Pull and row based", allExercises, ["Pull-Ups", "Barbell Rows"]),
                CreateWorkout(user.Id, "Shoulder Day", "Overhead and side delts", allExercises, ["Overhead Press", "Lateral Raises"]),
                CreateWorkout(user.Id, "Arms Day", "Biceps and triceps", allExercises, ["Barbell Curl", "Tricep Pushdown"]),
                CreateWorkout(user.Id, "Leg Day", "Heavy compound leg work", allExercises, ["Squats", "Leg Press"])
            };

            user.Workouts.AddRange(workouts);
            db.Users.Add(user);

            await db.SaveChangesAsync();
        }

        private static Workout CreateWorkout(
            string userId,
            string name, 
            string desc, 
            List<Exercise> exercises,
            List<string> exerciseNames)
        {
            var workoutId = Guid.NewGuid().ToString();

            return new Workout
            {
                Id = workoutId,
                Name = name,
                Description = desc,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow,
                WorkoutExercises = exerciseNames
                    .Select((exName, i) =>
                    {
                        var ex = exercises.FirstOrDefault(e => e.Name == exName);
                        if (ex == null)
                        {
                            // Skip if exercise not found in database
                            return null;
                        }
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
                    })
                    .Where(we => we != null)
                    .ToList()!
            };
        }
    }
}