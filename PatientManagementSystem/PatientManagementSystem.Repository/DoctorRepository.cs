//using Microsoft.EntityFrameworkCore;
//using PatientManagementSystem.Common.DTOs;
//namespace PatientManagementSystem.Repository
//{
//    public class DoctorRepository : IDoctorRepository
//    {
//        private readonly ApplicationDbContext ApplicationDbContext;

//        public DoctorRepository(ApplicationDbContext ApplicationDbContext)
//        {
//            ApplicationDbContext = ApplicationDbContext;
//        }

//        public async Task<IEnumerable<DoctorDto>> GetAllAsync()
//        {
//            return await ApplicationDbContext.Doctors.ToListAsync();
//        }

//        public async Task<DoctorDto> GetByIdAsync(int id)
//        {
//            return await ApplicationDbContext.Doctors.FindAsync(id);
//        }

//        public async Task AddAsync(DoctorDto doctor)
//        {
//            await ApplicationDbContext.Doctors.AddAsync(doctor);
//            await ApplicationDbContext.SaveChangesAsync();
//        }

//        public async Task UpdateAsync(DoctorDto doctor)
//        {
//            ApplicationDbContext.Entry(doctor).State = EntityState.Modified;
//            await ApplicationDbContext.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(DoctorDto doctor)
//        {
//            ApplicationDbContext.Doctors.Remove(doctor);
//            await ApplicationDbContext.SaveChangesAsync();
//        }

//        public async Task<bool> ExistsAsync(int id)
//        {
//            return await ApplicationDbContext.Doctors.AnyAsync(d => d.Id == id);
//        }
//    }
//}
