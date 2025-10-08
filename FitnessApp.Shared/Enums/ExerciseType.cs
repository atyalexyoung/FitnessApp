namespace FitnessApp.Shared.Enums
{
    public static class ExerciseTypes
    {
        public enum ExerciseTypeTag
        {
            Cardio,
            Strength,
            Powerlifting,
            OlympicLifting,
            HIIT,
            Bodyweight,
            Mobility,
            Stretching,
            Balance,
            Core,
            Endurance,
            Speed,
            Agility,
            Rehab
        }

        public static readonly Dictionary<ExerciseTypeTag, string> Descriptions = new()
        {
            { ExerciseTypeTag.Cardio, "Exercises that elevate heart rate to improve cardiovascular endurance and stamina." },
            { ExerciseTypeTag.Strength, "Exercises that build muscle mass and increase overall muscular strength, typically using resistance or weight." },
            { ExerciseTypeTag.Powerlifting, "Heavy compound lifts focused on maximum strength, including squat, bench press, and deadlift." },
            { ExerciseTypeTag.OlympicLifting, "Explosive full-body movements like the clean and jerk or snatch that develop strength, speed, and coordination." },
            { ExerciseTypeTag.HIIT, "High-Intensity Interval Training: alternating short bursts of intense activity with brief recovery periods." },
            { ExerciseTypeTag.Bodyweight, "Exercises using only body weight as resistance, such as push-ups, pull-ups, or squats." },
            { ExerciseTypeTag.Mobility, "Exercises that improve joint movement and active range of motion, aiding performance and injury prevention." },
            { ExerciseTypeTag.Stretching, "Movements designed to increase flexibility and lengthen muscles, often performed statically or dynamically." },
            { ExerciseTypeTag.Balance, "Exercises that improve stability, proprioception, and control over body positioning." },
            { ExerciseTypeTag.Core, "Exercises targeting the muscles of the abdomen, lower back, and pelvis to build core strength and stability." },
            { ExerciseTypeTag.Endurance, "Long-duration activities aimed at increasing muscular or cardiovascular stamina." },
            { ExerciseTypeTag.Speed, "Training focused on improving the ability to move quickly, often involving sprints or rapid movements." },
            { ExerciseTypeTag.Agility, "Exercises that enhance the ability to change direction quickly and efficiently." },
            { ExerciseTypeTag.Rehab, "Controlled movements designed for recovery, injury prevention, or physical therapy." }
        };
    }
}
