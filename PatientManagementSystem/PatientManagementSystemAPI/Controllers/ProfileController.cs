using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Services.Interfaces;

namespace PatientManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        /// <summary>
        /// Get a user's profile by their userId
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            var response = await profileService.GetProfileByUserIdAsync(userId);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Create a new profile
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] ProfileDto profileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid profile data.");

            var response = await profileService.CreateProfileAsync(profileDto);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Update an existing profile
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileDto profileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid profile data.");

            var response = await profileService.UpdateProfileAsync(profileDto);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Delete a profile by userId
        /// </summary>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteProfile(int userId)
        {
            var response = await profileService.DeleteProfileAsync(userId);
            return StatusCode(response.StatusCode, response);
        }
    }
}