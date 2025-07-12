using FitnessApp.Helpers;
using FitnessApp.Interfaces.Repositories;
using FitnessApp.Interfaces.Services;
using FitnessApp.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FitnessApp.Services
{
    public class UsersService : IUsersService
    {
        private readonly IConfiguration _config;
        private readonly IUsersRepository _users;
        private readonly ILogger _logger;

        public UsersService(IConfiguration config, IUsersRepository usersRepo, ILogger<UsersService> logger)
        {
            _config = config;
            _users = usersRepo;
            _logger = logger;
        }

        /// <summary>
        /// Will attempt to register the new user with username and password.
        /// </summary>
        ///
        /// <param name="username">The username of new user.</param>
        /// <param name="password">Password of new user.</param>
        /// <returns><see cref="Result"/> type.</returns>
        public async Task<Result<User?>> RegisterAsync(string username, string password)
        {
            // if we can get a user, then the username is already taken.
            if (await _users.GetByUsernameAsync(username) != null)
            {
                _logger.LogTrace("Username: {username} already exists in {class} at {time}", username, nameof(RegisterAsync), DateTime.UtcNow);
                return Result.Fail<User?>("Username taken", ErrorType.Conflict);
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = username,
                PasswordHash = HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            if (await _users.AddAsync(user))
            {
                _logger.LogTrace("New User added with username: {username} at {time}", username, DateTime.UtcNow);
                return Result.Ok<User?>(user);
            }
            else
            {
                _logger.LogError("Failed to add new user with username: {username} at {time}", username, DateTime.UtcNow);
                return Result.Fail<User?>("Failed to add new user.", ErrorType.Internal);
            }
        }

        /// <summary>
        /// Will attempt to login as user and will give back token.
        /// </summary>
        /// <param name="username">Usename of attempted login.</param>
        /// <param name="password">Password of attempted login.</param>
        /// <returns>String of JWT token.</returns>
        public async Task<Result<string?>> LoginAsync(string username, string password)
        {
            var user = await _users.GetByUsernameAsync(username);

            // validate user
            if (user == null)
            {
                _logger.LogError("Couldn't find user: {username} in {class}, at {time}", username, nameof(LoginAsync), DateTime.UtcNow);
                return Result.Fail<string?>("Couldn't find user.", ErrorType.NotFound);
            }

            // validate password
            if (user.PasswordHash != HashPassword(password))
            {
                _logger.LogError("Incorrect password for {username} in {class}, at {time}", username, nameof(LoginAsync), DateTime.UtcNow);
                return Result.Fail<string?>("Incorrect password", ErrorType.Unauthorized);
            }

            // get JWT
            if (!TryGenerateJwt(user, out string jwt))
            {
                _logger.LogError("Failed to create JWT for user: {username} at {time}", username, DateTime.UtcNow);
                return Result.Fail<string?>("Failed to create JWT", ErrorType.Internal);
            }

            return Result.Ok<string?>(jwt);
        }

        public Task<User?> GetByIdAsync(string id)
        {
            return _users.GetByIdAsync(id);
        }

        private bool TryGenerateJwt(User user, out string jwt)
        {
            try
            {
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                };

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256));

                jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when generating JWT with exception: {exception}", ex);
                jwt = default!;
                return false;
            }

        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
