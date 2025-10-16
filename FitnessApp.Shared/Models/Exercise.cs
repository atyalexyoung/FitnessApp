namespace FitnessApp.Shared.Models
{
    public class Exercise
    {
        /// <summary>
        /// Identifier for the exercise
        /// </summary>
        public string Id { get; set; } = null!;

        /// <summary>
        /// The human-readable-friendly name for the exercise.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// General description for the exercise
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Additional infromation about the exercise including Do's/Dont's, benefits, form cues, progressions, etc.
        /// </summary>
        public string? AdditionalInformation { get; set; }

        /// <summary>
        /// List of urls for the images. The first image URL is what will display for the overall exercise.
        /// </summary>
        public List<string> ImageUrls { get; set; } = new();

        /// <summary>
        /// List of URLs for the videos associated with the exercise.
        /// </summary>
        public List<string> VideoUrls { get; set; } = new();

        /// <summary>
        /// Collection of ExerciseBodyPart objects which tie this exercise to the BodyParts.BodyPart enums that are associated with the exercise
        /// </summary>
        public ICollection<ExerciseBodyPart> ExerciseBodyParts { get; set; } = new List<ExerciseBodyPart>();
        
        /// <summary>
        /// Collection of ExerciseTag objects which tie this exercise to the ExerciseTypes.ExerciseTypeTags enums that are associated with the exercise
        /// </summary>
        public ICollection<ExerciseTag> ExerciseTags { get; set; } = new List<ExerciseTag>();
    }
}
