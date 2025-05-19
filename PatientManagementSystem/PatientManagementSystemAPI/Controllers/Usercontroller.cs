using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("with-roles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            try
            {
                var users = await userService.GetUsersWithRolesAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Optionally log ex here using a logger
                return StatusCode(500, "An unexpected error occurred while fetching users.");
            }
        }
    }

}
