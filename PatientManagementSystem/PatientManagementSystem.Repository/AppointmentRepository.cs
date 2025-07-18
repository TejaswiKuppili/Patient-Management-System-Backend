using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Common.DTOs;
namespace PatientManagementSystem.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext Context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId)
        {
            return await Context.Appointments
                .Where(appointment => appointment.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllWithDoctorAsync()
        {
            return await Context.Appointments
                .Include(appointment => appointment.Doctor)
                .Include(appointment => appointment.Patient)
                //.Where(appointment => appointment.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await Context.Appointments.FindAsync(id);
        }

        public async Task AddAsync(Appointment appointment)
        {
            Context.Appointments.Add(appointment);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            Context.Appointments.Update(appointment);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Appointment appointment)
        {
            Context.Appointments.Remove(appointment);
            await Context.SaveChangesAsync();
        }
    }
}
