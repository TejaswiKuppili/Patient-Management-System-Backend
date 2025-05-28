using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Services.Interfaces;

namespace PatientManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;

        public PatientController(IPatientService patientService)
        {
            this.patientService = patientService;
        }


        [HttpGet("getAllPatients")]
        public async Task<IActionResult> GetAll()
        {
            var patients = await patientService.GetAllAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await patientService.GetByIdAsync(id);
            if (patient == null)
                return NotFound("Patient not found");

            return Ok(patient);
        }

        [HttpPost("AddPatient")]
        public async Task<IActionResult> Add( PatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await patientService.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await patientService.UpdateAsync(id, dto);
            if (!result)
                return NotFound("Patient not found");

            return Ok("Patient updated successfully");
        }
    }
}
