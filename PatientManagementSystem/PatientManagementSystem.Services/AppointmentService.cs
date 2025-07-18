using PatientManagementSystem.Common.Constants;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Common.Helpers;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;

namespace PatientManagementSystem.Common.Services
{
    /// <summary>
    /// Service for managing appointments.
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository AppointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            AppointmentRepository = appointmentRepository;
        }

        public async Task<ApiResponse<IEnumerable<AppointmentDto>>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            try
            {
                IEnumerable<Appointment>? appointments = await AppointmentRepository.GetByDoctorIdAsync(doctorId);

                IEnumerable<AppointmentDto>? result = appointments.Select(appointment => new AppointmentDto
                {
                    Id = appointment.Id,
                    PatientId=appointment.PatientId,
                    PatientName = appointment.Patient?.FirstName + " " + appointment.Patient?.LastName,
                    AppointmentStartTime = appointment.AppointmentStartTime,
                    AppointmentEndTime = appointment.AppointmentEndTime,
                    DoctorName = appointment.Doctor.Username,
                    DoctorId=appointment.Doctor.Id,
                    CreatedBy=appointment.CreatedBy,
                    Reason=appointment.Reason
                });

                return ApiResponseHelper.Success(result, ResponseConstants.AppointmentsFetchedMessage);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<IEnumerable<AppointmentDto>>(
                    $"{ResponseConstants.AppointmentsFetchFailedMessage}: {ex.Message}",
                    ResponseConstants.InternalServerError);
            }
        }

        public async Task<ApiResponse<IEnumerable<AppointmentDto>>> GetAppointmentsWithDoctorNameAsync()
        {
            try
            {
                IEnumerable<Appointment>? appointments = await AppointmentRepository.GetAllWithDoctorAsync();

                IEnumerable<AppointmentDto>? result = appointments.Select(appointment => new AppointmentDto
                {
                    Id = appointment.Id,
                    PatientName = appointment.Patient?.FirstName+ " " + appointment.Patient?.LastName,
                    AppointmentStartTime = appointment.AppointmentStartTime,
                    AppointmentEndTime = appointment.AppointmentEndTime,
                    DoctorName = appointment.Doctor.Username,
                    PatientId = appointment.PatientId,
                    Reason = appointment.Reason,
                    CreatedBy=appointment.CreatedBy,
                    DoctorId=appointment.DoctorId
                });

                return ApiResponseHelper.Success(result, ResponseConstants.AppointmentsFetchedMessage);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<IEnumerable<AppointmentDto>>(
                    $"{ResponseConstants.AppointmentsFetchFailedMessage}: {ex.Message}",
                    ResponseConstants.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> CreateAppointmentAsync(AppointmentDto appointment)
        {
            try
            {
                Appointment? entity = new Appointment
                {
                    PatientId = appointment.PatientId,
                    AppointmentStartTime = appointment.AppointmentStartTime,
                    AppointmentEndTime = appointment.AppointmentEndTime,
                    Reason = appointment.Reason,
                    Status = string.IsNullOrWhiteSpace(appointment.Status) ? "Scheduled" : appointment.Status,
                    CreatedAt = DateTime.Now,
                    CreatedBy = appointment.CreatedBy,
                    DoctorId = appointment.DoctorId
                };

                await AppointmentRepository.AddAsync(entity);

                return ApiResponseHelper.Success(ResponseConstants.AppointmentAddedMessage);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<string>(
                    $"{ResponseConstants.AppointmentAddFailedMessage}: {ex.Message}",
                    ResponseConstants.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> UpdateAppointmentAsync(AppointmentDto appointment)
        {
            try
            {
                Appointment? existing = await AppointmentRepository.GetByIdAsync(appointment.Id);
                if (existing == null)
                {
                    return ApiResponseHelper.Fail<string>("Appointment not found.", ResponseConstants.NotFound);
                }

                existing.AppointmentStartTime = appointment.AppointmentStartTime;
                existing.AppointmentEndTime = appointment.AppointmentEndTime;
                existing.DoctorId = appointment.DoctorId;
                existing.PatientId = appointment.PatientId;
                existing.Reason = appointment.Reason;

                await AppointmentRepository.UpdateAsync(existing);

                return ApiResponseHelper.Success(ResponseConstants.AppointmentUpdatedMessage);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<string>(
                    $"{ResponseConstants.AppointmentUpdateFailedMessage}: {ex.Message}",
                    ResponseConstants.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> DeleteAppointmentAsync(int id)
        {
            try
            {
                Appointment? appointment = await AppointmentRepository.GetByIdAsync(id);
                if (appointment == null)
                {
                    return ApiResponseHelper.Fail<string>("Appointment not found.", ResponseConstants.NotFound);
                }

                await AppointmentRepository.DeleteAsync(appointment);
                return ApiResponseHelper.Success(ResponseConstants.AppointmentDeletedMessage);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<string>(
                    $"{ResponseConstants.AppointmentDeleteFailedMessage}: {ex.Message}",
                    ResponseConstants.InternalServerError);
            }
        }
    }
}
