using PatientManagementSystem.Common.DTOs;
using PatientManagementSystemAPI.Common.DTOs;


namespace PatientManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Contract for vital-related services.
    /// </summary>
    public interface IVitalService
    {
        Task<ApiResponse<List<VitalDto>>> GetVitalsAsync(int patientId);
        Task<ApiResponse<bool>> AddVitalsAsync(VitalDto vitalDto);
    }
}
