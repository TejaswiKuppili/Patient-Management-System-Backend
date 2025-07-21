using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Repository.Interfaces;

namespace PatientManagementSystem.Repository
{/// <summary>
/// Repository Layer for Doctor Data
/// </summary>
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext context;

        public DoctorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Retrieves all doctors from the ApplicationUsers table those are having RoleId as doctors.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DoctorDto>> GetAllAsync()
        {
            return await context.ApplicationUsers
                .Where(u => u.RoleId == 2) 
                .Include(u => u.Specialty)
                .Select(u => new DoctorDto
                {
                    Id = u.Id,
                    Name = u.Username,
                    EmailId = u.Email,
                    SpecialtyName = u.Specialty != null ? u.Specialty.Name : "Not Assigned"
                })
                .ToListAsync();
        }
        /// <summary>
        /// Retrieves a single doctor by their ID from the ApplicationUsers table if they have the doctor role (RoleId = 2), including specialty.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DoctorDto?> GetByIdAsync(int id)
        {
            var user = await context.ApplicationUsers
                .Where(u => u.Id == id && u.RoleId == 2)
                .Include(u => u.Specialty)
                .FirstOrDefaultAsync();

            if (user == null) return null;

            return new DoctorDto
            {
                Id = user.Id,
                Name = user.Username,
                EmailId = user.Email,
                SpecialtyName = user.Specialty != null ? user.Specialty.Name : "Not Assigned"
            };
        }


        /// <summary>
        /// Checks if a doctor with the given ID exists in the ApplicationUsers table with RoleId as 2 (doctor).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await context.ApplicationUsers
                .AnyAsync(u => u.Id == id && u.RoleId == 2);
        }
    }
}
