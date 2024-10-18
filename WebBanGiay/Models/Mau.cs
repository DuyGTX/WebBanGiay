using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Mau
{
    public int MauId { get; set; }

    public int MaSp { get; set; }

    public string TenMau { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
