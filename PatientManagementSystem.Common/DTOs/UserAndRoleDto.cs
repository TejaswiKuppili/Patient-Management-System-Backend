using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace PatientManagementSystem.Common.DTOs
    {
    /// <summary>
    /// Dto to return User and Role details
    /// </summary>
        public class UserAndRoleDto
        {
            public IEnumerable<UserDto> Users { get; set; }
            public IEnumerable<string> Roles { get; set; }
        }
    }


