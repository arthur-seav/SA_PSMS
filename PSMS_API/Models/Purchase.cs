using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public decimal TotalCost { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal? Balance { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int SupplrId { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

    public virtual ICollection<PurchaseReturn> PurchaseReturns { get; set; } = new List<PurchaseReturn>();

    public virtual ICollection<SupplierPayment> SupplierPayments { get; set; } = new List<SupplierPayment>();

    public virtual Supplier Supplr { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
