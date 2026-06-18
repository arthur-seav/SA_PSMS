using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public string ContractNumber { get; set; } = null!;

    public DateOnly SignedDate { get; set; }

    public string? CustmrIdCard { get; set; }

    public string? CustmrContact { get; set; }

    public string Status { get; set; } = null!;

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CustmrName { get; set; }

    public int CustmrId { get; set; }

    public int PlanId { get; set; }

    public virtual Customer Custmr { get; set; } = null!;

    public virtual InstallmentPlan Plan { get; set; } = null!;
}
