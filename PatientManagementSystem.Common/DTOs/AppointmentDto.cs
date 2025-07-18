using System;

namespace PatientManagementSystem.Common.DTOs
{
    /// <summary>
    /// Data Transfer Object for Appointment
    /// </summary>
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        public DateTime AppointmentStartTime { get; set; }

        public DateTime AppointmentEndTime { get; set; }

        public string? Reason { get; set; }

        public string Status { get; set; } = "Scheduled";

        public DateTime CreatedAt { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;

        public int CreatedBy { get; set; }  
        
    }
}
