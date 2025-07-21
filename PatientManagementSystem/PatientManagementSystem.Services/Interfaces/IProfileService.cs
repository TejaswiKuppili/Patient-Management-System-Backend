using PatientManagementSystem.Common.DTOs;
using System.Threading.Tasks;

namespace PatientManagementSystem.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDto?> GetProfileByUserIdAsync(int userId);
        Task CreateProfileAsync(ProfileDto profileDto);
        Task<bool> UpdateProfileAsync( ProfileDto profileDto);
        Task<bool> DeleteProfileAsync(int userId);
    }
}
