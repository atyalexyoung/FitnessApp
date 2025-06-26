using FitnessApp.Shared.Models;

public static class SampleData
{
    private static List<Workout> _workouts = CreateInitialWorkouts();

    public static List<Workout> GetAllWorkouts() => _workouts;

    public static Workout? GetWorkout(string id) =>
        _workouts.FirstOrDefault(w => w.Id == id);

    public static Workout AddWorkout(Workout workout)
    {
        workout.Id = Guid.NewGuid().ToString("N");
        _workouts.Add(workout);
        return workout;
    }

    public static bool UpdateWorkout(string id, Workout updatedWorkout)
    {
        var index = _workouts.FindIndex(w => w.Id == id);
        if (index == -1) return false;

        updatedWorkout.Id = id;
        _workouts[index] = updatedWorkout;
        return true;
    }

    public static bool DeleteWorkout(string id)
    {
        var workout = GetWorkout(id);
        return workout != null && _workouts.Remove(workout);
    }

    private static List<Workout> CreateInitialWorkouts()
    {
        var pushup = new Exercise { Id = 1, Name = "Push-Up" };
        var squat = new Exercise { Id = 2, Name = "Squat" };

        return new List<Workout>
        {
            new Workout
            {
                Id = "testWorkout",
                Name = "Morning Routine",
                UserId = "1",
                WorkoutExercises = new List<WorkoutExercise>
                {
                    new WorkoutExercise
                    {
                        Id = "testExercise1",
                        ExerciseId = pushup.Id,
                        Exercise = pushup,
                        Sets = 3,
                        Reps = 15,
                        Order = 1
                    },
                    new WorkoutExercise
                    {
                        Id = "testExercise2",
                        ExerciseId = squat.Id,
                        Exercise = squat,
                        Sets = 4,
                        Reps = 10,
                        Order = 2
                    }
                }
            }
        };
    }
    public static IEnumerable<Exercise> GetAllExercises()
    {
        return new List<Exercise>
        {
            new() { Id = 1, Name = "Push-Up", Description = "A bodyweight exercise" },
            new() { Id = 2, Name = "Squat", Description = "A leg strength exercise" },
            // Add more sample exercises...
        };
    }
}