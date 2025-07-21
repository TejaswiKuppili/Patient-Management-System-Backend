using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Services.Interfaces;
using PatientManagementSystem.Repository.Interfaces;

namespace PatientManagementSystem.Services
{
    /// <summary>
    /// Service class for managing doctor-related operations.
    /// </summary>
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository doctorRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorService"/> class.
        /// </summary>
        /// <param name="doctorRepository">The doctor repository.</param>
        public DoctorService(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        /// <summary>
        /// Retrieves all doctors asynchronously.
        /// </summary>
        /// <returns>A collection of <see cref="DoctorDto"/> objects.</returns>
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

        /// <summary>
        /// Retrieves a doctor by their unique ID.
        /// </summary>
        /// <param name="id">The doctor's ID.</param>
        /// <returns>A <see cref="DoctorDto"/> if found; otherwise, null.</returns>
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

        /// <summary>
        /// Checks if a doctor exists based on the provided ID.
        /// </summary>
        /// <param name="id">The doctor's ID.</param>
        /// <returns>True if the doctor exists; otherwise, false.</returns>
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
