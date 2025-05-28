using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Services.Interfaces
{
    public interface IPatientService
    {
        Task<ApiResponse<PatientDto>> AddAsync(PatientDto dto);
        Task<ApiResponse<List<PatientDto>>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, PatientDto dto);

    }
}
