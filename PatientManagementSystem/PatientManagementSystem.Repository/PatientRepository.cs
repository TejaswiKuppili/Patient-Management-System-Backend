using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Repository.Interfaces;

namespace PatientManagementSystem.Repository
{
    public class PatientRepository:IPatientRepository

    {
        private readonly ApplicationDbContext context;

        public PatientRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all patients with their associated employee who created the record.
        /// </summary>
        public async Task<List<Patient>> GetAllAsync()
        {
            return await context.Patients
                .Include(p => p.CreatedByEmployee)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a single patient by ID with navigation data.
        /// </summary>
        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await context.Patients
                .Include(p => p.CreatedByEmployee)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Adds a new patient record.
        /// </summary>
        public async Task AddAsync(Patient patient)
        {
            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing patient record.
        /// </summary>
        public async Task UpdateAsync(Patient patient)
        {
            context.Patients.Update(patient);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a patient record by ID.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await context.Patients.FindAsync(id);
            if (patient == null)
            {
                return false;
            }

            context.Patients.Remove(patient);
            await context.SaveChangesAsync();
            return true;
        }
    }
}


