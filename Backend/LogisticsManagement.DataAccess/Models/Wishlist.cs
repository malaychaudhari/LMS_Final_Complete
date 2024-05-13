using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class Wishlist
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int InventoryId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
