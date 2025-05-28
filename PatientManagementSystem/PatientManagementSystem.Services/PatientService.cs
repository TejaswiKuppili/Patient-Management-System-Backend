using PatientManagementSystem.Common.Constants;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Common.Helpers;
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
        public async Task<ApiResponse<List<PatientDto>>> GetAllAsync()
        {
            var patients = await repository.GetAllAsync();

             var result= patients.Select(p => new PatientDto
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
            return ApiResponseHelper.Success<List<PatientDto>>(result, ResponseConstants.PatientsFetchedMessage);
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
        public async Task<ApiResponse<PatientDto>> AddAsync(PatientDto dto)
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

            var result = await repository.AddAsync(newPatient);

            var returnDto = new PatientDto
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                DateOfBirth = result.DateOfBirth,
                Gender = result.Gender,
                ContactNumber = result.ContactNumber,
                Address = result.Address,
                CreatedAt = result.CreatedAt,
                CreatedBy = result.CreatedBy,
                ReasonForVisit = result.ReasonForVisit
            };

            return ApiResponseHelper.Success<PatientDto>(returnDto, ResponseConstants.PatientAddedMessage);
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
