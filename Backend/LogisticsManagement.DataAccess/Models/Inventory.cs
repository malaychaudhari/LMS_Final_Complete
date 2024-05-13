using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class Inventory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string? Description { get; set; }

    public int Stock { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public int WarehouseId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual InventoryCategory Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Warehouse Warehouse { get; set; } = null!;

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
