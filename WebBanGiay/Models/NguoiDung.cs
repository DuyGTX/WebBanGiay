using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class NguoiDung
{
    public string Username { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string Matkhau { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; } = new List<ChiTietGioHang>();

    public virtual ICollection<DonHang> DonHangs { get; } = new List<DonHang>();

    public virtual PhanQuyen Role { get; set; } = null!;
}
