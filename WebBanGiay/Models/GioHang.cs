using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class GioHang
{
    public int MaGioHang { get; set; }

    public int? MaNguoiDung { get; set; }

    public int? SoLuong { get; set; }

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; } = new List<ChiTietGioHang>();
}
