
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PatientManagementSystem.Repository
{
    /// <summary>
    /// Repository for accessing user data from the database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersWithRolesAsync()
        {
            try
            {
                return await context.ApplicationUsers
                    .Include(user => user.Role)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception (consider using your NLogger)
                throw new Exception("An error occurred while retrieving users with roles.", ex);
            }
        }
    }

}
