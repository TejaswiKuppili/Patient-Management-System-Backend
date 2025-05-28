using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Repository.Interfaces
{
    public interface IVitalRepository
    {
        Task<List<Vital>> GetVitalsByPatientIdAsync(int patientId);
    }
}
