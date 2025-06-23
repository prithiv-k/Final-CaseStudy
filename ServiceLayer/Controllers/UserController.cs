using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo<User> _repo;
        private readonly IConfiguration _config;

        public UserController(IUserRepo<User> repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // 🔐 Login
        [AllowAnonymous]
        [HttpPost("validate")]
        public IActionResult Validate(User user)
        {
            var result = _repo.ValidateUser(user);
            if (result == null)
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(result);
            return Ok(new { token, role = result.Role });
        }

        // ➕ Register new user
        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            var addedUser = _repo.AddUser(user);
            return Ok(addedUser);
        }

        // 📋 Get all users
        [HttpGet("All")]
        public IActionResult GetAll()
        {
            var users = _repo.GetAllUsers();
            return Ok(users);
        }

        // ❌ Delete user
        [HttpDelete("{id}/Delete")]
        public IActionResult Delete(int id)
        {
            var deletedUser = _repo.DeleteUser(id);
            if (deletedUser == null)
                return NotFound("User not found");

            return Ok("User deleted");
        }

        // 🛠️ Non-action method to generate JWT
        [NonAction]
        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
