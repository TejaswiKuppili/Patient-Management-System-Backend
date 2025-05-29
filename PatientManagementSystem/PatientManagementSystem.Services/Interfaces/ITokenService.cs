using PatientManagementSystem.Common.DTOs;

namespace PatientManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Contract for token generation services.
    /// </summary>
    public interface ITokenService
    {
        string GenerateAccessToken(UserDto user);
        string GenerateRefreshToken(UserDto user);
    }
}


