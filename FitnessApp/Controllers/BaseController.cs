using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessApp.Controllers
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// User ID obtained from the ClaimTypes.NameIdentifier
        /// </summary>
        protected string UserId =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedAccessException("No user ID found"); 
    }
}
