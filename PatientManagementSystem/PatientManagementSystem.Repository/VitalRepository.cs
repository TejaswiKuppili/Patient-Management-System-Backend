using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;

namespace PatientManagementSystem.Repository
{
    public class VitalRepository : IVitalRepository
    {
        private readonly ApplicationDbContext context;

        public VitalRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Vital>> GetVitalsByPatientIdAsync(int patientId)
        {
            return await context.Vitals
                .Where(v => v.PatientId == patientId)
                .OrderByDescending(v => v.RecordedAt)
                .ToListAsync();
        }
    }
}
