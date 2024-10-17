using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiay.Models;

public partial class DbwebGiayContext : DbContext
{
    public DbwebGiayContext()
    {
    }

    public DbwebGiayContext(DbContextOptions<DbwebGiayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<HinhAnh> HinhAnhs { get; set; }

    public virtual DbSet<Mau> Maus { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=PLINH;Initial Catalog=DBWebGiay;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .HasColumnName("diachi");
            entity.Property(e => e.MaDh)
                .HasMaxLength(255)
                .HasColumnName("maDH");
            entity.Property(e => e.MaNguoiDung).HasColumnName("maNguoiDung");
            entity.Property(e => e.MaSp).HasColumnName("maSP");
            entity.Property(e => e.SoLuong).HasColumnName("soLuong");
            entity.Property(e => e.TongTien).HasColumnName("tongTien");

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDonHang_DonHang");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDonHang_SanPham");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChiTietG__3213E83F13282BB4");

            entity.ToTable("ChiTietGioHang");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MaGioHang).HasColumnName("maGioHang");
            entity.Property(e => e.MaNguoi)
                .HasMaxLength(200)
                .HasColumnName("maNguoi");
            entity.Property(e => e.MaSp).HasColumnName("maSP");
            entity.Property(e => e.Makhuyenmai)
                .HasMaxLength(50)
                .HasColumnName("makhuyenmai");
            entity.Property(e => e.SoLuongSp).HasColumnName("soLuongSP");
            entity.Property(e => e.TongTien).HasColumnName("tongTien");

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGioHang)
                .HasConstraintName("FK_ChiTietGioHang_GioHang");

            entity.HasOne(d => d.MaNguoiNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaNguoi)
                .HasConstraintName("FK_ChiTietGioHang_NguoiDung");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK_ChiTietGioHang_SanPham");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDm).HasName("PK__DanhMuc__7A3EF408DC6597F6");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.MaDm).HasColumnName("maDM");
            entity.Property(e => e.TenDm)
                .HasMaxLength(100)
                .HasColumnName("tenDM");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDh);

            entity.ToTable("DonHang");

            entity.Property(e => e.MaDh)
                .HasMaxLength(255)
                .HasColumnName("maDH");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Diachi)
                .HasMaxLength(700)
                .HasColumnName("diachi");
            entity.Property(e => e.SoLuong).HasColumnName("soLuong");
            entity.Property(e => e.TongTien).HasColumnName("tongTien");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(700)
                .HasColumnName("trangThai");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DonHang_NguoiDung");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__2C76D2032A348BC6");

            entity.ToTable("GioHang");

            entity.Property(e => e.MaGioHang).HasColumnName("maGioHang");
            entity.Property(e => e.MaNguoiDung).HasColumnName("maNguoiDung");
            entity.Property(e => e.SoLuong).HasColumnName("soLuong");
        });

        modelBuilder.Entity<HinhAnh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HinhAnh__3213E83FF8D5CA67");

            entity.ToTable("HinhAnh");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MaSp).HasColumnName("maSP");
            entity.Property(e => e.UrlHinh)
                .HasMaxLength(2000)
                .HasColumnName("urlHinh");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.HinhAnhs)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK_HinhAnh_SanPham");
        });

        modelBuilder.Entity<Mau>(entity =>
        {
            entity.HasKey(e => e.MauId).HasName("PK__Mau__1CF0C9D5822EBD80");

            entity.ToTable("Mau");

            entity.Property(e => e.MauId).HasColumnName("mauID");
            entity.Property(e => e.TenMau)
                .HasMaxLength(2)
                .HasColumnName("tenMau");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.ToTable("NguoiDung");

            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .HasColumnName("username");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.HoTen)
                .HasMaxLength(200)
                .HasColumnName("hoTen");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(200)
                .HasColumnName("matkhau");
            entity.Property(e => e.RoleId).HasColumnName("roleID");
            entity.Property(e => e.Sdt)
                .HasMaxLength(200)
                .HasColumnName("sdt");

            entity.HasOne(d => d.Role).WithMany(p => p.NguoiDungs)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NguoiDung_PhanQuyen");
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__PhanQuye__CD98460A81596C8F");

            entity.ToTable("PhanQuyen");

            entity.Property(e => e.RoleId).HasColumnName("roleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__7A227A7A5BDA6906");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSp).HasColumnName("maSP");
            entity.Property(e => e.ChitietSp)
                .HasMaxLength(1000)
                .HasColumnName("chitietSP");
            entity.Property(e => e.GiaTien).HasColumnName("giaTien");
            entity.Property(e => e.Giamgia).HasColumnName("giamgia");
            entity.Property(e => e.Giasale).HasColumnName("giasale");
            entity.Property(e => e.HinhAnh1)
                .HasMaxLength(700)
                .HasColumnName("hinhAnh1");
            entity.Property(e => e.HinhAnh2)
                .HasMaxLength(700)
                .HasColumnName("hinhAnh2");
            entity.Property(e => e.HinhAnh3)
                .HasMaxLength(700)
                .HasColumnName("hinhAnh3");
            entity.Property(e => e.HinhAnh4)
                .HasMaxLength(700)
                .HasColumnName("hinhAnh4");
            entity.Property(e => e.MaDm).HasColumnName("maDM");
            entity.Property(e => e.MauId).HasColumnName("mauID");
            entity.Property(e => e.Ngaytao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngaytao");
            entity.Property(e => e.SizeId).HasColumnName("sizeID");
            entity.Property(e => e.TenSp)
                .HasMaxLength(700)
                .HasColumnName("tenSP");

            entity.HasOne(d => d.MaDmNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_DanhMuc");

            entity.HasOne(d => d.Mau).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MauId)
                .HasConstraintName("FK_SanPham_Mau");

            entity.HasOne(d => d.Size).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK_SanPham_Size");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("PK__Size__55B1E5778B131ADE");

            entity.ToTable("Size");

            entity.Property(e => e.SizeId).HasColumnName("sizeID");
            entity.Property(e => e.Size1)
                .HasMaxLength(2)
                .HasColumnName("size");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
