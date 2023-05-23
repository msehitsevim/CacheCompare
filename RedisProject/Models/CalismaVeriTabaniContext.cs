using Microsoft.EntityFrameworkCore;

namespace RedisProject.Models;

public partial class CalismaVeriTabaniContext : DbContext
{
    public CalismaVeriTabaniContext()
    {
    }

    public CalismaVeriTabaniContext(DbContextOptions<CalismaVeriTabaniContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Errlog> Errlogs { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CalismaVeriTabani;Trusted_Connection=True;Integrated Security=SSPI;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Errlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK___errlog__497F6CB44DE5121A");

            entity.ToTable("_errlog");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.ErrLine)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Err_line");
            entity.Property(e => e.ErrMsg)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("Err_msg");
            entity.Property(e => e.ErrNo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Err_no");
            entity.Property(e => e.ErrProc)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("Err_proc");
            entity.Property(e => e.Tarih)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Tarih");
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

