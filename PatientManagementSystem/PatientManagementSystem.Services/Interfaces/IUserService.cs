using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.Entities;
namespace PatientManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Defines user-related business logic.
    /// </summary>
    public interface IUserService
    {
        //Task<UserDto?> ValidateUserAsync(string email, string password);

        Task<ApiResponse<UserAndRoleDto>> GetUsersAndRolesAsync();
        Task<ApiResponse<string>> CreateUserAsync(UserDto userDetails);
        Task<ApiResponse<string>> UpdateUserRoleAsync(int userId, string newRoleName);
        Task<UserDto> ValidateUserAsync(string email, string password);

       

    }
}