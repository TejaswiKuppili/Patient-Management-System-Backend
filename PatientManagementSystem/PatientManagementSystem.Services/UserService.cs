using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Data.DataContext;
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

        public async Task<UserAndRoleDto> GetUsersAndRolesAsync()
        {
            return await userRepository.GetUsersAndRolesAsync();
        }

        public Task UpdateUserRoleAsync(int userId, string newRoleName)
        {
            return userRepository.UpdateUserRoleAsync(userId, newRoleName);
        }
    }
}
