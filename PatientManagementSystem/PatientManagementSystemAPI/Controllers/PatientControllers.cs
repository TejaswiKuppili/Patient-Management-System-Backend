using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Services.Interfaces;

namespace PatientManagementSystemAPI.Controllers
{
    /// <summary>
    /// Controller for managing patient-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;

        public PatientController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        /// <summary>
        ///     Retrieves all patients from the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllPatients")]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<List<PatientDto>>? patients = await patientService.GetAllAsync();
            return Ok(patients);
        }

        /// <summary>
        /// Retrieves a patient by their ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ApiResponse<PatientDto?>? patient = await patientService.GetByIdAsync(id);
            if (patient == null)
                return NotFound("Patient not found");

            return Ok(patient);
        }

        /// <summary>
        ///     Adds a new patient to the system.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("AddPatient")]
        public async Task<IActionResult> Add( PatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApiResponse<PatientDto>? result = await patientService.AddAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing patient in the system.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool result = await patientService.UpdateAsync(id, dto);
            if (!result)
                return NotFound("Patient not found");

            return Ok("Patient updated successfully");
        }
    }
}
