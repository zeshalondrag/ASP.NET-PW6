using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SORAPC.Models;

public partial class SorapcContext : DbContext
{
    public SorapcContext()
    {
    }

    public SorapcContext(DbContextOptions<SorapcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderPosition> OrderPositions { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductStatus> ProductStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ZESHALONDRAG\\SQLEXPRESS;Initial Catalog=sorapc;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PK__Cart__72140ECFFBB4BA27");

            entity.ToTable("Cart");

            entity.Property(e => e.IdCart).HasColumnName("ID_Cart");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.UsersId).HasColumnName("Users_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Cart__Product_ID__6B24EA82");

            entity.HasOne(d => d.Users).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Cart__Users_ID__6A30C649");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Orders__EC9FA9553B60FDEF");

            entity.HasIndex(e => e.OrderNumber, "UQ__Orders__67C7B3CB3D8574AA").IsUnique();

            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Order_Date");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Order_Number");
            entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatus_ID");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Total_Amount");
            entity.Property(e => e.UsersId).HasColumnName("Users_ID");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusId)
                .HasConstraintName("FK__Orders__OrderSta__6383C8BA");

            entity.HasOne(d => d.Users).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Orders__Users_ID__619B8048");
        });

        modelBuilder.Entity<OrderPosition>(entity =>
        {
            entity.HasKey(e => e.IdOrderPosition).HasName("PK__OrderPos__FD3AA9D69D1F7996");

            entity.ToTable("OrderPosition");

            entity.Property(e => e.IdOrderPosition).HasColumnName("ID_OrderPosition");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderPositions)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderPosi__Order__66603565");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderPositions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderPosi__Produ__6754599E");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.IdOrderStatus).HasName("PK__OrderSta__36EC88CF6A7BEB33");

            entity.ToTable("OrderStatus");

            entity.HasIndex(e => e.Title, "UQ__OrderSta__2CB664DC90A0F84D").IsUnique();

            entity.Property(e => e.IdOrderStatus).HasColumnName("ID_OrderStatus");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__Products__522DE49613FACB62");

            entity.HasIndex(e => e.Names, "UQ__Products__44C034864C81CA1F").IsUnique();

            entity.Property(e => e.IdProduct).HasColumnName("ID_Product");
            entity.Property(e => e.Descriptions).IsUnicode(false);
            entity.Property(e => e.Img).IsUnicode(false);
            entity.Property(e => e.Names)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategory_ID");
            entity.Property(e => e.ProductStatusId).HasColumnName("ProductStatus_ID");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK__Products__Produc__5AEE82B9");

            entity.HasOne(d => d.ProductStatus).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductStatusId)
                .HasConstraintName("FK__Products__Produc__59FA5E80");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.IdProductCategory).HasName("PK__ProductC__8FAD631E31CA3F2D");

            entity.ToTable("ProductCategory");

            entity.HasIndex(e => e.Title, "UQ__ProductC__2CB664DC0A500AC6").IsUnique();

            entity.Property(e => e.IdProductCategory).HasColumnName("ID_ProductCategory");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductStatus>(entity =>
        {
            entity.HasKey(e => e.IdProductStatus).HasName("PK__ProductS__92EB9BF6FEBD2215");

            entity.ToTable("ProductStatus");

            entity.HasIndex(e => e.Title, "UQ__ProductS__2CB664DC4D2F6798").IsUnique();

            entity.Property(e => e.IdProductStatus).HasColumnName("ID_ProductStatus");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Roles__43DCD32DE4EDE179");

            entity.HasIndex(e => e.Title, "UQ__Roles__2CB664DC22C16A45").IsUnique();

            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUsers).HasName("PK__Users__B97FFDA1E7B4C070");

            entity.HasIndex(e => e.Phone, "UQ__Users__5C7E359EB0CBEBA5").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105348BF8C691").IsUnique();

            entity.HasIndex(e => e.Logins, "UQ__Users__D00D06327CAFF670").IsUnique();

            entity.Property(e => e.IdUsers).HasColumnName("ID_Users");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Logins)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Passwords).IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(18)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("Role_ID");
            entity.Property(e => e.UserMiddlename)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserSurname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__Role_ID__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
