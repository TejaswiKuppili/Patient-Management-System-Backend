using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;

namespace PatientManagementSystem.Repository
{
    /// <summary>
    /// Repository for managing vital signs data operations.
    /// </summary>
    public class VitalRepository : IVitalRepository
    {
        private readonly ApplicationDbContext context;

        public VitalRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Retrieves a list of vital signs for a specific patient, ordered by the date they were recorded.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<List<Vital>> GetVitalsByPatientIdAsync(int patientId)
        {
            return await context.Vitals
                .Where(v => v.PatientId == patientId)
                .OrderByDescending(v => v.RecordedAt)
                .ToListAsync();
        }
    }
}
