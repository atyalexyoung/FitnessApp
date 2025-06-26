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
            Biceps,
            Triceps,
            Forearms,

            // Shoulders
            Deltoids,

            // Chest
            Chest,

            // Back
            Lats,
            Rhomboids,
            Trapezius,
            LowerBack,

            // Core
            Abdominals,
            Obliques,

            // Legs
            Glutes,
            Quads,
            Hamstrings,
            Calves
        }

        /// <summary>
        /// Body part groupings
        /// </summary>
        public enum BodyPartType
        {
            Arms,
            Shoulders,
            Chest,
            Back,
            Core,
            Legs
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

