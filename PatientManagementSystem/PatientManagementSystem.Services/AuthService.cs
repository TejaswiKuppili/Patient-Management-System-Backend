using Microsoft.Extensions.Logging;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Services.Interfaces;

/// <summary>
/// Service for handling authentication-related operations, including user login and token generation.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUserService userService;
    private readonly ITokenService tokenService;
    private readonly ILogger<AuthService> logger;
    //private readonly IUserRepository userRepository;

    public AuthService(
        IUserService userService,
        ITokenService tokenService, ILogger<AuthService> logger)
    {
        this.userService = userService;
        this.tokenService = tokenService;
        this.logger = logger;
       // this.userRepository = userRepository;

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

            // Get full user entity
            //ApplicationUser? userEntity = await userRepository.GetUserDetailsAsync(user.Id);
            //if (userEntity == null)
            //{
            //    logger.LogWarning("User details not found for validated user with ID: {UserId}", user.Id);
            //    return null;
            //}

            // Map to UserIdResponseDto inside service
            UserIdResponseDto userDetails = new UserIdResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleName = user.RoleName
            };

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserDetails = userDetails
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while logging in user with email: {Email}", email);
            return null;
        }
    }

}