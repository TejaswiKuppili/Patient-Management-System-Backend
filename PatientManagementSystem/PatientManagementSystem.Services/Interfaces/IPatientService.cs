using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Contract for patient-related services.
    /// </summary>
    public interface IPatientService
    {

        Task<ApiResponse<List<PatientDto>>> GetAllAsync();
        Task<ApiResponse<PatientDto?>> GetByIdAsync(int id);
        Task<ApiResponse<PatientDto>> AddAsync(PatientDto dto);
        Task<bool> UpdateAsync(int id, PatientDto dto);

    }
}
