using System.ComponentModel.DataAnnotations;

namespace PatientManagementSystem.Data.Entities
{
    /// <summary>
    /// Represents user roles in the system (e.g., Admin, Doctor).
    /// </summary>
    public class Role
    {
        [Key]
        public int Id { get; set; } // UNIQUEIDENTIFIER

        [Required]
        
        public int RoleId { get; set; } // NVARCHAR(50), UNIQUE, NOT NULL

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!; // NVARCHAR(100), UNIQUE, NOT NULL

        [MaxLength(255)]
        public string? Description { get; set; } // NVARCHAR(255), NULLABLE

        // Navigation property (optional)
        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
