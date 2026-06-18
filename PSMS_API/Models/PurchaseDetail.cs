using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class PurchaseDetail
{
    public int PurchaseDetailId { get; set; }

    public int Qty { get; set; }

    public decimal UnitCost { get; set; }

    public decimal Deduction { get; set; }

    public int PurchaseId { get; set; }

    public int ProdId { get; set; }

    public virtual Product Prod { get; set; } = null!;

    public virtual Purchase Purchase { get; set; } = null!;

    public virtual ICollection<PurchaseReturn> PurchaseReturns { get; set; } = new List<PurchaseReturn>();
}
