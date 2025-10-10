using FitnessApp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult(this Result result)
        {
            return result.ErrorType switch
            {
                ErrorType.Conflict => new ConflictObjectResult(result.Error),
                ErrorType.Validation => new BadRequestObjectResult(result.Error),
                ErrorType.NotFound => new NotFoundObjectResult(result.Error),
                _ => new ObjectResult(result.Error) { StatusCode = 500 }
            };
        }

        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsFailure)
                return result.ToActionResult();

            return new OkObjectResult(result.Value);
        }
    }
}
