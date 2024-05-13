using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class WarehouseDTO
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]

        public string? Address { get; set; }

        [Required]

        public int CityId { get; set; }

        public string? City { get; set; } 
        public string? State { get; set; } 

        public string? Country { get; set; } 

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
