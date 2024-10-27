using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiay.Models;

public partial class DbwebGiayOnlineContext : DbContext
{
    public DbwebGiayOnlineContext()
    {
    }

    public DbwebGiayOnlineContext(DbContextOptions<DbwebGiayOnlineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Colour> Colours { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderNote> OrderNotes { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleAccount> RoleAccounts { get; set; }

    public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }

    public virtual DbSet<Shoe> Shoes { get; set; }

    public virtual DbSet<ShoeAttribute> ShoeAttributes { get; set; }

    public virtual DbSet<ShoeCategory> ShoeCategories { get; set; }

    public virtual DbSet<ShoeImage> ShoeImages { get; set; }

    public virtual DbSet<ShoeItem> ShoeItems { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-DUY\\SQLEXPRESS;Initial Catalog=DBWebGiayOnline;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK__attribut__9090C9BB6D03B184");

            entity.ToTable("attribute");

            entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
            entity.Property(e => e.AttributeName)
                .HasMaxLength(255)
                .HasColumnName("attribute_name");
        });

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

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB85B82E4FE8");

            entity.ToTable("customer");

            entity.HasIndex(e => e.Email, "UQ__customer__AB6E61644AEADEE4").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CusAddress)
                .HasMaxLength(255)
                .HasColumnName("cus_address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__order__4659622958D90153");

            entity.ToTable("order");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CouponId).HasColumnName("coupon_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.ShippingAddressId).HasColumnName("shipping_address_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Coupon).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CouponId)
                .HasConstraintName("FK__order__coupon_id__693CA210");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__order__customer___656C112C");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK__order__payment_m__66603565");

            entity.HasOne(d => d.ShippingAddress).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShippingAddressId)
                .HasConstraintName("FK__order__shipping___68487DD7");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__order__status_id__6754599E");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__order_it__3764B6BC5E06BAA7");

            entity.ToTable("order_item");

            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ShoeItemId).HasColumnName("shoe_item_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__order_ite__order__6C190EBB");

            entity.HasOne(d => d.ShoeItem).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ShoeItemId)
                .HasConstraintName("FK__order_ite__shoe___6D0D32F4");
        });

        modelBuilder.Entity<OrderNote>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__order_no__CEDD0FA4EFC50179");

            entity.ToTable("order_notes");

            entity.Property(e => e.NoteId).HasColumnName("note_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.NoteText).HasColumnName("note_text");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderNotes)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__order_not__order__70DDC3D8");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__order_st__3683B531643A7DD2");

            entity.ToTable("order_status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__payment___8A3EA9EB458D075A");

            entity.ToTable("payment_method");

            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.MethodName)
                .HasMaxLength(50)
                .HasColumnName("method_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__role__760965CCED52F0EC");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<RoleAccount>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.AccountId }).HasName("PK__RoleAcco__F9B314400B671812");

            entity.ToTable("RoleAccount");

            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Account).WithMany(p => p.RoleAccounts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleAccou__Accou__5812160E");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleAccounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleAccou__RoleI__571DF1D5");
        });

        modelBuilder.Entity<ShippingAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__shipping__CAA247C82568C55C");

            entity.ToTable("shipping_address");

            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .HasColumnName("postal_code");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            entity.Property(e => e.StreetAddress)
                .HasMaxLength(255)
                .HasColumnName("street_address");

            entity.HasOne(d => d.Customer).WithMany(p => p.ShippingAddresses)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__shipping___custo__5EBF139D");
        });

        modelBuilder.Entity<Shoe>(entity =>
        {
            entity.HasKey(e => e.ShoeId).HasName("PK__shoe__3AC0314EB0506C92");

            entity.ToTable("shoe");

            entity.Property(e => e.ShoeId).HasColumnName("shoe_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CareInstructions).HasColumnName("care_instructions");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ShoeDescription).HasColumnName("shoe_description");
            entity.Property(e => e.ShoeName)
                .HasMaxLength(255)
                .HasColumnName("shoe_name");

            entity.HasOne(d => d.Brand).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__shoe__brand_id__3D5E1FD2");

            entity.HasOne(d => d.Category).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__shoe__category_i__3C69FB99");
        });

        modelBuilder.Entity<ShoeAttribute>(entity =>
        {
            entity.HasKey(e => new { e.ShoeId, e.AttributeId }).HasName("PK__shoe_att__93C93DD5E2782B71");

            entity.ToTable("shoe_attribute");

            entity.Property(e => e.ShoeId).HasColumnName("shoe_id");
            entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
            entity.Property(e => e.AttributeValue)
                .HasMaxLength(255)
                .HasColumnName("attribute_value");

            entity.HasOne(d => d.Attribute).WithMany(p => p.ShoeAttributes)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shoe_attr__attri__4E88ABD4");

            entity.HasOne(d => d.Shoe).WithMany(p => p.ShoeAttributes)
                .HasForeignKey(d => d.ShoeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shoe_attr__shoe___4D94879B");
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

        modelBuilder.Entity<ShoeItem>(entity =>
        {
            entity.HasKey(e => e.ShoeItemId).HasName("PK__shoe_ite__9ECD427BA415457D");

            entity.ToTable("shoe_item");

            entity.Property(e => e.ShoeItemId).HasColumnName("shoe_item_id");
            entity.Property(e => e.ColourId).HasColumnName("colour_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.SalePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sale_price");
            entity.Property(e => e.ShoeId).HasColumnName("shoe_id");
            entity.Property(e => e.SizeId).HasColumnName("size_id");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .HasColumnName("sku");
            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");

            entity.HasOne(d => d.Colour).WithMany(p => p.ShoeItems)
                .HasForeignKey(d => d.ColourId)
                .HasConstraintName("FK__shoe_item__colou__44FF419A");

            entity.HasOne(d => d.Shoe).WithMany(p => p.ShoeItems)
                .HasForeignKey(d => d.ShoeId)
                .HasConstraintName("FK__shoe_item__shoe___440B1D61");

            entity.HasOne(d => d.Size).WithMany(p => p.ShoeItems)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK__shoe_item__size___45F365D3");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("PK__size__0DCACE31C597D616");

            entity.ToTable("size");

            entity.Property(e => e.SizeId).HasColumnName("size_id");
            entity.Property(e => e.SizeName)
                .HasMaxLength(10)
                .HasColumnName("size_name");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
