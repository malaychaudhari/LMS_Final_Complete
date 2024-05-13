using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class Address
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Address1 { get; set; } = null!;

    public string Pincode { get; set; } = null!;

    public int CityId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
