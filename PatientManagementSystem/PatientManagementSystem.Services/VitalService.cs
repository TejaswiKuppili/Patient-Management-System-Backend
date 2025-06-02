using PatientManagementSystem.Common.Constants;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Common.Helpers;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;
using PatientManagementSystemAPI.Common.DTOs;

namespace PatientManagementSystem.Services
{   /// <summary>
    /// Service for managing patient vitals.
    /// </summary>
    public class VitalService : IVitalService
    {
        private readonly IVitalRepository vitalRepository;

        public VitalService(IVitalRepository vitalRepository)
        {
            this.vitalRepository = vitalRepository;
        }
        /// <summary>
        /// Fetches the vitals for a specific patient by their ID.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<ApiResponse<List<VitalDto>>> GetVitalsAsync(int patientId)
        {
            try
            {
                List<Vital> vitals = await vitalRepository.GetVitalsByPatientIdAsync(patientId);

                List<VitalDto> data = vitals.Select(v => new VitalDto
                {
                    Id = v.Id,
                    PatientId = v.PatientId,
                    RecordedAt = v.RecordedAt,
                    BloodPressure = v.BloodPressure,
                    HeartRate = v.HeartRate,
                    Temperature = v.Temperature,
                    RespiratoryRate = v.RespiratoryRate,
                    CreatedBy = v.CreatedBy
                }).ToList();

                if (data == null || data.Count == 0)
                {
                    return ApiResponseHelper.Fail<List<VitalDto>>(ResponseConstants.NoUsersFoundMessage, ResponseConstants.NotFound);
                }

                return ApiResponseHelper.Success<List<VitalDto>>(data, ResponseConstants.VitalFetchedSuccessMessage);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<List<VitalDto>>(ResponseConstants.GenericErrorMessage + ex.Message, ResponseConstants.InternalServerError);
            }
        }

        /// <summary>
        /// Adds new vitals for a patient.
        /// </summary>
        /// <param name="vitalDto">Vital data to add.</param>
        /// <returns>Success or failure response.</returns>
        public async Task<ApiResponse<bool>> AddVitalsAsync(VitalDto vitalDto)
        {
            try
            {
                Vital vital = new Vital
                {
                    PatientId = vitalDto.PatientId,
                    RecordedAt = vitalDto.RecordedAt,
                    BloodPressure = vitalDto.BloodPressure,
                    HeartRate = vitalDto.HeartRate,
                    Temperature = vitalDto.Temperature,
                    RespiratoryRate = vitalDto.RespiratoryRate,
                    CreatedBy = vitalDto.CreatedBy
                };

                bool result = await vitalRepository.AddVitalAsync(vital);

                if (!result)
                {
                    return ApiResponseHelper.Fail<bool>(ResponseConstants.VitalFailedMessage, ResponseConstants.BadRequest);
                }

                return ApiResponseHelper.Success(true, ResponseConstants.VitalSuccessMessage);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<bool>(ResponseConstants.GenericErrorMessage + ex.Message, ResponseConstants.InternalServerError);
            }
        }

    }
}
