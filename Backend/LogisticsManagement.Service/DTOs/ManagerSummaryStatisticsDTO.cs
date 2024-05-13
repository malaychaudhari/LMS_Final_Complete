using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class ManagerSummaryStatisticsDTO
    {
        public decimal InventoryCount { get; set; }
        public int InventoryCategoryCount{ get; set; }
        public int VehicleCount { get; set; }
        public int AvailableVehicleCount { get; set; }
        public int VehicleTypeCount { get; set; }
        public int DriverCount { get; set; }
        public int AvailableDriverCount { get; set; }
        public int OrderCount { get; set; }
        public int PendingOrderCount{ get; set; }
    }
}
