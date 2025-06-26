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

        public UsersService(IConfiguration config, IUsersRepository usersRepo)
        {
            _config = config;
            _users = usersRepo;
        }

        public async Task<User?> RegisterAsync(string username, string password)
        {
            if (await _users.GetByUsernameAsync(username) != null)
                return null;

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = username,
                PasswordHash = HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            await _users.AddAsync(user);
            return user;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var user = await _users.GetByUsernameAsync(username);

            if (user == null || user.PasswordHash != HashPassword(password))
                return null;

            return GenerateJwt(user);
        }

        public Task<User?> GetByIdAsync(string id)
        {
            return _users.GetByIdAsync(id);
        }

        private string GenerateJwt(User user)
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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
