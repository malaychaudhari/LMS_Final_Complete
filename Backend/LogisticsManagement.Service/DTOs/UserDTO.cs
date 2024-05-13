using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } 

        public string Email { get; set; } 

        public string Password { get; set; }

        public string Phone { get; set; } 

        public int RoleId { get; set; }

        public int? AddressId { get; set; }

        public string? LicenseNumber { get; set; }

        public int? WareHouseId { get; set; }

        public bool? IsAvailable { get; set; }

        public int? IsApproved { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
