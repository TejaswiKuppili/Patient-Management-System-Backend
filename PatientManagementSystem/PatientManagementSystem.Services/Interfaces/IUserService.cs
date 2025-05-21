using PatientManagementSystem.Common.DTOs;
namespace PatientManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Defines user-related business logic.
    /// </summary>
    public interface IUserService
    {
        Task<UserAndRoleDto> GetUsersAndRolesAsync();
        Task CreateUser(UserDto userDetails);
        Task UpdateUserRole(int userId, string newRoleName);
    }

}
