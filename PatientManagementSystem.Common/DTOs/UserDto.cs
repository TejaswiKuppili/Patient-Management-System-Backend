namespace PatientManagementSystem.Common.DTOs
{
    /// <summary>
    /// DTO to return user details with their associated role.
    /// </summary>
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
    /// <summary>
    /// DTO to update role of the user
    /// </summary>
    public class UpdateUserRoleDto
    {
        public string Role { get; set; }
    }
}
