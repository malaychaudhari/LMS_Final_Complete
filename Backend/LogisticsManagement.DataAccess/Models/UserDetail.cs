using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class UserDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? AddressId { get; set; }

    public string? LicenseNumber { get; set; }

    public int? WareHouseId { get; set; }

    public bool? IsAvailable { get; set; }

    public int? IsApproved { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Address? Address { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Warehouse? WareHouse { get; set; }
}
