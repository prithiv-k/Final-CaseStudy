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
    [ApiVersion("1.0")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo<User> _repo;
        private readonly IEmployeeRepo<Employee> _employeeRepo; // ✅ Inject EmployeeRepo
        private readonly IConfiguration _config;

        public UserController(IUserRepo<User> repo, IEmployeeRepo<Employee> employeeRepo, IConfiguration config)
        {
            _repo = repo;
            _employeeRepo = employeeRepo;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("ValidateUser")]
        public IActionResult Validate([FromBody] User user)
        {
            var result = _repo.ValidateUser(user);
            if (result == null)
                return Unauthorized("Invalid credentials");

            var employee = _employeeRepo.GetAllEmployees()
                .FirstOrDefault(e => e.Email == result.Email);

            var token = GenerateJwtToken(result, employee?.EmployeeId ?? 0);
            return Ok(new
            {
                token,
                role = result.Role
            });
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("Add")]
        public IActionResult Add(User user)
        {
            var addedUser = _repo.AddUser(user);
            return Ok(addedUser);
        }

        [Authorize(Roles = "Admin,Manager,Employee")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var users = _repo.GetAllUsers();
            return Ok(users);
        }

        [HttpOptions("ValidateUser")]
        [AllowAnonymous]
        public IActionResult ValidateUserOptions()
        {
            return Ok();
        }

        // 🔐 JWT Generator with employeeId included
        [NonAction]
        private string GenerateJwtToken(User user, int employeeId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("employeeId", employeeId.ToString()) // ✅ Added here
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
