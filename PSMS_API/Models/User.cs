using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Contact { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<PurchaseReturn> PurchaseReturns { get; set; } = new List<PurchaseReturn>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SaleReturn> SaleReturns { get; set; } = new List<SaleReturn>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
