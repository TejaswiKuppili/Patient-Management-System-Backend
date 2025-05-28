using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Repository.Interfaces
{
    public interface IPatientRepository
    {
        Task<bool> DeleteAsync(int id);
        Task UpdateAsync(Patient patient);
        Task<Patient?> GetByIdAsync(int id);
        Task<List<Patient>> GetAllAsync();
        Task AddAsync(Patient patient);

    }
}
