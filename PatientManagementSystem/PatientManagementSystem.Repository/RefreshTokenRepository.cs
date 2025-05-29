using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;

namespace PatientManagementSystem.Data.Repositories
{
    /// <summary>
    /// Repository for managing refresh tokens in the database.
    /// </summary>
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Saves a new refresh token to the database.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            await context.RefreshTokens.AddAsync(refreshToken);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Retrieves a refresh token by its token string, ensuring it is not revoked.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            return await context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsRevoked);
        }
        /// <summary>
        /// Revokes a refresh token by marking it as revoked in the database.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task RevokeTokenAsync(RefreshToken refreshToken)
        {
            refreshToken.IsRevoked = true;
            context.RefreshTokens.Update(refreshToken);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Saves all changes made in the context to the database asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
