using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository;
using Xunit;

namespace PatientManagementSystem.Tests.Repository
{
    public class UserRepositoryTests
    {
        private ApplicationDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            // Seed roles
            var adminRole = new Role { Name = "Admin" };
            var doctorRole = new Role { Name = "Doctor" };

            context.Roles.AddRange(adminRole, doctorRole);
            context.SaveChanges();

            // Seed users with RoleId
            var user1 = new ApplicationUser
            {
                Username = "Adithya",
                Email = "adithya@gmail.com",
                RoleId = doctorRole.Id
            };

            var user2 = new ApplicationUser
            {
                Username = "Teju",
                Email = "Teju@gmail.com",
                RoleId = adminRole.Id
            };

            context.ApplicationUsers.AddRange(user1, user2);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetUsersAndRolesAsync_ShouldReturnCorrectData()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repository = new UserRepository(context);

            // Act
            var result = await repository.GetUsersAndRolesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Users.Count());
            Assert.Equal(2, result.Roles.Count());

            var userAdithya = result.Users.FirstOrDefault(u => u.Name == "Adithya");
            var userTeju = result.Users.FirstOrDefault(u => u.Name == "Teju");

            Assert.NotNull(userAdithya);
            Assert.Equal("adithya@gmail.com", userAdithya?.Email);
            Assert.Equal("Doctor", userAdithya?.RoleName);

            Assert.NotNull(userTeju);
            Assert.Equal("Teju@gmail.com", userTeju?.Email);
            Assert.Equal("Admin", userTeju?.RoleName);

            Assert.Contains("Admin", result.Roles);
            Assert.Contains("Doctor", result.Roles);
        }

        [Fact]
        public async Task GetUsersAndRolesAsync_ShouldThrowException_WhenContextDisposed()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("DisposedContextDb")
                .Options;

            var context = new ApplicationDbContext(options);
            context.Dispose(); // Simulate disposed context

            var repository = new UserRepository(context);

            // Act & Assert
            await Assert.ThrowsAsync<ObjectDisposedException>(async () =>
            {
                await repository.GetUsersAndRolesAsync();
            });
        }
    }
}
