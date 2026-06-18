using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class PnlCache
{
    public int CacheId { get; set; }

    public decimal TotalRevenue { get; set; }

    public decimal TotalCost { get; set; }

    public decimal? GrossProfit { get; set; }

    public DateTime UpdatedAt { get; set; }
}
