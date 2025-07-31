using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Data.DataContext;
using Microsoft.EntityFrameworkCore;
namespace PatientManagementSystem.Repository
{
    /// <summary>
    /// Repository Layer for Appointment Data
    /// </summary>
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// handles getting the data of appointments by doctor id from database
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId)
        {
            return await context.Appointments
                .Include(a => a.Doctor)     // Include Doctor data
                .Include(a => a.Patient)    // Include Patient data
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }
        /// <summary>
        /// Retrieves all appointments along with the associated doctor information from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Appointment>> GetAllWithDoctorAsync()
        {
            return await context.Appointments
                .Include(appointment => appointment.Doctor)
                .ToListAsync();
        }
        /// <summary>
        /// Retrieves an appointment by its ID from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await context.Appointments.FindAsync(id);
        }
        /// <summary>
        /// Adding Appointment in database
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public async Task AddAsync(Appointment appointment)
        {
            appointment.Reason= await context.Patients
            .Where(a => a.Id == appointment.PatientId)
            .Select(a => a.ReasonForVisit)
            .FirstOrDefaultAsync();

            context.Appointments.Add(appointment);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            context.Appointments.Update(appointment);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Deleting appointment record in database
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Appointment appointment)
        {
            context.Appointments.Remove(appointment);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves appointments for a specific patient by ID.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return await context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        }

    }
}
