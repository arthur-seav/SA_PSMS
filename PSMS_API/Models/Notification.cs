using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class Notification
{
    public int NotifyId { get; set; }

    public string Type { get; set; } = null!;

    public string Message { get; set; } = null!;

    public int UserId { get; set; }

    public string? RefType { get; set; }

    public int? RefId { get; set; }

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
