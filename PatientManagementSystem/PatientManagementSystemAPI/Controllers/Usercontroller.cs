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

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDto userDetails)
        {
            if (string.IsNullOrEmpty(userDetails.Name) || string.IsNullOrEmpty(userDetails.Email) || string.IsNullOrEmpty(userDetails.RoleName))
            {
                return BadRequest("Missing user details.");
            }

            await userService.CreateUser(userDetails);
            return Ok(new { Message = "User created successfully." });
        }

        /// <summary>
        /// Updates the role of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("with-roles/{userId}/role")]
        public async Task<IActionResult> UpdateUserRoleAsync(int userId, [FromBody] UpdateUserRoleDto request)
        {
            if (request == null || string.IsNullOrEmpty(request.Role))
            {
                return BadRequest("Invalid request body."); 
            }

            try
            {
                await userService.UpdateUserRole(userId, request.Role);
                return Ok(new { Message = "User role updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "An unexpected error occurred while updating role of the user");
            }
        }
    }
}