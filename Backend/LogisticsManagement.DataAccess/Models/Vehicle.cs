using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class Vehicle
{
    public int Id { get; set; }

    public int VehicleTypeId { get; set; }

    public string VehicleNumber { get; set; } = null!;

    public bool? IsAvailable { get; set; }

    public int WareHouseId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ResourceMapping> ResourceMappings { get; set; } = new List<ResourceMapping>();

    public virtual VehicleType VehicleType { get; set; } = null!;

    public virtual Warehouse WareHouse { get; set; } = null!;
}
