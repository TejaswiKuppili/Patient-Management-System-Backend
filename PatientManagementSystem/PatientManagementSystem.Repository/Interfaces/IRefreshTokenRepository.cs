using PatientManagementSystem.Data.Entities;
using System.Threading.Tasks;

namespace PatientManagementSystem.Repository.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task SaveRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetByTokenAsync(string token);
        Task RevokeTokenAsync(RefreshToken refreshToken);
        Task SaveChangesAsync();
    }
}
