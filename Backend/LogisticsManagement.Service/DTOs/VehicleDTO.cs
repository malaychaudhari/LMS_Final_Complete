using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class VehicleDTO
    {
        public int Id { get; set; }

        public int VehicleTypeId { get; set; }

        public string VehicleType { get; set; }

        public string VehicleNumber { get; set; }

        public int WareHouseId { get; set; }

        public bool? IsAvailable { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
