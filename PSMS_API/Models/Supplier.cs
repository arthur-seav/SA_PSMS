using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class Supplier
{
    public int SupplrId { get; set; }

    public string Name { get; set; } = null!;

    public string? Contact { get; set; }

    public string? Address { get; set; }

    public string? Categories { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
