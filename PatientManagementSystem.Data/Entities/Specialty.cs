using System.ComponentModel.DataAnnotations;

namespace PatientManagementSystem.Data.Entities
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
