using FitnessApp.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Register([FromBody] LoginRequest req)
        {
            _logger.LogDebug("POST to register user recieved at {Time}", DateTime.UtcNow);

            var user = await _userService.RegisterAsync(req.Username, req.Password);

            if (user == null)
            {
                _logger.LogDebug("Conflict, username: {username} is already taken at {time}", req.Username, DateTime.UtcNow);
                return Conflict("Username taken");
            }
            else
            {
                _logger.LogDebug("Registered user {user} at {time}.", req.Username, DateTime.UtcNow);
                return Ok("Registerd");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var token = await _userService.LoginAsync(req.Username, req.Password);

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
