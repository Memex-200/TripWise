using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public AuthController(
            UserManager<Customer> userManager,
            SignInManager<Customer> signInManager,
            IConfiguration config,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _roleManager = roleManager;
        }

        #region Registration
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            Console.WriteLine($"Received Registration Request for: {request.Email}");

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new { code = "InvalidEmail", description = "Email is required." });
            }

            var user = new Customer
            {
                UserName = request.Email.ToLower(),
                Email = request.Email.ToLower(),
                NormalizedEmail = _userManager.NormalizeEmail(request.Email),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Mobile = request.Mobile,
                Address = request.Address,
                Details = request.Details ?? "New User",
                CustomerFrom = DateTime.UtcNow
            };

            // Let UserManager handle password hashing automatically
            var result = await _userManager.CreateAsync(user, request.Password);
 
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"❌ Registration Error: {error.Code} - {error.Description}");
                }
                return BadRequest(result.Errors);
            }

            // Assign "Customer" role if it doesn't exist
            if (!await _roleManager.RoleExistsAsync("Customer"))
            {
                await _roleManager.CreateAsync(new IdentityRole<int>("Customer"));
            }

            await _userManager.AddToRoleAsync(user, "Customer");

            Console.WriteLine($"✅ User Registered: {user.Email}");

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new
            {
                Message = "User registered successfully.",
                User = new
                {
                    user.Id,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                 }
            });
        }
               
        #endregion

        #region Login

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Fetch the user by email (case insensitive)
            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null)
            {
                Console.WriteLine("❌ Login failed: User not found.");
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            // Use CheckPasswordAsync() to compare entered password with the stored hash
            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
            {
                Console.WriteLine($"❌ Login failed: Invalid password for {user.Email}");
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            // Get the user's roles
            var roles = await _userManager.GetRolesAsync(user);

            // Generate the JWT token
            var token = GenerateJwtToken(user, roles);

            Console.WriteLine($"✅ User Logged In: {user.Email}");

            return Ok(new
            {
                Token = token,
                UserId = user.Id,
                Name = $"{user.FirstName} {user.LastName}",
                Roles = roles
            });
        }

        #endregion

        #region Forgot Password

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new { Message = "Email is required." });
            }

            // Normalize to uppercase to match NormalizedUserName column
            var normalizedUserName = request.Email.Trim().ToUpperInvariant();

            // Query against NormalizedUserName column
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);

            // Security: Always return 200 to prevent email enumeration
            if (user == null)
            {
                return Ok(new { Message = "If the email exists, a reset link has been sent." });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // TEMPORARY TESTING RESPONSE - REMOVE IN PRODUCTION
            return Ok(new
            {
                Message = "Password reset token (TEST MODE)",
                Token = token,
                Email = user.Email,
                NormalizedUserName = user.NormalizedUserName
            });
        }

        #endregion

        #region Password Reset

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.NewPassword) ||
                string.IsNullOrWhiteSpace(request.Token))
            {
                return BadRequest(new { Message = "Email, token, and new password are required." });
            }

            // Normalize email to match NormalizedUserName format
            var normalizedUserName = _userManager.NormalizeEmail(request.Email);

            // Find user by NormalizedUserName where emails are actually stored
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);

            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            var resetResult = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (!resetResult.Succeeded)
            {
                return BadRequest(new
                {
                    Message = "Password reset failed",
                    Errors = resetResult.Errors.Select(e => e.Description)
                });
            }

            return Ok(new { Message = "Password reset successfully." });
        }

        #endregion


        #region Get User

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            return Ok(new
            {
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.Phone,
                //user.Mobile,
                user.Address,
                user.Details,
                user.CustomerFrom
            });
        }

        #endregion

        #region Logout

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest(new { Message = "No user was logged in." });
            }

            // Logout logic for authenticated users
            Console.WriteLine("✅ User logged out.");

            // Since JWT is stateless, actual token invalidation would require additional strategies
            // (e.g., token blacklisting or short token expiration). Client should delete the token.

            return Ok(new { Message = "Logged out successfully." });
        }

        #endregion

        private string GenerateJwtToken(Customer user, IList<string> roles)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
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
