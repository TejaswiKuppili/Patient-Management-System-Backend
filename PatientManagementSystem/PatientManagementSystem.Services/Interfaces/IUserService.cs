using PatientManagementSystem.Common.DTOs;

namespace PatientManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Contract for user-related services.
    /// </summary>
    public interface IUserService
    {
        //Task<UserDto?> ValidateUserAsync(string email, string password);

        Task<ApiResponse<UserAndRoleDto>> GetUsersAndRolesAsync();
        Task<ApiResponse<string>> CreateUserAsync(UserDto userDetails);
        Task<ApiResponse<string>> UpdateUserRoleAsync(int userId, string newRoleName);
        Task<UserDto> ValidateUserAsync(string email, string password);

        Task<ApiResponse<UserIdResponseDto>> GetUserByIdAsync(int userId);
        Task<ApiResponse<UserIdResponseDto>> DeleteUserAsync(int userId);

    }
}