using PatientManagementSystem.Common.DTOs;
namespace PatientManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Defines user-related business logic.
    /// </summary>
    public interface IUserService
    {
        Task<ApiResponse<UserAndRoleDto>> GetUsersAndRolesAsync();
        Task<ApiResponse<string>> CreateUserAsync(UserDto userDetails);
        Task<ApiResponse<string>> UpdateUserRoleAsync(int userId, string newRoleName);
    }
}