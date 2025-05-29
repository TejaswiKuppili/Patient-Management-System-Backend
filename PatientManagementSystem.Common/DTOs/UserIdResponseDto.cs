namespace PatientManagementSystem.Common.DTOs
{
    /// <summary>
    /// DTO to return user details by their Id.
    /// </summary>
    public class UserIdResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }

}
