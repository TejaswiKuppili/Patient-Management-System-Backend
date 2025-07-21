using PatientManagementSystem.Common.DTOs;

namespace PatientManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Service interface for managing doctor-related operations.
    /// </summary>
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetDoctorsAsync();
        Task<DoctorDto?> GetDoctorByIdAsync(int id);
        Task<bool> DoctorExistsAsync(int id);
    }
}
