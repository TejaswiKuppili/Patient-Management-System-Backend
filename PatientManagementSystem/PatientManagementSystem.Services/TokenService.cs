using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PatientManagementSystem.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public TokenService(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this.refreshTokenRepository = refreshTokenRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GenerateAccessToken(UserDto user)
        {
            byte[]? key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            JwtSecurityTokenHandler? tokenHandler = new JwtSecurityTokenHandler();
            

            SecurityTokenDescriptor? tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.RoleName)
    }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["Jwt:AccessTokenExpiryMinutes"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };


            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(UserDto user)
        {
            byte[]? randomBytes = new byte[64];
            using RandomNumberGenerator? rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            string? token = Convert.ToBase64String(randomBytes);

            RefreshToken? refreshToken = new RefreshToken
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddDays(double.Parse(configuration["Jwt:RefreshTokenExpiryDays"])),
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),

                UserId = user.Id,
                IsRevoked = false
            };

            refreshTokenRepository.SaveRefreshToken(refreshToken);
            

            return token;
        }
    }
}
