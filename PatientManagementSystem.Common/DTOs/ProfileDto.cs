
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using PatientManagementSystem.Common.Enums;
namespace PatientManagementSystem.Common.DTOs
{   /// <summary>
/// Data transfer objects for profile table
/// </summary>
    public class ProfileDto
    {
       
        public int Id { get; set; }
        [Required]
        
        public int ApplicationUserId { get; set; }

       
        public byte[]? ProfilePicture { get; set; }


        public DateTime? DateOfBirth { get; set; }
        [AllowNull]
        public string? Date { get; set; }

        [MaxLength(10)]
        public Gender? Gender { get; set; } = null!;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(500)]
        public string? Bio { get; set; }

        [MaxLength(300)]
        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public Dictionary<int, string>? GenderOptions { get; set; }
    }
}


