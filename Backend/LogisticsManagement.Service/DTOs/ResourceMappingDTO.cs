using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class ResourceMappingDTO
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }

        public int DriverId { get; set; }

        public int ManagerId { get; set; }
        public string? ManagerName { get; set; }

        public int VehicleId { get; set; }
        public string? VehicleType { get; set; }
        public string? VehicleNumber { get; set; }

        public DateTime AssignedDate { get; set; }

        public int AssignmentStatusId { get; set; } = 1;
        public string? AssignmentStatus { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

}
