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
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AuthController(UserManager<Customer> userManager, SignInManager<Customer> signInManager, IConfiguration config, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            Console.WriteLine($"Received Email: {request.Email}"); // Debugging log

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new { code = "InvalidEmail", description = "Email is required." });
            }

            var user = new Customer
            {
                UserName = request.Email.ToLower(),
                Email = request.Email.ToLower(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Mobile = request.Mobile,
                Address = request.Address,
                Details = request.Details ?? "New User",
                CustomerFrom = DateTime.UtcNow
            };

            var passwordHasher = new PasswordHasher<Customer>();
            user.PasswordHash = passwordHasher.HashPassword(user, request.Password);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"❌ Registration Error: {error.Code} - {error.Description}");
                }
                return BadRequest(result.Errors);
            }

            // Assign "Customer" role
            if (!await _roleManager.RoleExistsAsync("Customer"))
            {
                await _roleManager.CreateAsync(new IdentityRole<int>("Customer"));
            }
            await _userManager.AddToRoleAsync(user, "Customer");

            Console.WriteLine($"✅ User Registered: {user.Email}");

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new { Message = "User registered successfully.", User = user });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(request.Email.ToLower());
            if (user == null)
            {
                Console.WriteLine("❌ Login failed: User not found.");
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            // Debugging: Log stored password hash
            Console.WriteLine($"🔹 Stored Password Hash: {user.PasswordHash}");

            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
            {
                Console.WriteLine($"❌ Login failed: Invalid password for {user.Email}");
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles);
            Console.WriteLine($"✅ User Logged In: {user.Email}");

            return Ok(new { Token = token, UserId = user.Id, Name = user.FirstName + " " + user.LastName, Roles = roles });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound("User not found.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetResult = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (resetResult.Succeeded)
                return Ok("Password reset successfully.");
            else
                return BadRequest(resetResult.Errors);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }
            return Ok(user);
        }

        private string GenerateJwtToken(Customer user, IList<string> roles)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
