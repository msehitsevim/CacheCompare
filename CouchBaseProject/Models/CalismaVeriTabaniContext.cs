using Microsoft.EntityFrameworkCore;

namespace CouchBaseProject.Models;

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
            entity.HasKey(e => e.Guid).HasName("PK___errlog__497F6CB44DE5121A");

            entity.ToTable("_errlog");

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.ErrLine)
                .HasDefaultValueSql("((1))")
                .HasColumnName("err_line");
            entity.Property(e => e.ErrMsg)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("err_msg");
            entity.Property(e => e.ErrNo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("err_no");
            entity.Property(e => e.ErrProc)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("err_proc");
            entity.Property(e => e.Tarih)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("tarih");
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

