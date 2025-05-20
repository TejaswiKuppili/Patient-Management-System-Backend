using Moq;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services;
using Xunit;

namespace PatientManagementSystem.Tests
{
    public class UserServiceTests
    {
        /// <summary>
        /// X Unit Testing on user services.
        /// </summary>
        /// <returns>Test case passed or failed</returns>
        [Fact]
        public async Task GetUsersAndRolesAsync_200Ok()
        {
            // Arrange
            Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
            UserAndRoleDto expectedData = new UserAndRoleDto
            {
                Users = new List<UserDto> {
                    new UserDto {  Name = "AP",Email="ap@gmail.com",RoleName="Scientist"}
                },
                Roles = new List<string> { "Admin" }
            };

            mockRepo.Setup(repo => repo.GetUsersAndRolesAsync())
                    .ReturnsAsync(expectedData);

            UserService service = new UserService(mockRepo.Object);

            // Act
            UserAndRoleDto result = await service.GetUsersAndRolesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Users);
            Assert.Equal("AP", result.Users.First().Name);


        }
        //[Fact]
        //public async Task GetUsersAndRolesAsync_500()
        //{
        //    // Arrange
        //    Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
        //    UserAndRoleDto expectedData = new UserAndRoleDto
        //    {
        //        Users = new List<UserDto> {
        //            new UserDto {  }
        //        },
        //        Roles = new List<string> { }
        //    };

        //    mockRepo.Setup(repo => repo.GetUsersAndRolesAsync())
        //            .ReturnsAsync(expectedData);

        //    UserService service = new UserService(mockRepo.Object);

        //    // Act
        //    UserAndRoleDto result = await service.GetUsersAndRolesAsync();

        //    //Assert
        //    Assert.NotNull(result);

        //}
    }
}
