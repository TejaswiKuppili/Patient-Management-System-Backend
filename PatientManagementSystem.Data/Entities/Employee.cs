using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagementSystem.Data.Entities
{
    /// <summary>
    /// Represents an employee in the system.
    /// </summary>
    public class Employee
    {
        [Key]
        public int Id { get; set; } // UNIQUEIDENTIFIER

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = null!; // NVARCHAR(100), NOT NULL

        [Required]
        [MaxLength(256)]
        public string Email { get; set; } = null!; // NVARCHAR(256), UNIQUE, NOT NULL

        [Required]
    
        public string Gender { get; set; }  // NVARCHAR(10), NOT NULL

        [Required]
        [MaxLength(20)]
        public string ContactNumber { get; set; } = null!; // NVARCHAR(20), NOT NULL

        [Required]
        [MaxLength(300)]
        public string Address { get; set; } = null!; // NVARCHAR(300), NOT NULL

        [Required]
        
        public int RoleId { get; set; }// NVARCHAR(50), UNIQUE, NOT NULL

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; } = null!; // FK -> Roles

        [Required]
        public bool IsActive { get; set; } // BOOL, NOT NULL

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now; // DATETIME, DEFAULT GETDATE()
        //
        [Required]
        public int CreatedBy { get; set; }// NVARCHAR(100), NOT NULL
    }
}
