using FitnessApp.Helpers;
using FitnessApp.Shared.DTOs.Requests;
using FitnessApp.Shared.DTOs.Responses;
using FitnessApp.Shared.Models;

namespace FitnessApp.Interfaces.Services
{
    public interface IWorkoutsService
    {
        /// <summary>
        /// Gets all workouts for a user.
        /// </summary>
        /// <param name="userId">The id of the user to get the workout for.</param>
        /// <returns>Task of enumerable of <see cref="Workout"/> objects.</returns>
        Task<Result<IEnumerable<WorkoutResponse>>> GetAllWorkoutsAsync(string userId);

        /// <summary>
        /// Will get a workout by id.
        /// </summary>
        /// <param name="workoutId">The id of the workout to get.</param>
        /// <param name="userId">The id of the user.</param>
        /// <returns>Task of nullable <see cref="Workout"/> object.</returns>
        Task<Result<WorkoutResponse?>> GetWorkoutByIdAsync(string workoutId, string userId);


        /// <summary>
        /// Will create a new workout for a user.
        /// </summary>
        /// <param name="workout">The workout to create.</param>
        /// <param name="userId">The id of the user.</param>
        /// <returns>Task of workout object created.</returns>
        Task<Result<WorkoutResponse>> CreateWorkoutAsync(CreateWorkoutRequest workout, string userId);

        /// <summary>
        /// Updates a current workout with a new workout.
        /// </summary>
        /// <param name="workoutId">The id of the workout to update.</param>
        /// <param name="workout">The new workout to update the previous workout to.</param>
        /// <param name="userId">The id of the user.</param>
        /// <returns>Task of bool if the update was successful or not.</returns>
        Task<bool> UpdateWorkoutAsync(string workoutId, Workout workout, string userId);

        /// <summary>
        /// Deletes a workout from a user by id.
        /// </summary>
        /// <param name="workoutId">The id of the workout to delete.</param>
        /// <param name="userId">The id of the user.</param>
        /// <returns>Task of bool if the deletion was successful or not.</returns>
        Task<Result<bool>> DeleteWorkoutAsync(string workoutId, string userId);

        /// <summary>
        /// Gets all the workout exercises associated with a particular workout.
        /// </summary>
        /// <param name="workoutId">The id of the workout to get the exercises from.</param>
        /// <param name="userId">The id of the user to get the workout from.</param>
        /// <returns>Task of an <see cref="IEnumerable"/> of type <see cref="WorkoutExercise"/></returns>
        Task<Result<IEnumerable<WorkoutExercise>>> GetExercisesInWorkoutAsync(string workoutId, string userId);

        /// <summary>
        /// Gets a specific exercise in a specific workout.
        /// </summary>
        /// <param name="workoutId">The id of the workout to get the exercise from.</param>
        /// <param name="exerciseId">The id of the exercise to get.</param>
        /// <param name="userId">The id of the user to get the workout from</param>
        /// <returns>Task of nullable <see cref="WorkoutExercise"/></returns>
        Task<WorkoutExercise?> GetWorkoutExerciseAsync(string workoutId, string exerciseId, string userId);

        /// <summary>
        /// Will add a new exercise to a workout.
        /// </summary>
        /// <param name="workoutId">The id of the workout to add the exercise to.</param>
        /// <param name="workoutExercise">The exercise to add to the workout.</param>
        /// <param name="userId">The user to add the exercise to their workout.</param>
        /// <returns>Task of bool if the addition of exercise was successful or not.</returns>
        Task<bool> AddExerciseToWorkoutAsync(string workoutId, WorkoutExercise workoutExercise, string userId);

        /// <summary>
        /// Removes an exercise by id from a user's workout
        /// </summary>
        /// <param name="workoutId">The id of the workout to remove the exercise from.</param>
        /// <param name="exerciseId">The id of the exercise to remove.</param>
        /// <param name="userId">The id of the user.</param>
        /// <returns>Task of bool if the removal of exercise from workout was sucessful or not.</returns>
        Task<bool> RemoveExerciseFromWorkoutAsync(string workoutId, string exerciseId, string userId);

        // Optional: other workout-related business logic
        // Task<IEnumerable<WorkoutStats>> GetWorkoutStatsAsync(string userId);
    }
}
