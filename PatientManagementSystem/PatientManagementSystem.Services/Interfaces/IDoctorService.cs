using PatientManagementSystem.Common.DTOs;

namespace PatientManagementSystem.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetDoctorsAsync();
        Task<DoctorDto?> GetDoctorByIdAsync(int id);
        Task<bool> DoctorExistsAsync(int id);
    }
}
