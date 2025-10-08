using FitnessApp.Data;
using FitnessApp.Interfaces.Repositories;
using FitnessApp.Shared.Enums;
using FitnessApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ILogger<ExerciseRepository> _logger;
        private readonly FitnessAppDbContext _dbContext;

        public ExerciseRepository(ILogger<ExerciseRepository> logger, FitnessAppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Exercise>> GetFilteredAsync(
            List<BodyParts.BodyPartType>? bodyPartTypes = null,
            List<BodyParts.BodyPart>? bodyParts = null,
            List<ExerciseTypes.ExerciseTypeTag>? exerciseTags = null)
        {
            var query = _dbContext.Exercises
                .Include(e => e.ExerciseBodyParts)
                .Include(e => e.ExerciseTags)
                .AsQueryable();
            
            // Build list of all body parts to search for
            var searchBodyParts = new HashSet<BodyParts.BodyPart>();
            
            // Add body parts from types
            if (bodyPartTypes?.Any() == true)
            {
                foreach (var type in bodyPartTypes)
                {
                    if (BodyParts.Groups.TryGetValue(type, out var parts))
                    {
                        foreach (var part in parts)
                            searchBodyParts.Add(part);
                    }
                }
            }
            
            // Add specific body parts
            if (bodyParts?.Any() == true)
            {
                foreach (var part in bodyParts)
                    searchBodyParts.Add(part);
            }
            
            // Apply filters (OR logic across all filters)
            var hasBodyPartFilter = searchBodyParts.Any();
            var hasTagFilter = exerciseTags?.Any() == true;
            
            if (hasBodyPartFilter || hasTagFilter)
            {
                query = query.Where(e =>
                    (hasBodyPartFilter && e.ExerciseBodyParts.Any(eb => searchBodyParts.Contains(eb.BodyPart))) ||
                    (hasTagFilter && e.ExerciseTags.Any(et => exerciseTags!.Contains(et.Tag)))
                );
            }
            
            return await query.ToListAsync();
        }

        public async Task<Exercise?> GetByIdAsync(string id)
        {
            return await _dbContext.Exercises
                .Include(e => e.ExerciseBodyParts)
                .Include(e => e.ExerciseTags)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}