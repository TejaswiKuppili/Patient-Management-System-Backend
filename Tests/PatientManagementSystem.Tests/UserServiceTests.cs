
using Moq;
using PatientManagementSystem.Common.Constants;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services;
using PatientManagementSystem.Services.Interfaces;
using Xunit;

namespace PatientManagementSystem.Tests.Services
{
    /// <summary>
    /// XUnit testing of user service
    /// </summary>
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly IUserService userService;

        public UserServiceTests()
        {
            mockUserRepository = new Mock<IUserRepository>();
            userService = new UserService(mockUserRepository.Object);
        }
        /// <summary>
        /// Tests that GetUsersAndRolesAsync returns a 200 OK response with data when users and roles are found.
        /// </summary>
        [Fact]
        public async Task GetUsersAndRolesAsync_Returns200()
        {
            // Arrange
            List<UserDto> users = new List<UserDto>
            {
                new UserDto { Name = "Test", Email = "test@example.com", RoleName = "Admin" }
            };
            List<string> roles = new List<string> { "Admin" };

            UserAndRoleDto dto = new UserAndRoleDto
            {
                Users = users,
                Roles = roles
            };

            mockUserRepository.Setup(repo => repo.GetUsersAndRolesAsync()).ReturnsAsync(dto);

            // Act
            ApiResponse<UserAndRoleDto> response = await userService.GetUsersAndRolesAsync();

            // Assert
            Assert.True(response.Success);
            Assert.Equal(200, response.StatusCode);
            Assert.NotNull(response.Data);
          
        }
        /// <summary>
        /// Tests that GetUsersAndRolesAsync returns a 404 Not Found response when no users are found.
        /// </summary>
        [Fact]
        public async Task GetUsersAndRolesAsync_Returns404()
        {
            // Arrange
            UserAndRoleDto dto = new UserAndRoleDto
            {
                Users = new List<UserDto>(), // empty users
                Roles = new List<string>()
            };

            mockUserRepository.Setup(repo => repo.GetUsersAndRolesAsync()).ReturnsAsync(dto);

            // Act
            ApiResponse<UserAndRoleDto> response = await userService.GetUsersAndRolesAsync();

            // Assert
            Assert.False(response.Success);
            Assert.Equal(404, response.StatusCode);
            Assert.Null(response.Data);
            Assert.Equal("No users found.", response.Message);
        }

        /// <summary>
        /// Tests that CreateUserAsync creates a user successfully and returns a 200 OK response.
        /// </summary>
        [Fact]
        public async Task CreateUserAsync_CreatesUserSuccessfully()
        {
            // Arrange
             UserDto? newUser = new UserDto
            {
                Name = "John Doe",
                Email = "john@example.com",
                Password = "password123",
                RoleName = "Admin"
            };

            UserDto? createdUser = new UserDto
            {
                Id = 1,
                Name = newUser.Name,
                Email = newUser.Email,
                RoleName = newUser.RoleName
            };

            mockUserRepository.Setup(repo => repo.CreateUserAsync(newUser)).ReturnsAsync(createdUser);

            // Act
            ApiResponse<string> response = await userService.CreateUserAsync(newUser);

            // Assert
            Assert.True(response.Success);
            Assert.Equal(200, response.StatusCode);
            Assert.NotNull(response.Data);
        }

        /// <summary>
        /// Tests that CreateUserAsync throws an exception when the role is not found.
        /// </summary>
        [Fact]
        public async Task CreateUserAsync_ReturnsFailureResponse_WhenRoleNotFound()
        {
            // Arrange
             UserDto invalidUser = new UserDto
            {
                Name = "Invalid User",
                Email = "invalid@example.com",
                Password = "password123",
                RoleName = "NonExistentRole"
            };

            mockUserRepository
                .Setup(repo => repo.CreateUserAsync(invalidUser))
                .ThrowsAsync(new Exception("Role not found."));

            // Act
            ApiResponse<string>? result = await userService.CreateUserAsync(invalidUser);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Role not found", result.Message);
            Assert.Equal(ResponseConstants.InternalServerError, result.StatusCode);
        }


        /// <summary>
        /// Tests that UpdateUserRoleAsync updates the user's role successfully.
        /// </summary>
        [Fact]
        public async Task UpdateUserRoleAsync_UpdatesRoleSuccessfully()
        {
            // Arrange
            int userId = 1;
            string newRoleName = "Doctor";

            mockUserRepository
                .Setup(repo => repo.UpdateUserRoleAsync(userId, newRoleName))
                .Returns(Task.CompletedTask);

            // Act
            await userService.UpdateUserRoleAsync(userId, newRoleName);

            // Assert – If no exception is thrown, test is successful
            mockUserRepository.Verify(repo => repo.UpdateUserRoleAsync(userId, newRoleName), Times.Once);
        }

        /// <summary>
        /// Tests that UpdateUserRoleAsync throws an exception when the user or role is not found.
        /// </summary>
        [Fact]
        public async Task UpdateUserRoleAsync_ThrowsException_WhenUserOrRoleNotFound()
        {
            // Arrange
            int userId = 99;
            string invalidRole = "InvalidRole";

            mockUserRepository
                .Setup(repo => repo.UpdateUserRoleAsync(userId, invalidRole))
                .ThrowsAsync(new Exception("Role not found."));

            // Act
            ApiResponse<string> result = await userService.UpdateUserRoleAsync(userId, invalidRole);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Role not found", result.Message);
            Assert.Equal(ResponseConstants.InternalServerError, result.StatusCode);
        }
    }
}
