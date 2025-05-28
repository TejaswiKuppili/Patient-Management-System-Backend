using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services.Interfaces;
using PatientManagementSystem.Common.Constants;
using PatientManagementSystem.Common.Helpers;
using Microsoft.Extensions.Logging;
using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Services
{
    /// <summary>
    /// Provides user-related business operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserService> logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves list of users and roles.
        /// </summary>
        /// <returns>ApiResponse containing users and roles, or error message on failure</returns>
        public async Task<ApiResponse<UserAndRoleDto>> GetUsersAndRolesAsync()
        {
            try
            {
                UserAndRoleDto? result = await userRepository.GetUsersAndRolesAsync();

                if (result == null || result.Users == null || !result.Users.Any())
                {
                    return ApiResponseHelper.Fail<UserAndRoleDto>(
                        ResponseConstants.NoUsersFoundMessage,
                        ResponseConstants.NotFound
                    );
                }

                return ApiResponseHelper.Success(result, message: ResponseConstants.UserFetchedMessage);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<UserAndRoleDto>(
                    $"{ResponseConstants.GenericErrorMessage}{ex.Message}",
                    ResponseConstants.InternalServerError
                );
            }
        }
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDetails"></param>
        public async Task<ApiResponse<string>> CreateUserAsync(UserDto userDetails)
        {
            try
            {
                userDetails.Password = PasswordHasher.HashPassword(userDetails.Password);


                await userRepository.CreateUserAsync(userDetails);
                return ApiResponseHelper.Success(ResponseConstants.CreatedUserMessage);
            }

            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<string>(
                    $"{ResponseConstants.GenericErrorMessage}{ex.Message}",
                    ResponseConstants.InternalServerError
                );
            }
        }
        /// <summary>
        /// Updates the role of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newRoleName"></param>
        public async Task<ApiResponse<string>> UpdateUserRoleAsync(int userId, string newRoleName)
        {
            try
            {
                await userRepository.UpdateUserRoleAsync(userId, newRoleName);
                return ApiResponseHelper.Success(ResponseConstants.RoleUpdatedMessage);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Fail<string>(
                    $"{ResponseConstants.GenericErrorMessage}{ex.Message}",
                    ResponseConstants.InternalServerError
                );
            }
        }



        /// <summary>
        /// Validates user credentials against stored hash.
        /// </summary>
        /// <param name="email">Email input</param>
        /// <param name="password">Plain text password input</param>
        /// <returns>ApplicationUser object if valid; otherwise null</returns>
        public async Task<UserDto?> ValidateUserAsync(string email, string password)
        {
            try { 
            // Await the result of the asynchronous call to get the user
            UserDto? user = await userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                logger.LogWarning("Login failed: email not found for {email}", email);
                return null;
            }

            // Verify password
            bool isValid = PasswordHasher.VerifyHashedPassword(user.Password, password);
            if (!isValid)
            {
                logger.LogWarning("Login failed: invalid password for {email}", email);
                return null;
            }

            return user;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error validating user credentials for {email}", email);
                return null;
            }

        }

    }
}
