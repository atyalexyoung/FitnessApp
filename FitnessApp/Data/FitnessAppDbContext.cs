using FitnessApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data
{
    public class FitnessAppDbContext : DbContext
    {
        public FitnessAppDbContext(DbContextOptions<FitnessAppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Workout: 1->many
            modelBuilder.Entity<User>()
                .HasMany(u => u.Workouts)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Workout - WorkoutExercise: 1-many
            modelBuilder.Entity<Workout>()
                .HasMany(w => w.WorkoutExercises)
                .WithOne(we => we.Workout)
                .HasForeignKey(we => we.WorkoutId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // WorkoutExercise - Exercise: many-to-1
            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Exercise)
                .WithMany() // No navigation on Exercise needed unless you want it
                .HasForeignKey(we => we.ExerciseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // Exercise is static, so don't cascade delete
        }
    }
}
