using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class PurchaseReturn
{
    public int PurchaseReturnId { get; set; }

    public int Qty { get; set; }

    public decimal DeductionAmount { get; set; }

    public string? Reason { get; set; }

    public DateOnly ReturnDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public int PurchaseId { get; set; }

    public int PurchaseDetailId { get; set; }

    public int UserId { get; set; }

    public virtual Purchase Purchase { get; set; } = null!;

    public virtual PurchaseDetail PurchaseDetail { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
