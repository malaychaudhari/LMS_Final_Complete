using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class OrderStatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
