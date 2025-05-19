using PatientManagementSystem.Common.DTOs;
namespace PatientManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Defines user-related business logic.
    /// </summary>
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersWithRolesAsync();
    }

}
