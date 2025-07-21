using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Repository.Interfaces
{
    public interface IProfileRepository
    {
        Task<Profile?> GetByUserIdAsync(int userId);
        Task AddAsync(Profile profile);
        Task UpdateAsync(Profile profile);
        Task DeleteAsync(Profile profile);
    }
}
