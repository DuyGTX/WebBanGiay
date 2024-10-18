using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Size
{
    public int SizeId { get; set; }

    public int MaSp { get; set; }

    public string Size1 { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
