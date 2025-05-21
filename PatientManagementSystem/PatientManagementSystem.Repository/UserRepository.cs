
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

            public async Task<UserAndRoleDto> GetUsersAndRolesAsync()
            {

            List<ApplicationUser>? users = await context.ApplicationUsers
                        .Include(user => user.Role)
                        .ToListAsync();

                    List<UserDto>? userDtos = users.Select(user => new UserDto
                    {
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
        }
    }


