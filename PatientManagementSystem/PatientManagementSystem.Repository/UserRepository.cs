
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

            public async Task<UserAndRoleDto> GetUsersAndRolesAsync()
            {
                try
                {
                    var users = await context.ApplicationUsers
                        .Include(user => user.Role)
                        .ToListAsync();

                    var userDtos = users.Select(user => new UserDto
                    {
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
        }
    }


