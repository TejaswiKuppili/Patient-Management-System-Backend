using PatientManagementSystem.Common.DTOs;

namespace PatientManagementSystem.Repository.Interfaces
{
    public interface IDoctorRepository
    {
        /// <summary>
        /// Gets all users who have a doctor role.
        /// </summary>
        Task<IEnumerable<DoctorDto>> GetAllAsync();

        /// <summary>
        /// Gets a single doctor by user ID if they have the doctor role.
        /// </summary>
        Task<DoctorDto?> GetByIdAsync(int id);

        /// <summary>
        /// Checks if a doctor with the given user ID exists.
        /// </summary>
        Task<bool> ExistsAsync(int id);
    }
}
