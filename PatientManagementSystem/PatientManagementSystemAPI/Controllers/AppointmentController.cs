using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService AppointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        AppointmentService = appointmentService;
    }

    [HttpGet("doctor/{doctorId}")]
    public async Task<IActionResult> GetAppointmentsByDoctorId(int doctorId)
    {
        var response = await AppointmentService.GetAppointmentsByDoctorIdAsync(doctorId);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("with-doctor")]
    public async Task<IActionResult> GetAppointmentsWithDoctorName()
    {
        var response = await AppointmentService.GetAppointmentsWithDoctorNameAsync();
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointment)
    {
        var response = await AppointmentService.CreateAppointmentAsync(appointment);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDto updatedAppointment)
    {
        if (id != updatedAppointment.Id)
        {
            return BadRequest("Appointment ID mismatch.");
        }

        var response = await AppointmentService.UpdateAppointmentAsync(updatedAppointment);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var response = await AppointmentService.DeleteAppointmentAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}
