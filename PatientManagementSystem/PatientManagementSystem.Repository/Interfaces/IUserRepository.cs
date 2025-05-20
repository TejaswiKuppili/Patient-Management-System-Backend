using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Repository.Interfaces
{
    /// <summary>
    /// Contract for user data access operations.
    /// </summary>
    public interface IUserRepository
    {
        Task<UserAndRoleDto> GetUsersAndRolesAsync();
        Task UpdateUserRoleAsync(int userId, string newRoleName);
    }

}
