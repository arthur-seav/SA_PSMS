using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public DateOnly SaleDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal? Balance { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int CustmrId { get; set; }

    public int UserId { get; set; }

    public virtual Customer Custmr { get; set; } = null!;

    public virtual ICollection<InstallmentPlan> InstallmentPlans { get; set; } = new List<InstallmentPlan>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    public virtual ICollection<SaleReturn> SaleReturns { get; set; } = new List<SaleReturn>();

    public virtual User User { get; set; } = null!;
}
