using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class DonHang
{
    public string MaDh { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Diachi { get; set; } = null!;

    public double TongTien { get; set; }

    public int SoLuong { get; set; }

    public string TrangThai { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; } = new List<ChiTietDonHang>();

    public virtual NguoiDung UsernameNavigation { get; set; } = null!;
}
