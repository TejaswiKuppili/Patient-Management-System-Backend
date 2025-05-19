
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
        public int Id { get; set; } // UNIQUEIDENTIFIER, PRIMARY KEY

        [Required]
        public int PatientId { get; set; } // FOREIGN KEY REFERENCES Patients(Id)

        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; } = null!; // Navigation to Patient

        [Required]
        public DateTime AppointmentDateTime { get; set; } // DATETIME, NOT NULL

        [MaxLength(300)]
        public string? Reason { get; set; } // NVARCHAR(300), NULLABLE

        [MaxLength(50)]
        public string Status { get; set; } 

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now; // DATETIME, DEFAULT GETDATE()

        [Required]
        //

        public int CreatedBy { get; set; } // FK -> ApplicationUsers.Id

        [ForeignKey(nameof(CreatedBy))]
        public ApplicationUser CreatedByUser { get; set; } // Navigation to ApplicationUser
        
    }
}
