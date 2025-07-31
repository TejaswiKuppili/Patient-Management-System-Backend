using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Repository.Interfaces
{
    /// <summary>
    /// Repository interface for managing appointments.
    /// </summary>
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId);
        Task<IEnumerable<Appointment>> GetAllWithDoctorAsync();
        Task<Appointment> GetByIdAsync(int id);
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(Appointment appointment);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);

    }
}
