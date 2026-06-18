using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class InventoryMovement
{
    public int MovementId { get; set; }

    public string Type { get; set; } = null!;

    public int Qty { get; set; }

    public string? RefType { get; set; }

    public int? RefId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int ProdId { get; set; }

    public virtual Product Prod { get; set; } = null!;
}
