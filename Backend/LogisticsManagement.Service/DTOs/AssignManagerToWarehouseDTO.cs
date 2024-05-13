using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class AssignManagerToWarehouseDTO
    {
        [Required]
        public int ManagerId { get; set; }
        [Required]
        public int WarehouseId { get; set; }

    }
}
