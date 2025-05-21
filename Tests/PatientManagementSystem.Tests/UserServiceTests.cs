using Moq;
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

        
    }
}
