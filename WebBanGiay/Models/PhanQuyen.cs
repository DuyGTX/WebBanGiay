using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class PhanQuyen
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<NguoiDung> NguoiDungs { get; } = new List<NguoiDung>();
}
