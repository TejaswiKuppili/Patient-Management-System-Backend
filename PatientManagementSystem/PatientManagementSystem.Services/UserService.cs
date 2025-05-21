using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;
using PatientManagementSystem.Common.Constants;
using PatientManagementSystem.Common.Helpers;

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

        /// <summary>
        /// Retrieves list of users and roles.
        /// </summary>
        /// <returns>ApiResponse containing users and roles, or error message on failure</returns>
        public async Task<ApiResponse<UserAndRoleDto>> GetUsersAndRolesAsync()
        {
            try
            {
                var result = await userRepository.GetUsersAndRolesAsync();

                if (result == null || result.Users == null || !result.Users.Any())
                {
                    return ApiResponseHelper.Fail<UserAndRoleDto>(
                        ResponseConstants.NoUsersFoundMessage,
                        ResponseConstants.NotFound
                    );
                }

                return ApiResponseHelper.Success(result);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<UserAndRoleDto>(
                    $"{ResponseConstants.GenericErrorMessage}{ex.Message}",
                    ResponseConstants.InternalServerError
                );
            }
        }

        public async Task<ApiResponse<string>> CreateUserAsync(UserDto userDetails)
        {
            try
            {
                await userRepository.CreateUserAsync(userDetails);
                return ApiResponseHelper.Success("User created successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<string>(
                    $"{ResponseConstants.GenericErrorMessage}{ex.Message}",
                    ResponseConstants.InternalServerError
                );
            }
        }

        public async Task<ApiResponse<string>> UpdateUserRoleAsync(int userId, string newRoleName)
        {
            try
            {
                await userRepository.UpdateUserRoleAsync(userId, newRoleName);
                return ApiResponseHelper.Success("User role updated successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<string>(
                    $"{ResponseConstants.GenericErrorMessage}{ex.Message}",
                    ResponseConstants.InternalServerError
                );
            }
        }
    }
}
