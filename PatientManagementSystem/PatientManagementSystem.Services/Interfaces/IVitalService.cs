using PatientManagementSystem.Common.DTOs;
using PatientManagementSystemAPI.Common.DTOs;


namespace PatientManagementSystem.Services.Interfaces
{
    public interface IVitalService
    {
        Task<ApiResponse<List<VitalDto>>> GetVitalsAsync(int patientId);
    }
}
