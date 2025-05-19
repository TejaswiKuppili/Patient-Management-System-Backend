
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PatientManagementSystem.Data.Entities
{
    public class MedicalRecord
    {
        [Key]
        public int Id { get; set; } // PK: Unique record ID

        [Required]
        public int PatientId { get; set; } // FK -> Patients.Id

        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; } = null!; // Navigation

        public string RecordType { get; set; } 

        [MaxLength(300)]
        public string? FilePath { get; set; } // Nullable: File path or URL

        public string? Notes { get; set; } // Nullable: Extra description or context

        [Required]
        public DateTime UploadedAt { get; set; } = DateTime.Now; // Default GETDATE()
        //
        [Required]
        public int CreatedBy { get; set; } // FK -> Employees.Id

        [ForeignKey(nameof(CreatedBy))]
        public Employee CreatedByEmployee { get; set; } = null!; // Navigation
    }

}
