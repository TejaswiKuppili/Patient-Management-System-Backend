using PatientManagementSystem.Common.DTOs;

namespace PatientManagementSystem.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserDto user);
        string GenerateRefreshToken(UserDto user);
    }
}


