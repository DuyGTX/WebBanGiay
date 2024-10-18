using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class ChiTietGioHang
{
    public int Id { get; set; }

    public int? MaGioHang { get; set; }

    public int? SoLuongSp { get; set; }

    public int? MaSp { get; set; }

    public string? MaNguoi { get; set; }

    public string? Makhuyenmai { get; set; }

    public int? TongTien { get; set; }

    public virtual GioHang? MaGioHangNavigation { get; set; }

    public virtual NguoiDung? MaNguoiNavigation { get; set; }

    public virtual SanPham? MaSpNavigation { get; set; }
}
