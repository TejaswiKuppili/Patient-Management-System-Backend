
namespace PatientManagementSystem.Data.Entities
{
    /// <summary>
    /// Represents a refresh token used for maintaining user sessions and authentication.
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Gets or sets the unique identifier for the refresh token.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the token string used for refreshing user sessions.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time of the refresh token.
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the refresh token has been revoked.
        /// </summary>
        public bool IsRevoked { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the refresh token was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the IP address from which the refresh token was created.
        /// </summary>
        public string CreatedByIp { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user associated with this refresh token.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with this refresh token.
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}
