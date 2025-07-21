using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Services.Interfaces;

namespace PatientManagementSystem.Controllers
{
    /// <summary>
    /// Controller to manage appointment-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService AppointmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentController"/> class.
        /// </summary>
        /// <param name="appointmentService">Service for managing appointments.</param>
        public AppointmentController(IAppointmentService appointmentService)
        {
            AppointmentService = appointmentService;
        }

        /// <summary>
        /// Gets appointments by doctor ID.
        /// </summary>
        /// <param name="doctorId">The doctor's ID.</param>
        /// <returns>List of appointments for the specified doctor.</returns>
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsByDoctorId(int doctorId)
        {
            ApiResponse<IEnumerable<AppointmentDto>>? response = await AppointmentService.GetAppointmentsByDoctorIdAsync(doctorId);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Gets all appointments along with doctor names.
        /// </summary>
        /// <returns>List of appointments including doctor names.</returns>
        [HttpGet("with-doctor")]
        public async Task<IActionResult> GetAppointmentsWithDoctorName()
        {
            ApiResponse<IEnumerable<AppointmentDto>>? response = await AppointmentService.GetAppointmentsWithDoctorNameAsync();
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="appointment">Appointment details.</param>
        /// <returns>Status and message of the operation.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointment)
        {
            ApiResponse<string>? response = await AppointmentService.CreateAppointmentAsync(appointment);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="id">Appointment ID to update.</param>
        /// <param name="updatedAppointment">Updated appointment details.</param>
        /// <returns>Status and message of the operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDto updatedAppointment)
        {
            if (id != updatedAppointment.Id)
            {
                return BadRequest("Appointment ID mismatch.");
            }

            ApiResponse<string>? response = await AppointmentService.UpdateAppointmentAsync(updatedAppointment);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Deletes an appointment by ID.
        /// </summary>
        /// <param name="id">Appointment ID to delete.</param>
        /// <returns>Status and message of the operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            ApiResponse<string>? response = await AppointmentService.DeleteAppointmentAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
