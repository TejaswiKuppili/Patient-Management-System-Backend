using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Data.Entities;
using PatientManagementSystem.Repository.Interfaces;

namespace PatientManagementSystem.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            await context.RefreshTokens.AddAsync(refreshToken);
            await context.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            return await context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsRevoked);
        }

        public async Task RevokeTokenAsync(RefreshToken refreshToken)
        {
            refreshToken.IsRevoked = true;
            context.RefreshTokens.Update(refreshToken);
            await context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
