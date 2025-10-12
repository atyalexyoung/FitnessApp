using FitnessApp.Extensions;
using FitnessApp.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace FitnessApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUsersService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest req, CancellationToken cancellationToken)
        {
            _logger.LogDebug("POST to register user recieved at {Time}", DateTime.UtcNow);

            var result = await _userService.RegisterAsync(req.Username, req.Password, cancellationToken);

            if (result == null)
            {
                _logger.LogWarning("Result was null for registering username: {user}", req.Username);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred.");
            }

            if (result.IsFailure)
            {
                if (result.ErrorType == ErrorType.Conflict)
                    return Conflict(result.Error);

                return result.ToActionResult();
            }

            // Return 201 Created with user ID (or email, username, etc.)
            return StatusCode(201, new { Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req, CancellationToken cancellationToken)
        {
            var token = await _userService.LoginAsync(req.Username, req.Password, cancellationToken);

            if (token == null) { return Unauthorized("Invalid credentials."); }

            if (token.IsFailure)
            {
                if (token.Value == null) { return Unauthorized("Invalid credentials"); }
                else { return Unauthorized($"{token.Value}"); }
            }
            else { return Ok(new { token.Value }); }

        }
    }

    public record LoginRequest(string Username, string Password);
}
