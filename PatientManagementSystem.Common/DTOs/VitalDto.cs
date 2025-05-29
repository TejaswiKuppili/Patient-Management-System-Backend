namespace PatientManagementSystemAPI.Common.DTOs
{
    /// <summary>
    /// Data Transfer Object representing vital signs and measurements for a patient.
    /// </summary>
    public class VitalDto
    {
        public int Id { get; set; }              // Unique vitals ID
        public int PatientId { get; set; }       // Associated patient ID
        public DateTime RecordedAt { get; set; } // Date and time the vitals were recorded
        public string? BloodPressure { get; set; } // e.g., "120/80"
        public int? HeartRate { get; set; }        // Beats per minute
        public decimal? Temperature { get; set; }  // In Celsius
        public int? RespiratoryRate { get; set; }  // Breaths per minute
        public int CreatedBy { get; set; }         // Employee ID who recorded this
    }
}
