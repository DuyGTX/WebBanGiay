using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class SanPham
{
    public int MaSp { get; set; }

    public int MaDm { get; set; }

    public string TenSp { get; set; } = null!;

    public double GiaTien { get; set; }

    public double Giasale { get; set; }

    public string? ChitietSp { get; set; }

    public int? Giamgia { get; set; }

    public string? HinhAnh1 { get; set; }

    public string? HinhAnh2 { get; set; }

    public string? HinhAnh3 { get; set; }

    public string? HinhAnh4 { get; set; }

    public DateTime Ngaytao { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; } = new List<ChiTietDonHang>();

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; } = new List<ChiTietGioHang>();

    public virtual ICollection<HinhAnh> HinhAnhs { get; } = new List<HinhAnh>();

    public virtual DanhMuc MaDmNavigation { get; set; } = null!;

    public virtual ICollection<Mau> Maus { get; } = new List<Mau>();

    public virtual ICollection<Size> Sizes { get; } = new List<Size>();
}
