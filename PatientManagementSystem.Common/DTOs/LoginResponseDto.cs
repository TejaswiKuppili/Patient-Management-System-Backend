namespace PatientManagementSystem.Common.DTOs
{/// <summary>
/// DTO for the response of a login operation, containing access and refresh tokens.
/// </summary>
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}
