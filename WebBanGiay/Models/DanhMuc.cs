using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class DanhMuc
{
    public int MaDm { get; set; }

    public string TenDm { get; set; } = null!;

    public virtual ICollection<SanPham> SanPhams { get; } = new List<SanPham>();
}
