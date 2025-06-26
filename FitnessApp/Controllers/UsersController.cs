using FitnessApp.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest req)
        {
            var user = await _userService.RegisterAsync(req.Username, req.Password);
            return user == null ? Conflict("Username taken") : Ok("Registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var token = await _userService.LoginAsync(req.Username, req.Password);
            return token == null ? Unauthorized("Invalid credentials") : Ok(new { token });
        }
    }

    public record LoginRequest(string Username, string Password);
}
