using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int InventoryId { get; set; }

    public int Quantity { get; set; }

    public decimal SubTotal { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
