using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class SaleReturn
{
    public int SaleReturnId { get; set; }

    public int Qty { get; set; }

    public decimal RefundAmount { get; set; }

    public DateOnly ReturnDate { get; set; }

    public string? Reason { get; set; }

    public int SaleId { get; set; }

    public int SaleDetailId { get; set; }

    public int UserId { get; set; }

    public virtual Sale Sale { get; set; } = null!;

    public virtual SaleDetail SaleDetail { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
