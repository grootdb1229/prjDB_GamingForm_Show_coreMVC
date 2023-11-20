using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prjDB_GamingForm_Show.Models;

public partial class DbDemoContext : DbContext
{
    public DbDemoContext()
    {
    }

    public DbDemoContext(DbContextOptions<DbDemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TCustomer> TCustomers { get; set; }

    public virtual DbSet<TProduct> TProducts { get; set; }

    public virtual DbSet<TShoppingCart> TShoppingCarts { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=dbDemo;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TCustomer>(entity =>
        {
            entity.HasKey(e => e.FId);

            entity.ToTable("tCustomer");

            entity.Property(e => e.FId).HasColumnName("fId");
            entity.Property(e => e.FAddress)
                .HasMaxLength(50)
                .HasColumnName("fAddress");
            entity.Property(e => e.FEmail)
                .HasMaxLength(50)
                .HasColumnName("fEmail");
            entity.Property(e => e.FName)
                .HasMaxLength(50)
                .HasColumnName("fName");
            entity.Property(e => e.FPassword)
                .HasMaxLength(50)
                .HasColumnName("fPassword");
            entity.Property(e => e.FPhone)
                .HasMaxLength(50)
                .HasColumnName("fPhone");
        });

        modelBuilder.Entity<TProduct>(entity =>
        {
            entity.HasKey(e => e.FId);

            entity.ToTable("tProduct");

            entity.Property(e => e.FId).HasColumnName("fId");
            entity.Property(e => e.FCost)
                .HasColumnType("money")
                .HasColumnName("fCost");
            entity.Property(e => e.FImagePath)
                .HasMaxLength(50)
                .HasColumnName("fImagePath");
            entity.Property(e => e.FName)
                .HasMaxLength(50)
                .HasColumnName("fName");
            entity.Property(e => e.FPrice)
                .HasColumnType("money")
                .HasColumnName("fPrice");
            entity.Property(e => e.FQty).HasColumnName("fQty");
        });

        modelBuilder.Entity<TShoppingCart>(entity =>
        {
            entity.HasKey(e => e.FId);

            entity.ToTable("tShoppingCart");

            entity.Property(e => e.FId).HasColumnName("fId");
            entity.Property(e => e.FCount).HasColumnName("fCount");
            entity.Property(e => e.FCustomerId).HasColumnName("fCustomerId");
            entity.Property(e => e.FDate)
                .HasMaxLength(50)
                .HasColumnName("fDate");
            entity.Property(e => e.FPrice)
                .HasColumnType("money")
                .HasColumnName("fPrice");
            entity.Property(e => e.FProductId).HasColumnName("fProductId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
