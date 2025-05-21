
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Repository
{
    /// <summary>
    /// Repository for accessing user and role data from the database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Fetches all the users from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserAndRoleDto> GetUsersAndRolesAsync()
        {
            try
            {
                var users = await context.ApplicationUsers
                    .Include(user => user.Role)
                    .ToListAsync();

                var userDtos = users.Select(user => new UserDto
                {
                    Id = user.Id,
                    Name = user.Username,
                    Email = user.Email,
                    RoleName = user.Role?.Name
                }).ToList();

                var allRoles = await context.Roles
                    .Select(r => r.Name)
                    .ToListAsync();

                return new UserAndRoleDto
                {
                    Users = userDtos,
                    Roles = allRoles
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users and roles.", ex);
            }
        }

        public async Task<UserDto> CreateUserAsync(UserDto newUser)
        {
            var role = await context.Roles
        .FirstOrDefaultAsync(r => r.Name == newUser.RoleName);

            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            var user = new ApplicationUser
            {
                Username = newUser.Name,
                Email = newUser.Email,
                PasswordHash = newUser.Password,
                RoleId = role.RoleId
            };

            context.ApplicationUsers.Add(user);
            await context.SaveChangesAsync();
            // Return the created user as a DTO
            return new UserDto
            {
                Id = user.Id,
                Name = user.Username,
                Email = user.Email,
                RoleName = role.Name
            };
        }

        /// <summary>
        /// Updates the role of the user in the database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newRoleName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateUserRoleAsync(int userId, string newRoleName)
        {
            var user = await context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var newRole = await context.Roles
                .FirstOrDefaultAsync(r => r.Name == newRoleName);

            if (newRole == null)
            {
                throw new Exception("Role not found.");
            }

            user.RoleId = newRole.Id;
            await context.SaveChangesAsync();
        }
    }
}


