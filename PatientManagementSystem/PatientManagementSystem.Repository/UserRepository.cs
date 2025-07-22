
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
        /// <returns>User details list and role names list</returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserAndRoleDto> GetUsersAndRolesAsync()
        {

            List<ApplicationUser>? users = await context.ApplicationUsers
                    .Include(user => user.Role)
                    .ToListAsync();

            List<UserDto>? userDtos = users.Select(user => new UserDto
                {
                    Id = user.Id,
                    Name = user.Username,
                    Email = user.Email,
                    RoleName = user.Role?.Name
                }).ToList();

                List<string>? allRoles = await context.Roles
                    .Select(r => r.Name)
                    .ToListAsync();

                return new UserAndRoleDto
                {
                    Users = userDtos,
                    Roles = allRoles
                };
                       
        }
        /// <summary>
        /// Creating new user
        /// </summary>
        /// <param name="newUser"></param>

        /// <exception cref="Exception"></exception>
        public async Task<UserDto> CreateUserAsync(UserDto newUser)
        {
            Role? role = await context.Roles
        .FirstOrDefaultAsync(r => r.Name == newUser.RoleName) ?? null;
            
            
            ApplicationUser? user = new ApplicationUser
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
       
        /// <exception cref="Exception"></exception>
        public async Task UpdateUserRoleAsync(int userId, string newRoleName)
        {
            ApplicationUser? user = await context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            Role? newRole = await context.Roles
                .FirstOrDefaultAsync(r => r.Name == newRoleName);

            if (newRole == null)
            {
                throw new Exception("Role not found.");
            }

            user.RoleId = newRole.Id;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Fetches a user by their email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await context.ApplicationUsers
                .Include(u => u.Role) 
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Username,
                RoleName = user.Role?.Name,
                Password = user.PasswordHash 
            };
        }


        public async Task<ApplicationUser?> GetUserDetailsAsync(int userId)
        {
            return await context.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="userId">ID of the user to delete</param>
        /// <returns>True if deleted, false otherwise</returns>
        /// <exception cref="Exception">If user not found</exception>
        public async Task<bool> DeleteUserAsync(int userId)
        {
            ApplicationUser? user = await context.ApplicationUsers.FindAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            context.ApplicationUsers.Remove(user);
            await context.SaveChangesAsync();

            return true;
        }

    }
}


