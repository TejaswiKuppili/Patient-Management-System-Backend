using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Repository.Interfaces;

namespace PatientManagementSystem.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext context;

        public DoctorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

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



        public async Task<bool> ExistsAsync(int id)
        {
            return await context.ApplicationUsers
                .AnyAsync(u => u.Id == id && u.RoleId == 2);
        }
    }
}
