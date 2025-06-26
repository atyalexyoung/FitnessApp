using FitnessApp.Shared.DTOs.Responses;
using FitnessApp.Shared.Models;

namespace FitnessApp.Shared.Mappers
{
    public static class WorkoutMapper
    {
        /// <summary>
        /// Converts the <see cref="Workout"/> to a <see cref="WorkoutResponse"/>.
        /// </summary>
        /// <param name="e">The <see cref="Workout"/> to convert.</param>
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
                Order = e.Order
            };
        }
    }
}
