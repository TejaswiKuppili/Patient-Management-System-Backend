using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Services.Interfaces;
using PatientManagementSystemAPI.Common.DTOs;

namespace PatientManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VitalController : ControllerBase
    {
        private readonly IVitalService vitalService;

        public VitalController(IVitalService vitalService)
        {
            this.vitalService = vitalService;
        }

        /// <summary>
        /// Get all vitals for a patient by their ID.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <returns>List of vitals for the patient.</returns>
        [HttpGet("getVitals/{patientId}")]
        public async Task<IActionResult> GetVitals(int patientId)
        {
            ApiResponse<List<VitalDto>> response = await vitalService.GetVitalsAsync(patientId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
