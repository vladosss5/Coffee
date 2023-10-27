using System;
using System.Collections.Generic;
using Coffee.Models;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Cooking> Cookings { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<DishCategory> DishCategories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDish> OrderDishes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<StatusesOrder> StatusesOrders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;user id=postgres;password=toor;database=Coffee;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("Category_pkey");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Photo).HasMaxLength(250);
        });

        modelBuilder.Entity<Cooking>(entity =>
        {
            entity.HasKey(e => e.IdCooking).HasName("Cookings_pkey");

            entity.Property(e => e.DateAdmission).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.Cookings)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cookings_IdOrder_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Cookings)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cookings_IdUser_fkey");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.IdDish).HasName("Dishes_pkey");

            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Photo).HasMaxLength(250);
        });

        modelBuilder.Entity<DishCategory>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("DishCategory_pkey");

            entity.ToTable("DishCategory");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.DishCategories)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DishCategory_IdCategory_fkey");

            entity.HasOne(d => d.IdDishNavigation).WithMany(p => p.DishCategories)
                .HasForeignKey(d => d.IdDish)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DishCategory_IdDish_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("Orders_pkey");

            entity.Property(e => e.DateAndTime).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_IdStatus_fkey");
        });

        modelBuilder.Entity<OrderDish>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("OrderDish_pkey");

            entity.ToTable("OrderDish");

            entity.HasOne(d => d.IdDishNavigation).WithMany(p => p.OrderDishes)
                .HasForeignKey(d => d.IdDish)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderDish_IdDish_fkey");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.OrderDishes)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderDish_IdOrder_fkey");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.IdPost).HasName("Posts_pkey");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.IdPromotion).HasName("Promotions_pkey");

            entity.Property(e => e.FinishDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.IdDishNavigation).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.IdDish)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Promotions_IdDish_fkey");
        });

        modelBuilder.Entity<StatusesOrder>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("StatusesOrder_pkey");

            entity.ToTable("StatusesOrder");

            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("Users_pkey");

            entity.Property(e => e.Fname)
                .HasMaxLength(30)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(30)
                .HasColumnName("LName");
            entity.Property(e => e.Login).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.Sname)
                .HasMaxLength(30)
                .HasColumnName("SName");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_IdPost_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
