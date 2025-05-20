namespace PatientManagementSystem.Common.DTOs
{
    /// <summary>
    /// DTO to return user details with their associated role.
    /// </summary>
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
    
}
