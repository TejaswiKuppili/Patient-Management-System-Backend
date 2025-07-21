using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementSystem.Common.DTOs
{
    /// <summary>
    ///  Data Transfer Object (DTO) for doctor information.
    /// </summary>
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SpecialtyName { get; set; }
        public string EmailId { get; set; }

    }
}
