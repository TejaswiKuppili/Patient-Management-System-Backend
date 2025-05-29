using System.ComponentModel.DataAnnotations;

namespace PatientManagementSystem.Common.DTOs
{
    /// <summary>
    /// Data Transfer Object for user login information.
    /// </summary>
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

}
