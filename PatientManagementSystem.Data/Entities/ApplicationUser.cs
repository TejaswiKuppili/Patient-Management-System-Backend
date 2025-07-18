using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagementSystem.Data.Entities
{
    /// <summary>
    /// Represents a system user for login and authentication.
    /// </summary>
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; } // UNIQUEIDENTIFIER

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!; // NVARCHAR(100)

        [Required]
        [MaxLength(256)]
        public string Email { get; set; } = null!; // NVARCHAR(256)

        [Required]
        public string PasswordHash { get; set; } = null!; // NVARCHAR(MAX)

        [Required]
       
        public int RoleId { get; set; } // FK to Roles(Id), NVARCHAR(50)

        public DateTime CreatedAt { get; set; } = DateTime.Now; // DEFAULT GETDATE()

        // Navigation Property
        public Role Role { get; set; } = null!;
        public ICollection<RefreshToken> RefreshTokens { get; set; }

        public ICollection<Patient> PatientsCreated { get; set; } = new List<Patient>();

        public int? SpecialtyId { get; set; } // Nullable FK to Specialty (nullable if not all users have it)

        [ForeignKey(nameof(SpecialtyId))]
        public Specialty? Specialty { get; set; }

    }
}
