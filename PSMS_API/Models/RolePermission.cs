using System;
using System.Collections.Generic;

namespace PSMS_API.Models;

public partial class RolePermission
{
    public int RnpId { get; set; }

    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
