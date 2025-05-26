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

        public void SaveRefreshToken(RefreshToken refreshToken)
        {
            context.RefreshTokens.Add(refreshToken);
            context.SaveChanges();
        }

        public RefreshToken GetByToken(string token)
        {
            return context.RefreshTokens.FirstOrDefault(rt => rt.Token == token && !rt.IsRevoked);
        }

        public void RevokeToken(string token)
        {
            var refreshToken = GetByToken(token);
            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
