//using Microsoft.AspNetCore.Mvc;
//using PatientManagementSystem.Services.Interfaces;
//using PatientManagementSystem.Common.DTOs;

//namespace PatientManagementSystemAPI.Controllers
//{

//    [Route("api/[controller]")]
//    [ApiController]
//    public class DoctorsController : ControllerBase
//    {
//        private readonly IDoctorService DoctorService;

//        public DoctorsController(IDoctorService doctorService)
//        {
//            DoctorService = doctorService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors()
//        {
//            IEnumerable<DoctorDto> doctors = await DoctorService.GetDoctorsAsync();
//            return Ok(doctors);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<DoctorDto>> GetDoctor(int id)
//        {
//            DoctorDto doctor = await DoctorService.GetDoctorByIdAsync(id);
//            if (doctor == null)
//            {
//                return NotFound();
//            }

//            return Ok(doctor);
//        }

//        [HttpPost]
//        public async Task<ActionResult<DoctorDto>> PostDoctor(DoctorDto doctor)
//        {
//            DoctorDto createdDoctor = await DoctorService.CreateDoctorAsync(doctor);
//            return CreatedAtAction(nameof(GetDoctor), new { id = createdDoctor.Id }, createdDoctor);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutDoctor(int id, DoctorDto doctor)
//        {
//            bool success = await DoctorService.UpdateDoctorAsync(id, doctor);
//            if (!success)
//            {
//                return NotFound();
//            }

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteDoctor(int id)
//        {
//            bool success = await DoctorService.DeleteDoctorAsync(id);
//            if (!success)
//            {
//                return NotFound();
//            }

//            return NoContent();
//        }
//    }
//}
