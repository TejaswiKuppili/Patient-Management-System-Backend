using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Services.Interfaces;
namespace PatientManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Retrieves all users with their roles.
        /// </summary>
        [HttpGet("roles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {

            ApiResponse<UserAndRoleDto> response = await userService.GetUsersAndRolesAsync();

                if (!response.Success)
                    return BadRequest(response);

                return Ok(response);
            

        }
    }

}
