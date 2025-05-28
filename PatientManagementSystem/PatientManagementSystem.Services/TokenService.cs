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
using Microsoft.Extensions.Logging;
namespace PatientManagementSystem.Services
{
    /// <summary>
    /// Generates JWT access and refresh tokens for user authentication.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<TokenService> logger;
        public TokenService(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository, IHttpContextAccessor httpContextAccessor, ILogger<TokenService> logger)
        {
            this.configuration = configuration;
            this.refreshTokenRepository = refreshTokenRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }
        /// <summary>
        /// Generates a JWT access token for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Token string</returns>
        public string GenerateAccessToken(UserDto user)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error generating access token for user {UserId}", user.Id);
                throw new InvalidOperationException("Failed to generate access token", ex);
            }
        }
        /// <summary>
        /// Generates a refresh token for the specified user and saves it to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Token string</returns>
        public string GenerateRefreshToken(UserDto user)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error generating refresh token for user {UserId}", user.Id);
                throw new InvalidOperationException("Failed to generate refresh token", ex);
            }
        }
    }
}
