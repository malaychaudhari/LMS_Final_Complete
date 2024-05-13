using System;
using System.Collections.Generic;

namespace LogisticsManagement.DataAccess.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OrderId { get; set; }

    public string? PaymentMode { get; set; }

    public string? TransactionStatus { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
