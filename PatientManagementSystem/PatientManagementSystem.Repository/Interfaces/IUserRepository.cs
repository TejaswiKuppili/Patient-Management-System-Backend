using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Repository.Interfaces
{
    /// <summary>
    /// Contract for user data access operations.
    /// </summary>
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersWithRolesAsync();
    }

}
