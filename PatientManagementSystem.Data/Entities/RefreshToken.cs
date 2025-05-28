﻿
namespace PatientManagementSystem.Data.Entities
{
    /// <summary>
    /// Represents a refresh token used for maintaining user sessions and authentication.
    /// </summary>
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedByIp { get; set; }

        
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
