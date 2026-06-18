using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class SaleDetail
{
    public int SaleDetailId { get; set; }

    public int Qty { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Discount { get; set; }

    public int SaleId { get; set; }

    public int ProdId { get; set; }

    public virtual Product Prod { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;

    public virtual ICollection<SaleReturn> SaleReturns { get; set; } = new List<SaleReturn>();
}
