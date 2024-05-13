using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
