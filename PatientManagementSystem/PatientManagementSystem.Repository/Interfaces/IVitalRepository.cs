using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Repository.Interfaces
{
    /// <summary>
    /// Contract for vital signs repository operations.
    /// </summary>
    public interface IVitalRepository
    {
        Task<List<Vital>> GetVitalsByPatientIdAsync(int patientId);
        Task<bool> AddVitalAsync(Vital vital);

    }
}
