using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Services.Interfaces;

namespace PatientManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login( LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string? ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            LoginResponseDto? result = await authService.LoginAsync(model.Email, model.Password, ip);

            if (result == null)
                return Unauthorized(new { message = "Invalid credentials." });

            return Ok(result);
        }
    }
}
