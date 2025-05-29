namespace PatientManagementSystem.Common.DTOs
{
    /// <summary>
    /// Dto to return User and Role details
    /// </summary>
    public class UserAndRoleDto
        {
            public IEnumerable<UserDto> Users { get; set; }
            public IEnumerable<string> Roles { get; set; }
        }
    }


