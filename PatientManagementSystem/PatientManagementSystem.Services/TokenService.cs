using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
{   /// <summary>
/// Service for generating JWT access and refresh tokens.
/// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
       // private readonly IJwtHelper jwtHelper;
        private readonly ILogger<TokenService> logger;

        public TokenService(
            IConfiguration configuration,
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
         //   IJwtHelper jwtHelper,
            ILogger<TokenService> logger)
        {
            this.configuration = configuration;
            this.refreshTokenRepository = refreshTokenRepository;
            this.userRepository = userRepository;
            this.httpContextAccessor = httpContextAccessor;
         //   this.jwtHelper = jwtHelper;
            this.logger = logger;
        }

        /// <summary>
        /// Generates a JWT access token for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateAccessToken(UserDto user)
        {
            try
            {
                byte[] key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
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

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error generating access token for user {UserId}", user.Id);
                throw;
            }
        }
        /// <summary>
        /// Generates a refresh token for the specified user and saves it to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateRefreshToken(UserDto user)
        {
            try
            {
                byte[] randomBytes = new byte[64];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomBytes);
                string token = Convert.ToBase64String(randomBytes);

                var refreshToken = new RefreshToken
                {
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddDays(double.Parse(configuration["Jwt:RefreshTokenExpiryDays"])),
                    CreatedAt = DateTime.UtcNow,
                    CreatedByIp = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                    UserId = user.Id,
                    IsRevoked = false
                };

                refreshTokenRepository.SaveRefreshTokenAsync(refreshToken);
                return token;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error generating refresh token for user {UserId}", user.Id);
                throw;
            }
        }

        //public async Task<LoginResponseDto> VerifyAndGenerateTokenAsync(LoginResponseDto request)
        //{
        //    var principal = jwtHelper.GetPrincipalFromExpiredToken(request.AccessToken);
        //    if (principal == null)
        //        return null;

        //    var storedToken = await refreshTokenRepository.GetByTokenAsync(request.RefreshToken);
        //    if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiresAt < DateTime.UtcNow)
        //        return null;

        //    // Revoke current token
        //    storedToken.IsRevoked = true;
        //    await refreshTokenRepository.RevokeTokenAsync(storedToken);

        //    var userIdClaim = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        //    if (!int.TryParse(userIdClaim, out int userId))
        //        return null;

        //    var userEntity = await userRepository.GetByIdAsync(userId);
        //    if (userEntity == null)
        //        return null;

        //    var userDto = new UserDto
        //    {
        //        Id = userEntity.Id,
        //        Name = userEntity.Name,
        //        Email = userEntity.Email,
        //        RoleName = userEntity.Role.Name
        //    };

        //    string newAccessToken = GenerateAccessToken(userDto);
        //    string newRefreshToken = GenerateRefreshToken(userDto);

        //    return new LoginResponseDto
        //    {
        //        AccessToken = newAccessToken,
        //        RefreshToken = newRefreshToken
        //    };
        //}
    }
}
