using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;

using PatientManagementSystem.Common.DTOs;
namespace PatientManagementSystem.Services
{
    /// <summary>
    /// Provides user-related business operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetUsersWithRolesAsync()
        {
            try
            {
                var users = await userRepository.GetAllUsersWithRolesAsync();

                return users.Select(user => new UserDto
                {
                    
                    Name = user.Username,
                    Email = user.Email,
                    RoleName = user.Role?.Name
                });
            }
            catch (Exception ex)
            {
                // Log error
                throw new Exception("An error occurred while mapping users to DTOs.", ex);
            }
        }
    }

}
