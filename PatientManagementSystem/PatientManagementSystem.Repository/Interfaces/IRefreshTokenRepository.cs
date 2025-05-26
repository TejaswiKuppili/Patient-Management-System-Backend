using PatientManagementSystem.Data.Entities;
namespace PatientManagementSystem.Repository.Interfaces
{
    public interface IRefreshTokenRepository
    {
        void SaveRefreshToken(RefreshToken refreshToken);
        RefreshToken GetByToken(string token);
        void SaveChanges();
        void RevokeToken(string token);

    }
}
