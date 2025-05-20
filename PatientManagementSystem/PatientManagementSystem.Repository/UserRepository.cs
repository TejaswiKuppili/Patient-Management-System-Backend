
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Common.DTOs;
using PatientManagementSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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


