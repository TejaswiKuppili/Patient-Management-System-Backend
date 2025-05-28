using PatientManagementSystem.Services.Interfaces;
using PatientManagementSystem.Common.DTOs;
using Microsoft.Extensions.Logging;

/// <summary>
/// Service for handling authentication-related operations, including user login and token generation.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUserService userService;
    private readonly ITokenService tokenService;
    private readonly ILogger<AuthService> logger;


    public AuthService(
        IUserService userService,
        ITokenService tokenService,ILogger<AuthService> logger)
    {
        this.userService = userService;
        this.tokenService = tokenService;
        this.logger = logger;

    }

    /// <summary>
    /// Authenticates a user with the provided credentials and generates authentication tokens.
    /// </summary>
    
    public async Task<LoginResponseDto?> LoginAsync(string email, string password, string ipAddress)
    {
        try
        {
            UserDto user = await userService.ValidateUserAsync(email, password);
            if (user == null)
            {
                return null;
            }

            string accessToken = tokenService.GenerateAccessToken(user);
            string refreshToken = tokenService.GenerateRefreshToken(user);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while logging in user with email: {Email}", email);
            return null;
        }
    }
}