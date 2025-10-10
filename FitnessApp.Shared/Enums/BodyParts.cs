namespace FitnessApp.Shared.Enums
{
    public static class BodyParts
    {
        /// <summary>
        /// The body parts that can be worked by exercises.
        /// </summary>
        public enum BodyPart
        {
            // Arms
            Biceps = 1,
            Triceps = 2,
            Forearms = 3 ,

            // Shoulders
            Deltoids = 4,

            // Chest
            Chest = 5,

            // Back
            Lats = 6,
            Rhomboids = 7,
            Trapezius = 8,
            LowerBack = 9,

            // Core
            Abdominals = 10,
            Obliques = 11,

            // Legs
            Glutes = 12,
            Quads = 13,
            Hamstrings = 14,
            Calves = 15
        }

        /// <summary>
        /// Body part groupings
        /// </summary>
        public enum BodyPartType
        {
            Arms = 1,
            Shoulders = 2,
            Chest = 3,
            Back = 4,
            Core = 5,
            Legs = 6
        }

        /// <summary>
        /// Dictionary that maps the body part types to a list of the body parts that
        /// are of that type (in that grouping.)
        /// </summary>
        public static readonly Dictionary<BodyPartType, BodyPart[]> Groups = new()
        {
            { BodyPartType.Arms, new[] { BodyPart.Biceps, BodyPart.Triceps, BodyPart.Forearms } },
            { BodyPartType.Shoulders, new[] { BodyPart.Deltoids } },
            { BodyPartType.Chest, new[] { BodyPart.Chest } },
            { BodyPartType.Back, new[] { BodyPart.Lats, BodyPart.Rhomboids, BodyPart.Trapezius, BodyPart.LowerBack } },
            { BodyPartType.Core, new[] { BodyPart.Abdominals, BodyPart.Obliques } },
            { BodyPartType.Legs, new[] { BodyPart.Glutes, BodyPart.Quads, BodyPart.Hamstrings, BodyPart.Calves } }
        };

        /// <summary>
        /// Will get the body part type/group for a specific body type
        /// </summary>
        ///
        /// <param name="part">The body part to find grouping for.</param>
        ///
        /// <returns>Nullable type/group for the body part.</returns>
        public static BodyPartType? GetGroupFor(BodyPart part)
        {
            foreach (var (group, parts) in Groups)
            {
                if (parts.Contains(part))
                    return group;
            }
            return null;
        }
    }
}

