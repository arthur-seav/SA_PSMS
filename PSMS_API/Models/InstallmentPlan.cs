using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class InstallmentPlan
{
    public int PlanId { get; set; }

    public int Periods { get; set; }

    public decimal AmountPerPeriod { get; set; }

    public DateTime CreatedAt { get; set; }

    public int SaleId { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<InstallmentPeriod> InstallmentPeriods { get; set; } = new List<InstallmentPeriod>();

    public virtual Sale Sale { get; set; } = null!;
}
