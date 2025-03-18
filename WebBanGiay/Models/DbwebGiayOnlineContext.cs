using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiay.Models;

public partial class DbwebGiayOnlineContext : IdentityDbContext<AppUserModel>
{
    public DbwebGiayOnlineContext()
    {
    }

    public DbwebGiayOnlineContext(DbContextOptions<DbwebGiayOnlineContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Colour> Colours { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<OrderModel> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Shipping> Shippings { get; set; }
    public virtual DbSet<WishList> WishLists { get; set; }

    public virtual DbSet<Shoe> Shoes { get; set; }
	public virtual DbSet<ProductQuantityModel> ProductQuantities { get; set; }

	public virtual DbSet<ShoeCategory> ShoeCategories { get; set; }

    public virtual DbSet<ShoeColour> ShoeColours { get; set; }

    public virtual DbSet<ShoeImage> ShoeImages { get; set; }

    public virtual DbSet<ShoeSize> ShoeSizes { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-DUY\\SQLEXPRESS;Initial Catalog=DBWebGiayOnline;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
		modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
		modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__brand__5E5A8E273CA2B74C");

            entity.ToTable("brand");

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(255)
                .HasColumnName("brand_name");
        });

        modelBuilder.Entity<Colour>(entity =>
        {
            entity.HasKey(e => e.ColourId).HasName("PK__colour__38955022F3DFA4F0");

            entity.ToTable("colour");

            entity.Property(e => e.ColourId).HasColumnName("colour_id");
            entity.Property(e => e.ColourName)
                .HasMaxLength(50)
                .HasColumnName("colour_name");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.CouponId).HasName("PK__coupon__58CF63895EE0BE10");

            entity.ToTable("coupon");

            entity.HasIndex(e => e.CouponCode, "UQ__coupon__ADE5CBB7EF1D0423").IsUnique();

            entity.Property(e => e.CouponId).HasColumnName("coupon_id");
            entity.Property(e => e.CouponCode)
                .HasMaxLength(50)
                .HasColumnName("coupon_code");
            entity.Property(e => e.DiscountAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("discount_amount");
            entity.Property(e => e.DiscountPercentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("discount_percentage");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ValidFrom)
                .HasColumnType("date")
                .HasColumnName("valid_from");
            entity.Property(e => e.ValidTo)
                .HasColumnType("date")
                .HasColumnName("valid_to");
        });

        

        

        

        
           
           

        modelBuilder.Entity<Shoe>(entity =>
        {
            entity.HasKey(e => e.ShoeId).HasName("PK__shoe__3AC0314EB0506C92");

            entity.ToTable("shoe");

            entity.Property(e => e.ShoeId).HasColumnName("shoe_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CareInstructions).HasColumnName("care_instructions");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.SalePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sale_price");
            entity.Property(e => e.ShoeDescription).HasColumnName("shoe_description");
            entity.Property(e => e.ShoeName)
                .HasMaxLength(255)
                .HasColumnName("shoe_name");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .HasColumnName("sku");

            entity.HasOne(d => d.Brand).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__shoe__brand_id__3D5E1FD2");

            entity.HasOne(d => d.Category).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__shoe__category_i__3C69FB99");
        });

       

        modelBuilder.Entity<ShoeCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__shoe_cat__D54EE9B4FFA3D24E");

            entity.ToTable("shoe_category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");

            entity.HasOne(d => d.Brand).WithMany(p => p.ShoeCategories)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__shoe_cate__brand__398D8EEE");
        });

        modelBuilder.Entity<ShoeColour>(entity =>
        {
            entity.HasKey(e => new { e.ShoeId, e.ColourId }).HasName("PK__shoe_col__0949644C695F25A2");

            entity.ToTable("shoe_colour");

            entity.Property(e => e.ShoeId).HasColumnName("shoe_id");
            entity.Property(e => e.ColourId).HasColumnName("colour_id");
            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");

            entity.HasOne(d => d.Colour).WithMany(p => p.ShoeColours)
                .HasForeignKey(d => d.ColourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shoe_colo__colou__7C1A6C5A");

            entity.HasOne(d => d.Shoe).WithMany(p => p.ShoeColours)
                .HasForeignKey(d => d.ShoeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shoe_colo__shoe___7D0E9093");
        });

        modelBuilder.Entity<ShoeImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__shoe_ima__DC9AC955AFF94A72");

            entity.ToTable("shoe_image");

            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.ShoeId).HasColumnName("shoe_id");

            entity.HasOne(d => d.Shoe).WithMany(p => p.ShoeImages)
                .HasForeignKey(d => d.ShoeId)
                .HasConstraintName("FK__shoe_imag__shoe___48CFD27E");
        });

        modelBuilder.Entity<ShoeSize>(entity =>
        {
            entity.HasKey(e => new { e.ShoeId, e.SizeId }).HasName("PK__shoe_siz__3A1C9DAD0EBF47AA");

            entity.ToTable("shoe_size");

            entity.Property(e => e.ShoeId).HasColumnName("shoe_id");
            entity.Property(e => e.SizeId).HasColumnName("size_id");
            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");

            entity.HasOne(d => d.Shoe).WithMany(p => p.ShoeSizes)
                .HasForeignKey(d => d.ShoeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shoe_size__shoe___00DF2177");

            entity.HasOne(d => d.Size).WithMany(p => p.ShoeSizes)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shoe_size__size___7FEAFD3E");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("PK__size__0DCACE31C597D616");

            entity.ToTable("size");

            entity.Property(e => e.SizeId).HasColumnName("size_id");
            entity.Property(e => e.SizeName)
                .HasMaxLength(10)
                .HasColumnName("size_name");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
