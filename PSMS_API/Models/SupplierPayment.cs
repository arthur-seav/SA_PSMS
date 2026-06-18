using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class SupplierPayment
{
    public int SupplrPaymentId { get; set; }

    public decimal Amount { get; set; }

    public DateOnly PaymentDate { get; set; }

    public int PurchaseId { get; set; }

    public virtual Purchase Purchase { get; set; } = null!;
}
