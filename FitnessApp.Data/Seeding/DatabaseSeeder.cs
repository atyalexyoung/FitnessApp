using FitnessApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FitnessApp.Data.Seeding
{
    public static class DatabaseSeeder
    {
        /// <summary>
        /// Seeds or updates exercises ALWAYS (upsert pattern)
        /// Then seeds test data ONLY if no users exist
        /// </summary>
        public static async Task SeedDatabaseAsync(FitnessAppDbContext db)
        {
            // ALWAYS upsert exercises (add new ones, update existing ones)
            await SeedExercisesAsync(db);

            // ONLY seed test data if no users exist (assumes dev/testing)
            if (!await db.Users.AnyAsync())
            {
                await SeedTestDataAsync(db);
            }
        }

        /// <summary>
        /// Upserts all exercises - adds new ones, updates existing ones
        /// Call this standalone if you only want to update exercises without test data
        /// </summary>
        public static async Task SeedExercisesAsync(FitnessAppDbContext db)
        {
            // Load all existing exercises at once (efficient)
            var existingExercises = await db.Exercises
                .Include(e => e.ExerciseBodyParts)
                .Include(e => e.ExerciseTags)
                .ToDictionaryAsync(e => e.Id);

            foreach (var seedExercise in ExerciseSeedData.Exercises)
            {
                if (!existingExercises.TryGetValue(seedExercise.Id, out var existing))
                {
                    // New exercise - add it
                    db.Exercises.Add(seedExercise);
                }
                else
                {
                    // Exercise exists - update it with latest data
                    existing.Name = seedExercise.Name;
                    existing.Description = seedExercise.Description;
                    existing.ImageUrls = seedExercise.ImageUrls;
                    existing.VideoUrls = seedExercise.VideoUrls;

                    // Update body parts
                    existing.ExerciseBodyParts.Clear();
                    foreach (var bp in seedExercise.ExerciseBodyParts)
                    {
                        existing.ExerciseBodyParts.Add(new ExerciseBodyPart
                        {
                            ExerciseId = existing.Id,
                            BodyPart = bp.BodyPart
                        });
                    }

                    // Update tags
                    existing.ExerciseTags.Clear();
                    foreach (var tag in seedExercise.ExerciseTags)
                    {
                        existing.ExerciseTags.Add(new ExerciseTag
                        {
                            ExerciseId = existing.Id,
                            Tag = tag.Tag
                        });
                    }
                }
            }

            await db.SaveChangesAsync();
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
                CreateWorkout(user.Id, "Chest Day", "Focus on pushing movements", allExercises, ["Barbell Bench Press", "Incline Dumbbell Press"]),
                CreateWorkout(user.Id, "Back Day", "Pull and row based", allExercises, ["Pull-Up", "Barbell Row"]),
                CreateWorkout(user.Id, "Shoulder Day", "Overhead and side delts", allExercises, ["Overhead Barbell Press", "Dumbbell Lateral Raise"]),
                CreateWorkout(user.Id, "Leg Day", "Heavy compound leg work", allExercises, ["Barbell Back Squat", "Leg Press"])
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
                        if (ex == null) return null;

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