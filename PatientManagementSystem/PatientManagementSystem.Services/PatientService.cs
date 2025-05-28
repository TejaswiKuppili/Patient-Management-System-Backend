using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;

namespace PatientManagementSystem.Services
{
    public class PatientService: IPatientService

    {
        private readonly IPatientRepository repository;

        public PatientService(IPatientRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets all patients with navigation properties.
        /// </summary>
        public async Task<List<PatientDto>> GetAllAsync()
        {
            var patients = await repository.GetAllAsync();

            return patients.Select(p => new PatientDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                ContactNumber = p.ContactNumber,
                Address = p.Address,
                CreatedAt = p.CreatedAt,
                CreatedBy = p.CreatedBy,
                ReasonForVisit = p.ReasonForVisit,
                CreatedByEmployeeName = p.CreatedByEmployee?.FullName ?? "Unknown"
            }).ToList();
        }


        /// <summary>
        /// Gets a specific patient by ID.
        /// </summary>
        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Adds a new patient based on DTO.
        /// </summary>
        public async Task AddAsync(PatientDto dto)
        {
            var newPatient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                ContactNumber = dto.ContactNumber,
                Address = dto.Address,
                CreatedAt = DateTime.Now,
                CreatedBy = dto.CreatedBy,
                ReasonForVisit = dto.ReasonForVisit
            };

            await repository.AddAsync(newPatient);
        }

        /// <summary>
        /// Updates an existing patient.
        /// </summary>
        public async Task<bool> UpdateAsync(int id, PatientDto dto)
        {
            var existingPatient = await repository.GetByIdAsync(id);
            if (existingPatient == null)
            {
                return false;
            }

            existingPatient.FirstName = dto.FirstName;
            existingPatient.LastName = dto.LastName;
            existingPatient.DateOfBirth = dto.DateOfBirth;
            existingPatient.Gender = dto.Gender;
            existingPatient.ContactNumber = dto.ContactNumber;
            existingPatient.Address = dto.Address;
            existingPatient.ReasonForVisit = dto.ReasonForVisit;

            await repository.UpdateAsync(existingPatient);
            return true;
        }
    }
}
