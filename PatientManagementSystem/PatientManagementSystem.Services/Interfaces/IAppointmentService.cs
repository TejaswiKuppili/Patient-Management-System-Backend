using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Services.Interfaces
{
    /// <summary>
    /// Interface for Appointment Service
    /// </summary>
    public interface IAppointmentService
    {
        Task<ApiResponse<IEnumerable<AppointmentDto>>> GetAppointmentsByDoctorIdAsync(int doctorId);
        Task<ApiResponse<IEnumerable<AppointmentDto>>> GetAppointmentsWithDoctorNameAsync();
        Task<ApiResponse<string>> CreateAppointmentAsync(AppointmentDto appointment);
        Task<ApiResponse<string>> UpdateAppointmentAsync(AppointmentDto appointment);
        Task<ApiResponse<string>> DeleteAppointmentAsync(int id);
    }
}
