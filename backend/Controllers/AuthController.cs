using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using GameStore.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace GameStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private static Dictionary<string, string> _refreshTokens = new Dictionary<string, string>();


        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private JwtSecurityToken GenerateAccessToken(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
                    SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        [HttpPost("api/login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Check user credentials (in a real application, you'd authenticate against a database)
            if (model is { Username: "demo", Password: "password" })
            {
                var token = GenerateAccessToken(model.Username);
                // Generate refresh token
                var refreshToken = Guid.NewGuid().ToString();

                // Store the refresh token (in-memory for simplicity)
                _refreshTokens[refreshToken] = model.Username;

                //return access token and refresh token
                return Ok(new
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken
                });

            }
            // unauthorized
            return Unauthorized("Invalid credentials");
        }

        [HttpPost("api/token/refresh")]
        public IActionResult Refresh([FromBody] RefreshRequest request)
        {
            if (_refreshTokens.TryGetValue(request.RefreshToken, out var userId))
            {
                // Generate a new access token
                var token = GenerateAccessToken(userId);

                // Return the new access token to the client
                return Ok(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return BadRequest("Invalid refresh token");
        }
        [HttpPost("api/token/revoke")]
        public IActionResult Revoke([FromBody] RevokeRequest request)
        {
            if (_refreshTokens.ContainsKey(request.RefreshToken))
            {
                // Remove the refresh token to revoke it
                _refreshTokens.Remove(request.RefreshToken);
                return Ok("Token revoked successfully");
            }

            return BadRequest("Invalid refresh token");
        }

        public class RefreshRequest
        {
            public string RefreshToken { get; set; }
        }
        public class RevokeRequest
        {
            public string RefreshToken { get; set; }
        }
    }
}
