using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Repository.Interfaces
{
    /// <summary>
    /// Contract for user data access operations.
    /// </summary>
    public interface IUserRepository
    {
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<UserAndRoleDto> GetUsersAndRolesAsync();
        Task<UserDto> CreateUserAsync(UserDto newUser);
        Task<ApplicationUser?> GetUserDetailsAsync(int userId);
        Task UpdateUserRoleAsync(int userId, string newRoleName); 

    }

}
