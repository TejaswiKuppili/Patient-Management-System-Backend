using PatientManagementSystem.Data.Entities;

namespace PatientManagementSystem.Repository.Interfaces
{
    /// <summary>
    /// Interface for refresh token repository operations.
    /// </summary>
    public interface IRefreshTokenRepository
    {
        Task SaveRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetByTokenAsync(string token);
        Task RevokeTokenAsync(RefreshToken refreshToken);
        Task SaveChangesAsync();
    }
}
