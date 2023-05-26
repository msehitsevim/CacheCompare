using Entities;
using Microsoft.EntityFrameworkCore;

namespace RedisProject.Models;

public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext()
    {
    }

    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ErrorLog> Errlogs { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CalismaVeriTabani;Trusted_Connection=True;Integrated Security=SSPI;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK___errlog__497F6CB44DE5121A");

            entity.ToTable("ErrorLog");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.ErrorLine)
                .HasDefaultValueSql("((1))")
                .HasColumnName("ErrorLine");
            entity.Property(e => e.ErrorMessage)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("ErrorMessage");
            entity.Property(e => e.ErrorNo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("ErrorNo");
            entity.Property(e => e.ErrorDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Tarih");
        });




        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

