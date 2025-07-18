using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Services.Interfaces;
using PatientManagementSystem.Repository.Interfaces;

namespace PatientManagementSystem.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctorsAsync()
        {
            try
            {
                return await doctorRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetDoctorsAsync: {ex.Message}");
                return new List<DoctorDto>();
            }
        }

        public async Task<DoctorDto?> GetDoctorByIdAsync(int id)
        {
            try
            {
                return await doctorRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetDoctorByIdAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DoctorExistsAsync(int id)
        {
            try
            {
                return await doctorRepository.ExistsAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DoctorExistsAsync: {ex.Message}");
                return false;
            }
        }
    }
}
