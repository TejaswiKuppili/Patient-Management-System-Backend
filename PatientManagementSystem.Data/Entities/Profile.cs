using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PatientManagementSystem.Common.Enums;
namespace PatientManagementSystem.Data.Entities
    {
        public class Profile
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public int ApplicationUserId { get; set; }

            [ForeignKey(nameof(ApplicationUserId))]
            public ApplicationUser ApplicationUser { get; set; } = null!;
            [AllowNull]
            public byte[]? ProfilePicture {get;set;}        
            
            public DateTime? DateOfBirth { get; set; }

            [MaxLength(10)]
            public Gender? Gender { get; set; } = null!;

            [MaxLength(20)]
            public string? PhoneNumber { get; set; }

            [MaxLength(500)]
            public string? Bio { get; set; }

            [MaxLength(300)]
            public string? Address { get; set; }
            
            public string? City { get; set; }
            
            public string? CityState { get; set; }
            
            public string? Country { get; set; }
        }
}


