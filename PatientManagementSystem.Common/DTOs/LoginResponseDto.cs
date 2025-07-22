using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace PatientManagementSystem.Common.DTOs
{/// <summary>
/// DTO for the response of a login operation, containing access and refresh tokens.
/// </summary>
    public class LoginResponseDto
    {
        //[Required]
        //[NotNull]
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserIdResponseDto UserDetails { get; set; }
    }
}
