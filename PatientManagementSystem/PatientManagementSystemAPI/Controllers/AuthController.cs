using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.Common.Constants;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Common.Helpers;
using PatientManagementSystem.Services.Interfaces;

namespace PatientManagementSystemAPI.Controllers
{
    /// <summary>
    /// Handles user authentication operations by generating tokens for users to access the system.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        /// <summary>
        /// Logins user by validating credentials and generating JWT tokens for access and refresh.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponseHelper.Fail<string>(
                    ResponseConstants.InvalidRequestBody,
                    ResponseConstants.BadRequest));
            }

            string? ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            LoginResponseDto? result = await authService.LoginAsync(model.Email, model.Password, ip);

            if (result == null)
            {
               
                return Unauthorized(ApiResponseHelper.Fail<string>(
                    ResponseConstants.InvalidCredentials,
                    ResponseConstants.UnAuthorized));
            }

            return Ok(ApiResponseHelper.Success(result, message: ResponseConstants.LoginSuccess));
        }
    }
}
