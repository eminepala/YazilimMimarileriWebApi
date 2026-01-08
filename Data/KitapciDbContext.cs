using Microsoft.EntityFrameworkCore;
using YazilimMimarileri.Models;

namespace YazilimMimarileri.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Kullanici> Kullanicilar { get; set; }
    public DbSet<Kitap> Kitaplar { get; set; }
    public DbSet<Siparis> Siparisler { get; set; }
    public DbSet<SiparisDetay> SiparisDetaylari { get; set; }
    public DbSet<Yorum> Yorumlar { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       
        // Kitap - Yorum (1-N)
        modelBuilder.Entity<Yorum>()
            .HasOne<Kitap>()
            .WithMany()
            .HasForeignKey(y => y.KitapId)
            .OnDelete(DeleteBehavior.Cascade);

        // Siparis - Kullanici (N-1)
        modelBuilder.Entity<Siparis>()
            .HasOne<Kullanici>()
            .WithMany()
            .HasForeignKey(s => s.KullaniciId)
            .OnDelete(DeleteBehavior.Restrict);

        // SiparisDetay - Siparis (N-1)
        modelBuilder.Entity<SiparisDetay>()
            .HasOne<Siparis>()
            .WithMany()
            .HasForeignKey(sd => sd.SiparisId)
            .OnDelete(DeleteBehavior.Cascade);

        // SiparisDetay - Kitap (N-1)
        modelBuilder.Entity<SiparisDetay>()
            .HasOne<Kitap>()
            .WithMany()
            .HasForeignKey(sd => sd.KitapId)
            .OnDelete(DeleteBehavior.Restrict);

      
        modelBuilder.Entity<Kullanici>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Kitap>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Siparis>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<SiparisDetay>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Yorum>()
            .HasQueryFilter(x => !x.IsDeleted);
    }
}
