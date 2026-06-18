using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
