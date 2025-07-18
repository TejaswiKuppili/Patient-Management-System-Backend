using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;
namespace PatientManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        /// <summary>
        /// Get all doctors.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors()
        {
            var doctors = await doctorService.GetDoctorsAsync();
            return Ok(doctors);
        }

        /// <summary>
        /// Get doctor by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctorById(int id)
        {
            var doctor = await doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound($"Doctor with ID {id} not found.");
            }
            return Ok(doctor);
        }

        /// <summary>
        /// Check if doctor exists by ID.
        /// </summary>
        [HttpGet("{id}/exists")]
        public async Task<ActionResult<bool>> DoctorExists(int id)
        {
            bool exists = await doctorService.DoctorExistsAsync(id);
            return Ok(exists);
        }
    }
}
