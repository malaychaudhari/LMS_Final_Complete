using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class VehicleType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
