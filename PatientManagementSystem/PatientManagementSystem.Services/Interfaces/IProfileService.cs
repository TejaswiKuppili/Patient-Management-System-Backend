using PatientManagementSystem.Common.DTOs;
using System.Threading.Tasks;

namespace PatientManagementSystem.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ApiResponse<bool>> DeleteProfileAsync(int userId);
        Task<ApiResponse<ProfileDto?>> UpdateProfileAsync(ProfileDto profileDto);
        Task<ApiResponse<ProfileDto?>> CreateProfileAsync(ProfileDto profileDto);
        Task<ApiResponse<ProfileDto?>> GetProfileByUserIdAsync(int userId);
    }
}
