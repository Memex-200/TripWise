using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TripWise.Application.DTOs;
using TripWise.Domain.Entities;

namespace TripWise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<Customer> userManager, SignInManager<Customer> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var emailAddress = new System.Net.Mail.MailAddress(request.Email);
            }
            catch
            {
                return BadRequest(new { code = "InvalidEmail", description = "Email is invalid." });
            }

            var normalizedEmail = _userManager.NormalizeEmail(request.Email);
            var user = new Customer
            {
                UserName = normalizedEmail,
                Email = normalizedEmail,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Mobile = request.Mobile,
                Address = request.Address,
                Details = request.Details ?? "New User",
                CustomerFrom = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, request.Password)))
                return Unauthorized("Invalid credentials.");

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token, UserId = user.Id, Name = user.FirstName + " " + user.LastName });
        }

        private string GenerateJwtToken(Customer user)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
