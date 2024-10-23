using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using hrm_web_api.Models.Dtos;
using hrm_web_api.Models.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using hrm_web_api.Healpers;
using hrm_web_api.Data;
using Microsoft.EntityFrameworkCore;

namespace hrm_web_api.Services
{
    public class AuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ApplicationDbContext _dbContext;

        public AuthService(IOptions<JwtSettings> jwtSettings, ApplicationDbContext dbContext)
        {
            _jwtSettings = jwtSettings.Value;
            _dbContext = dbContext;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            // Validate user credentials (use database or any other method)
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return null; // Invalid login
            }

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = tokenDescriptor.Expires.Value
            };
        }
    }
}
