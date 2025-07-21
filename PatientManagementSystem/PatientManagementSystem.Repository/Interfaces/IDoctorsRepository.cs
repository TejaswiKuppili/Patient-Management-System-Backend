using PatientManagementSystem.Common.DTOs;

namespace PatientManagementSystem.Repository.Interfaces
{
    /// <summary>
    /// Interface for Doctor Repository operations.
    /// </summary>
    public interface IDoctorRepository
    {
       
        Task<IEnumerable<DoctorDto>> GetAllAsync();

        
        Task<DoctorDto?> GetByIdAsync(int id);

        
        Task<bool> ExistsAsync(int id);
    }
}
