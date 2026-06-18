using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class Customer
{
    public int CustmrId { get; set; }

    public string CustmrName { get; set; } = null!;

    public string? Contact { get; set; }

    public string? Address { get; set; }

    public int? CreditScore { get; set; }

    public string CreditStatus { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
