using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagementSystem.Data.Entities
{
    /// <summary>
    /// Represents an appointment for a patient.
    /// </summary>
    public class Appointment
    {
        [Key]
        public int Id { get; set; } // PRIMARY KEY

        [Required]
        public int PatientId { get; set; } // FK to Patients

        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; } = null!; // Navigation to Patient

        [Required]
        public DateTime AppointmentStartTime { get; set; }

        [Required]
        public DateTime AppointmentEndTime { get; set; }

        [MaxLength(300)]
        public string? Reason { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Scheduled";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public int CreatedBy { get; set; } // FK to ApplicationUsers.Id

        [ForeignKey(nameof(CreatedBy))]
        public ApplicationUser CreatedByUser { get; set; } = null!; // Navigation to creator (doctor/admin)

        [Required]
        public int DoctorId { get;set; }

        [ForeignKey(nameof(DoctorId))]
        public ApplicationUser Doctor { get; set; } = null!; // Navigation to the doctor handling the appointment
    }
}
