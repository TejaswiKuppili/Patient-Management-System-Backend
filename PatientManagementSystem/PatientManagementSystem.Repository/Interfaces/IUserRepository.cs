using PatientManagementSystem.Common.DTOs;

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
        Task UpdateUserRoleAsync(int userId, string newRoleName);
    }

}
