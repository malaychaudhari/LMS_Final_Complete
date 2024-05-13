using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int InventoryId { get; set; }

    public int Quantity { get; set; }

    public decimal SubTotal { get; set; }

    public int OrderStatusId { get; set; }

    public int OriginId { get; set; }

    public int DestinationId { get; set; }

    public DateTime? ExpectedArrivalTime { get; set; }

    public DateTime? ActualArrivalTime { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Address Destination { get; set; } = null!;

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual OrderStatus OrderStatus { get; set; } = null!;

    public virtual Warehouse Origin { get; set; } = null!;
}
