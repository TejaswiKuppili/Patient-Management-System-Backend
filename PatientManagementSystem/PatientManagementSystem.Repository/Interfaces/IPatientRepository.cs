using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Repository.Interfaces
{
    /// <summary>
    /// Contract for patient repository operations.
    /// </summary>
    public interface IPatientRepository
    {
        Task<bool> DeleteAsync(int id);
        Task UpdateAsync(Patient patient);
        Task<Patient?> GetByIdAsync(int id);
        
        Task<Patient> AddAsync(Patient patient);
        Task<List<Patient>> GetAllAsync();
    }
}
