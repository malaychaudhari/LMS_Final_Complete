using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class OrderDTO
    {
        public int? Id { get; set; }

        public int CustomerId { get; set; }

        public string? UserName { get; set; }
        public DateTime? OrderDate { get; set; }

        public decimal? TotalAmount { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? OrderDetailId { get; set; }

        public int InventoryId { get; set; }

        public string? InventoryName { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

        public int OrderStatusId { get; set; }

        public int OriginId { get; set; }

        public int DestinationId { get; set; }

        public DateTime? ExpectedArrivalTime { get; set; }

        public DateTime? ActualArrivalTime { get; set; }

        public int StatusId { get; set; }

        public string? Status { get; set; }
    }
}
