using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class AuditLog
{
    public int LogId { get; set; }

    public string ActionType { get; set; } = null!;

    public string? EntityName { get; set; }

    public int? EntityId { get; set; }

    public string? Description { get; set; }

    public string? IpAddress { get; set; }

    public DateTime Timestamp { get; set; }

    public string? RecordHash { get; set; }

    public string? PrevHash { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
