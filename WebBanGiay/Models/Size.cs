using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Size
{
    public int SizeId { get; set; }

    public string Size1 { get; set; } = null!;

    public virtual ICollection<SanPham> SanPhams { get; } = new List<SanPham>();
}
