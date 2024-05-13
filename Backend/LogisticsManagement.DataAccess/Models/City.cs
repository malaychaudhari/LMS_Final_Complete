using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int StateId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual State State { get; set; } = null!;

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
