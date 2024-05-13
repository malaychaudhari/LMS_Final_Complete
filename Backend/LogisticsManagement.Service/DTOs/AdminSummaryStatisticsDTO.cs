using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class AdminSummaryStatisticsDTO
    {
        public int WarehousesCount { get; set; }
        public int ManagersCount { get; set; }
        public int DriversCount { get; set; }
        public int CustomersCount { get; set; }
        public int TotalOrdersCount { get; set; }
        public decimal TotalSalesAmount { get; set; }

    }
}
