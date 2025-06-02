using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagementSystem.Data.Entities
{
    /// <summary>
    /// Represents vital signs and measurements for a patient.
    /// </summary>
    public class Vital
    {
        [Key]
        public int Id { get; set; } // PK: Unique vitals ID

        [Required]
        public int PatientId { get; set; } // FK -> Patients.Id

        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; } = null!; // Navigation

        [Required]
        public DateTime RecordedAt { get; set; } = DateTime.Now; // Default GETDATE()

        [MaxLength(20)]
        public string? BloodPressure { get; set; } // Nullable, e.g., "120/80"

        public int? HeartRate { get; set; } // Nullable: beats per minute

        [Column(TypeName = "decimal(4,1)")]
        public decimal? Temperature { get; set; } // Nullable: Celsius

        public int? RespiratoryRate { get; set; } // Nullable: breaths per minute
        
        [Required]
        public int CreatedBy { get; set; } // FK -> Employees.Id

        [ForeignKey(nameof(CreatedBy))]
        public ApplicationUser CreatedByEmployee { get; set; } = null!; // Navigation
    }
}
