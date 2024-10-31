using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class RoleAccount
{
    public int RoleId { get; set; }

    public int AccountId { get; set; }

    public bool Status { get; set; }

    public virtual Customer Account { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
