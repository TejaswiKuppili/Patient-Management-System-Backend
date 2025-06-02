using PatientManagementSystem.Common.Constants;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Common.Helpers;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;

namespace PatientManagementSystem.Services
{   /// <summary>
    /// Service for managing patient-related operations.
    /// </summary>
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository repository;

        public PatientService(IPatientRepository repository)
        {
            this.repository = repository;
        }
        /// <summary>
        /// Fetches all patients from the repository and returns them as a list of PatientDto.
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<List<PatientDto>>> GetAllAsync()
        {
            try
            {
                List<Patient> patients = await repository.GetAllAsync();

                List<PatientDto> result = patients.Select(p => new PatientDto
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
                    CreatedByEmployeeName = p.CreatedByEmployee != null ? p.CreatedByEmployee.Username : "Unknown"
                }).ToList();

                return ApiResponseHelper.Success(result, ResponseConstants.PatientsFetchedMessage);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<List<PatientDto>>($"{ResponseConstants.PatientsFetchFailedMessage},{ex.Message}", ResponseConstants.InternalServerError);
            }
        }
        /// <summary>
        /// Retrieves a patient by their ID from the repository.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse<PatientDto?>> GetByIdAsync(int id)
        {
            try
            {
                Patient? patient = await repository.GetByIdAsync(id);
                if (patient == null) return ApiResponseHelper.Fail<PatientDto?>(ResponseConstants.PatientAddFailedMessage, ResponseConstants.InternalServerError);

                PatientDto? dto = new PatientDto
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                    ContactNumber = patient.ContactNumber,
                    Address = patient.Address,
                    CreatedAt = patient.CreatedAt,
                    CreatedBy = patient.CreatedBy,
                    ReasonForVisit = patient.ReasonForVisit,
                    CreatedByEmployeeName = patient.CreatedByEmployee?.Username
                };

                return ApiResponseHelper.Success<PatientDto?>(dto, ResponseConstants.PatientsFetchedMessage);
               
            }
            catch(Exception ex)
            {
                return ApiResponseHelper.Fail<PatientDto?>($"{ResponseConstants.PatientAddFailedMessage} {ex.Message}", ResponseConstants.InternalServerError);
            }
        }

        /// <summary>
        /// Adds a new patient to the repository and returns the created PatientDto.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResponse<PatientDto>> AddAsync(PatientDto dto)
        {
            try
            {
                Patient newPatient = new Patient
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

                Patient addedPatient = await repository.AddAsync(newPatient);

                PatientDto returnDto = new PatientDto
                {
                    Id = addedPatient.Id,
                    FirstName = addedPatient.FirstName,
                    LastName = addedPatient.LastName,
                    DateOfBirth = addedPatient.DateOfBirth,
                    Gender = addedPatient.Gender,
                    ContactNumber = addedPatient.ContactNumber,
                    Address = addedPatient.Address,
                    CreatedAt = addedPatient.CreatedAt,
                    CreatedBy = addedPatient.CreatedBy,
                    ReasonForVisit = addedPatient.ReasonForVisit
                };

                return ApiResponseHelper.Success(returnDto, ResponseConstants.PatientAddedMessage);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<PatientDto>(
                $"{ResponseConstants.PatientAddFailedMessage} {ex.Message}",
                ResponseConstants.InternalServerError
                );

            }
        }
        /// <summary>
        /// Updates an existing patient in the repository based on the provided ID and PatientDto.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(int id, PatientDto dto)
        {
            try
            {
                Patient? existingPatient = await repository.GetByIdAsync(id);
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
            catch
            {
                return false;
            }
        }
    }
}
