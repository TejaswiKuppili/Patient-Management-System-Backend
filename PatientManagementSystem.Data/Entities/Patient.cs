using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagementSystem.Data.Entities
{
    /// <summary>
    /// Represents a patient in the system.
    /// </summary>
    public class Patient
    {
        [Key]
        public int Id { get; set; } // UNIQUEIDENTIFIER, PRIMARY KEY

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = null!; // NVARCHAR(100), NOT NULL

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = null!; // NVARCHAR(100), NOT NULL

        [Required]
        public DateOnly DateOfBirth { get; set; } // DATE, NOT NULL


        public string Gender { get; set; } = null!; // NVARCHAR(10), NOT NULL

        [Required]
        [MaxLength(20)]
        public string ContactNumber { get; set; } = null!; // NVARCHAR(20), NOT NULL

        [MaxLength(300)]
        public string? Address { get; set; } // NVARCHAR(300), NULLABLE

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now; // DATETIME, DEFAULT GETDATE()
        
        [Required]
        public int CreatedBy { get; set; } // FK -> Employees.Id
        [Required]
        public string ReasonForVisit { get; set; } = null!;

        [ForeignKey(nameof(CreatedBy))]
        public Employee CreatedByEmployee { get; set; } = null!; 
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
        public ICollection<Vital> Vitals { get; set; } = new List<Vital>();
    }
}
