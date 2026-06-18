using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class InstallmentPeriod
{
    public int PeriodId { get; set; }

    public int PeriodNo { get; set; }

    public DateOnly DueDate { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly? PaidDate { get; set; }

    public int PlanId { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual InstallmentPlan Plan { get; set; } = null!;
}
