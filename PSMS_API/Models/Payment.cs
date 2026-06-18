using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string Method { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateOnly PaymentDate { get; set; }

    public int SaleId { get; set; }

    public int? PeriodId { get; set; }

    public virtual InstallmentPeriod? Period { get; set; }

    public virtual Sale Sale { get; set; } = null!;
}
