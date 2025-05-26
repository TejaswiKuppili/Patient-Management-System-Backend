using PatientManagementSystem.Services.Interfaces;
using PatientManagementSystem.Common.DTOs;


public class AuthService : IAuthService
{
    private readonly IUserService userService;
    private readonly ITokenService tokenService;
    
    public AuthService(
        IUserService userService,
        ITokenService tokenService)
    {
        this.userService = userService;
        this.tokenService = tokenService;
       
    }

    public async Task<LoginResponseDto> LoginAsync(string email, string password, string ipAddress)
    {
        UserDto user = await userService.ValidateUserAsync(email, password);
        

        string accessToken = tokenService.GenerateAccessToken(user);
        string refreshToken = tokenService.GenerateRefreshToken(user);
       
        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}
