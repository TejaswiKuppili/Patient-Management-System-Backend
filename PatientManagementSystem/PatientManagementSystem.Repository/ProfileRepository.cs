using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;

namespace PatientManagementSystem.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProfileRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Gets a profile by its associated ApplicationUserId.
        /// </summary>
        public async Task<Profile?> GetByUserIdAsync(int userId)
        {
            return await dbContext.Profiles
                .Include(p=>p.ApplicationUser)
                .FirstOrDefaultAsync(p => p.ApplicationUserId == userId);
        }

        /// <summary>
        /// Adds a new profile to the database.
        /// </summary>
        public async Task AddAsync(Profile profile)
        {
            await dbContext.Profiles.AddAsync(profile);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing profile in the database.
        /// </summary>
        public async Task UpdateAsync(Profile profile)
        {
            dbContext.Profiles.Update(profile);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a profile from the database.
        /// </summary>
        public async Task DeleteAsync(Profile profile)
        {
            dbContext.Profiles.Remove(profile);
            await dbContext.SaveChangesAsync();
        }
    }
}
