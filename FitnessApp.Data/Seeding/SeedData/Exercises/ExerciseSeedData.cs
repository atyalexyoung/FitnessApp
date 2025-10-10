using FitnessApp.Shared.Enums;
using FitnessApp.Shared.Models;

namespace FitnessApp.Data
{
    internal static class ExerciseSeedData
    {
        public static IReadOnlyList<Exercise> Exercises
        {
            get
            {
                var exercises = new List<Exercise>
                {
                    CreateExercise(
                        "bench-press",
                        "Barbell Bench Press",
                        "A compound pushing exercise targeting the chest, triceps, and shoulders.",
                        [BodyParts.BodyPart.Chest, BodyParts.BodyPart.Triceps, BodyParts.BodyPart.Deltoids],
                        [ExerciseTypes.ExerciseTypeTag.Strength, ExerciseTypes.ExerciseTypeTag.Powerlifting]
                    ),
                    CreateExercise(
                        "incline-dumbbell-press",
                        "Incline Dumbbell Press",
                        "Targets the upper chest and anterior deltoids with a greater range of motion than the barbell press.",
                        [BodyParts.BodyPart.Chest, BodyParts.BodyPart.Deltoids, BodyParts.BodyPart.Triceps],
                        [ExerciseTypes.ExerciseTypeTag.Strength]
                    ),
                    CreateExercise(
                        "push-up",
                        "Push-Up",
                        "A bodyweight pressing movement for chest, triceps, and core stability.",
                        [BodyParts.BodyPart.Chest, BodyParts.BodyPart.Triceps, BodyParts.BodyPart.Deltoids, BodyParts.BodyPart.Abdominals],
                        [ExerciseTypes.ExerciseTypeTag.Strength, ExerciseTypes.ExerciseTypeTag.Bodyweight]
                    ),
                    CreateExercise(
                        "overhead-press",
                        "Overhead Barbell Press",
                        "A vertical pressing exercise for shoulder strength and stability.",
                        [BodyParts.BodyPart.Deltoids, BodyParts.BodyPart.Triceps],
                        [ExerciseTypes.ExerciseTypeTag.Strength]
                    ),
                    CreateExercise(
                        "arnold-press",
                        "Arnold Press",
                        "A shoulder press variation that targets all heads of the deltoid through a rotational movement.",
                        [BodyParts.BodyPart.Deltoids, BodyParts.BodyPart.Triceps],
                        [ExerciseTypes.ExerciseTypeTag.Strength]
                    ),
                    CreateExercise(
                        "lateral-raise",
                        "Dumbbell Lateral Raise",
                        "An isolation exercise that develops the lateral deltoid for shoulder width.",
                        [BodyParts.BodyPart.Deltoids],
                        [ExerciseTypes.ExerciseTypeTag.Strength]
                    ),
                    CreateExercise(
                        "barbell-row",
                        "Barbell Row",
                        "A compound pull that develops the lats, rhomboids, and traps for back thickness.",
                        [BodyParts.BodyPart.Lats, BodyParts.BodyPart.Rhomboids, BodyParts.BodyPart.Trapezius, BodyParts.BodyPart.Biceps],
                        [ExerciseTypes.ExerciseTypeTag.Strength]
                    ),
                    CreateExercise(
                        "pull-up",
                        "Pull-Up",
                        "A bodyweight pull-up for lat width and upper body strength.",
                        [BodyParts.BodyPart.Lats, BodyParts.BodyPart.Biceps, BodyParts.BodyPart.Rhomboids],
                        [ExerciseTypes.ExerciseTypeTag.Strength, ExerciseTypes.ExerciseTypeTag.Bodyweight]
                    ),
                    CreateExercise(
                        "chin-up",
                        "Chin-Up",
                        "A supinated pull-up emphasizing the biceps and lats.",
                        [BodyParts.BodyPart.Biceps, BodyParts.BodyPart.Lats, BodyParts.BodyPart.Rhomboids],
                        [ExerciseTypes.ExerciseTypeTag.Strength, ExerciseTypes.ExerciseTypeTag.Bodyweight]
                    ),
                    CreateExercise(
                        "deadlift",
                        "Barbell Deadlift",
                        "A heavy compound lift engaging the posterior chain for total body strength.",
                        [BodyParts.BodyPart.Hamstrings, BodyParts.BodyPart.Glutes, BodyParts.BodyPart.LowerBack, BodyParts.BodyPart.Trapezius],
                        [ExerciseTypes.ExerciseTypeTag.Strength, ExerciseTypes.ExerciseTypeTag.Powerlifting]
                    ),
                    CreateExercise(
                        "romanian-deadlift",
                        "Romanian Deadlift",
                        "A deadlift variation that focuses on hamstring and glute development with minimal knee bend.",
                        [BodyParts.BodyPart.Hamstrings, BodyParts.BodyPart.Glutes, BodyParts.BodyPart.LowerBack],
                        [ExerciseTypes.ExerciseTypeTag.Strength]
                    ),
                    CreateExercise(
                        "squat",
                        "Barbell Back Squat",
                        "A compound leg movement that builds overall lower body strength and stability.",
                        [BodyParts.BodyPart.Quads, BodyParts.BodyPart.Glutes, BodyParts.BodyPart.Hamstrings],
                        [ExerciseTypes.ExerciseTypeTag.Strength, ExerciseTypes.ExerciseTypeTag.Powerlifting]
                    ),
                    CreateExercise(
                        "front-squat",
                        "Front Squat",
                        "A squat variation emphasizing the quads and core stability.",
                        [BodyParts.BodyPart.Quads, BodyParts.BodyPart.Abdominals, BodyParts.BodyPart.Glutes],
                        [ExerciseTypes.ExerciseTypeTag.Strength, ExerciseTypes.ExerciseTypeTag.OlympicLifting]
                    ),
                    CreateExercise(
                        "leg-press",
                        "Leg Press",
                        "A machine-based leg exercise for hypertrophy and lower body strength.",
                        [BodyParts.BodyPart.Quads, BodyParts.BodyPart.Glutes, BodyParts.BodyPart.Hamstrings],
                        [ExerciseTypes.ExerciseTypeTag.Strength]
                    ),
                    CreateExercise(
                        "lunges",
                        "Walking Lunges",
                        "A unilateral leg exercise that builds balance, coordination, and lower body strength.",
                        [BodyParts.BodyPart.Quads, BodyParts.BodyPart.Glutes, BodyParts.BodyPart.Hamstrings],
                        [ExerciseTypes.ExerciseTypeTag.Strength, ExerciseTypes.ExerciseTypeTag.Balance]
                    ),
                    CreateExercise(
                        "calf-raise",
                        "Standing Calf Raise",
                        "An isolation movement for developing the calves and ankle strength.",
                        [BodyParts.BodyPart.Calves],
                        [ExerciseTypes.ExerciseTypeTag.Strength]
                    ),
                    CreateExercise(
                        "plank",
                        "Plank",
                        "A static core exercise that strengthens the abdominals, obliques, and lower back.",
                        [BodyParts.BodyPart.Abdominals, BodyParts.BodyPart.Obliques],
                        [ExerciseTypes.ExerciseTypeTag.Core, ExerciseTypes.ExerciseTypeTag.Bodyweight]
                    ),
                    CreateExercise(
                        "crunch",
                        "Abdominal Crunch",
                        "An isolation exercise for the rectus abdominis.",
                        [BodyParts.BodyPart.Abdominals],
                        [ExerciseTypes.ExerciseTypeTag.Core]
                    ),
                    CreateExercise(
                        "hanging-leg-raise",
                        "Hanging Leg Raise",
                        "An advanced bodyweight exercise for lower abs and hip flexors.",
                        [BodyParts.BodyPart.Abdominals, BodyParts.BodyPart.Obliques],
                        [ExerciseTypes.ExerciseTypeTag.Core, ExerciseTypes.ExerciseTypeTag.Bodyweight]
                    ),
                    CreateExercise(
                        "russian-twist",
                        "Russian Twist",
                        "A rotational movement that strengthens the obliques and improves core control.",
                        [BodyParts.BodyPart.Obliques, BodyParts.BodyPart.Abdominals],
                        [ExerciseTypes.ExerciseTypeTag.Core, ExerciseTypes.ExerciseTypeTag.Balance]
                    ),
                    CreateExercise(
                        "burpees",
                        "Burpees",
                        "A high-intensity full-body conditioning exercise used for fat loss and endurance.",
                        [BodyParts.BodyPart.Chest, BodyParts.BodyPart.Quads, BodyParts.BodyPart.Glutes, BodyParts.BodyPart.Abdominals],
                        [ExerciseTypes.ExerciseTypeTag.Cardio, ExerciseTypes.ExerciseTypeTag.HIIT, ExerciseTypes.ExerciseTypeTag.Bodyweight]
                    ),
                    CreateExercise(
                        "jump-rope",
                        "Jump Rope",
                        "A coordination and cardio exercise improving agility, endurance, and timing.",
                        [BodyParts.BodyPart.Calves, BodyParts.BodyPart.Forearms],
                        [ExerciseTypes.ExerciseTypeTag.Cardio, ExerciseTypes.ExerciseTypeTag.Agility, ExerciseTypes.ExerciseTypeTag.Speed]
                    ),
                    CreateExercise(
                        "running",
                        "Running",
                        "A cardiovascular exercise improving endurance and lower body strength.",
                        [BodyParts.BodyPart.Quads, BodyParts.BodyPart.Hamstrings, BodyParts.BodyPart.Calves, BodyParts.BodyPart.Glutes],
                        [ExerciseTypes.ExerciseTypeTag.Cardio, ExerciseTypes.ExerciseTypeTag.Endurance]
                    ),
                    CreateExercise(
                        "rowing",
                        "Rowing (Machine)",
                        "A low-impact, full-body cardio workout targeting legs, core, and back.",
                        [BodyParts.BodyPart.Lats, BodyParts.BodyPart.Quads, BodyParts.BodyPart.Abdominals, BodyParts.BodyPart.Deltoids],
                        [ExerciseTypes.ExerciseTypeTag.Cardio, ExerciseTypes.ExerciseTypeTag.Endurance]
                    ),
                    CreateExercise(
                        "jump-squat",
                        "Jump Squat",
                        "A plyometric movement improving lower body power and explosiveness.",
                        [BodyParts.BodyPart.Quads, BodyParts.BodyPart.Glutes, BodyParts.BodyPart.Calves],
                        [ExerciseTypes.ExerciseTypeTag.Strength, ExerciseTypes.ExerciseTypeTag.HIIT, ExerciseTypes.ExerciseTypeTag.Speed]
                    ),
                    CreateExercise(
                        "mountain-climbers",
                        "Mountain Climbers",
                        "A dynamic core and cardio movement improving endurance and coordination.",
                        [BodyParts.BodyPart.Abdominals, BodyParts.BodyPart.Obliques, BodyParts.BodyPart.Deltoids],
                        [ExerciseTypes.ExerciseTypeTag.Cardio, ExerciseTypes.ExerciseTypeTag.HIIT, ExerciseTypes.ExerciseTypeTag.Bodyweight]
                    ),
                    CreateExercise(
                        "tricep-dip",
                        "Tricep Dips",
                        "A compound bodyweight exercise focusing on triceps, chest, and shoulders.",
                        [BodyParts.BodyPart.Triceps, BodyParts.BodyPart.Chest, BodyParts.BodyPart.Deltoids],
                        [ExerciseTypes.ExerciseTypeTag.Strength, ExerciseTypes.ExerciseTypeTag.Bodyweight]
                    ),
                    CreateExercise(
                        "hammer-curl",
                        "Dumbbell Hammer Curl",
                        "An arm exercise emphasizing the brachialis and forearms.",
                        [BodyParts.BodyPart.Biceps, BodyParts.BodyPart.Forearms],
                        [ExerciseTypes.ExerciseTypeTag.Strength]
                    ),
                    CreateExercise(
                        "yoga-flow",
                        "Yoga Flow",
                        "A mobility and flexibility routine combining balance, stretching, and breath control.",
                        [BodyParts.BodyPart.Hamstrings, BodyParts.BodyPart.Glutes, BodyParts.BodyPart.LowerBack, BodyParts.BodyPart.Deltoids],
                        [ExerciseTypes.ExerciseTypeTag.Mobility, ExerciseTypes.ExerciseTypeTag.Stretching, ExerciseTypes.ExerciseTypeTag.Balance]
                    ),
                    CreateExercise(
                        "face-pull",
                        "Face Pull",
                        "A cable movement targeting the rear delts and upper traps for shoulder health.",
                        [BodyParts.BodyPart.Deltoids, BodyParts.BodyPart.Trapezius, BodyParts.BodyPart.Rhomboids],
                        [ExerciseTypes.ExerciseTypeTag.Strength, ExerciseTypes.ExerciseTypeTag.Rehab]
                    )
                };

                return exercises;
            }
        }

        private static Exercise CreateExercise(
            string id,
            string name,
            string description,
            List<BodyParts.BodyPart> bodyParts,
            List<ExerciseTypes.ExerciseTypeTag> tags)
        {
            return new Exercise
            {
                Id = id,
                Name = name,
                Description = description,
                ImageUrls = [],
                VideoUrls = [],
                ExerciseBodyParts = bodyParts.Select(bp => new ExerciseBodyPart
                {
                    ExerciseId = id,
                    BodyPart = bp
                }).ToList(),
                ExerciseTags = tags.Select(tag => new ExerciseTag
                {
                    ExerciseId = id,
                    Tag = tag
                }).ToList()
            };
        }
    }
}