
using System.ComponentModel.DataAnnotations;

namespace PatientManagementSystem.Common.DTOs
{
    public class ProfileDto
    {
       
        public int Id { get; set; }

        
        public int ApplicationUserId { get; set; }

       
        public byte[]? ProfilePicture { get; set; }


        public DateTime? DateOfBirth { get; set; }


        [MaxLength(10)]
        public string? Gender { get; set; } = null!;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(500)]
        public string? Bio { get; set; }

        [MaxLength(300)]
        public string? Address { get; set; }

        public string? City { get; set; }

        public string? CityState { get; set; }

        public string? Country { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}


