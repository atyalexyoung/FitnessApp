using FitnessApp.Shared.DTOs.Requests;
using FitnessApp.Shared.DTOs.Responses;
using FitnessApp.Shared.Models;

namespace FitnessApp.Shared.Mappers
{
    public static class WorkoutMapper
    {
        /// <summary>
        /// Converts the <see cref="Workout"/> to a <see cref="WorkoutResponse"/>.
        /// </summary>
        /// <returns>The <see cref="WorkoutResponse"/> correlating with the <see cref="Workout"/></returns>
        public static WorkoutResponse ToResponse(this Workout workout)
        {
            return new WorkoutResponse
            {
                Id = workout.Id,
                Name = workout.Name,
                Description = workout.Description,
                CreatedAt = workout.CreatedAt,
                LastModifiedAt = workout.LastModifiedAt,
                WorkoutExercises = workout.WorkoutExercises
                    .OrderBy(e => e.Order)
                    .Select(e => e.ToResponse())
                    .ToList()
            };
        }

        /// <summary>
        /// Converts the <see cref="WorkoutExercise"/> to a <see cref="WorkoutExerciseResponse"/>.
        /// </summary>
        /// <param name="e">The <see cref="WorkoutExercise"/> to convert.</param>
        /// <returns>The <see cref="WorkoutExerciseResponse"/> correlating with the <see cref="WorkoutExercise"/></returns>
        public static WorkoutExerciseResponse ToResponse(this WorkoutExercise e)
        {
            return new WorkoutExerciseResponse
            {
                Id = e.Id,
                ExerciseId = e.ExerciseId,
                ExerciseName = e.Exercise?.Name ?? "",
                Notes = e.Notes,
                Sets = e.Sets,
                Reps = e.Reps,
                Weight = e.Weight,
                Duration = e.Duration,
                Order = e.Order
            };
        }

        public static WorkoutExercise ToEntity(this CreateWorkoutExerciseRequest req)
        {
            return new WorkoutExercise
            {
                Id = Guid.NewGuid().ToString(),
                WorkoutId = req.WorkoutId,
                ExerciseId = req.ExerciseId,
                Order = req.Order,
                Sets = req.Sets,
                Reps = req.Reps,
                Weight = req.Weight,
                Duration = req.Duration,
                Notes = req.Notes
            };
        }

        public static void UpdateFrom(this Workout workout, Workout updated)
        {
            workout.Name = updated.Name;
            workout.WorkoutExercises = updated.WorkoutExercises;
            workout.Description = updated.Description;
        }
    }
}
