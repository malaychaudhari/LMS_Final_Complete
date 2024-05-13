using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class ResourceMapping
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int DriverId { get; set; }

    public int ManagerId { get; set; }

    public int VehicleId { get; set; }

    public DateTime AssignedDate { get; set; }

    public int AssignmentStatusId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual AssignmentStatus AssignmentStatus { get; set; } = null!;

    public virtual User Driver { get; set; } = null!;

    public virtual User Manager { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
