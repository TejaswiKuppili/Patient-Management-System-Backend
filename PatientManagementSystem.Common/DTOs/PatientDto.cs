namespace PatientManagementSystem.Common.DTOs
{   /// <summary>
    /// Data Transfer Object for patient information.
    /// </summary>
    public class PatientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public string ReasonForVisit { get; set; } = null!;
        public string? CreatedByEmployeeName { get; set; } 
    }
}
