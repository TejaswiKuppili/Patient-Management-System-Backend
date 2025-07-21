using PatientManagementSystem.Common.DTOs;

namespace PatientManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Service interface  for authentication services.
    /// </summary>
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(string email, string password, string ipAddress);
    }

}
