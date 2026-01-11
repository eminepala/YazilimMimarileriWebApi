using Microsoft.EntityFrameworkCore;
using YazilimMimarileri.Models;

namespace YazilimMimarileri.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    
    private static readonly DateTime SeedDate = new DateTime(2026, 1, 11);

    public DbSet<Kullanici> Kullanicilar { get; set; }
    public DbSet<Kitap> Kitaplar { get; set; }
    public DbSet<Siparis> Siparisler { get; set; }
    public DbSet<SiparisDetay> SiparisDetaylari { get; set; }
    public DbSet<Yorum> Yorumlar { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        

        modelBuilder.Entity<Kullanici>().HasData(
            new Kullanici
            {
                Id = 1,
                Ad = "Emine",
                Soyad = "PALA",
                Email = "emine@test.com",
                Yas = 21,
                Adres = "Denizli",
                CreatedAt = SeedDate,
                IsDeleted = false
            }
        );

        modelBuilder.Entity<Kitap>().HasData(
            new Kitap
            {
                Id = 1,
                Ad = "Küçük Prens",
                Yazar = "Antoine de Saint-Exupéry",
                Fiyat = 105,
                Stok = 10,
                Aciklama =
                    "Küçük Prens, Antoine de Saint-Exupéry tarafından yazılmış klasik bir eserdir.",
                CreatedAt = SeedDate,
                IsDeleted = false
            }
        );

        modelBuilder.Entity<Siparis>().HasData(
            new Siparis
            {
                Id = 1,
                KullaniciId = 1,
                CreatedAt = SeedDate,
                IsDeleted = false
            }
        );

        modelBuilder.Entity<SiparisDetay>().HasData(
            new SiparisDetay
            {
                Id = 1,
                SiparisId = 1,
                KitapId = 1,
                Adet = 1,
                CreatedAt = SeedDate,
                IsDeleted = false
            }
        );

        modelBuilder.Entity<Yorum>().HasData(
            new Yorum
            {
                Id = 1,
                KitapId = 1,
                Icerik = "Her yaştaki kişi okumalı",
                Puan = 5,
                CreatedAt = SeedDate,
                IsDeleted = false
            }
        );

        

        // Kitap - Yorum (1-N)
        modelBuilder.Entity<Yorum>()
            .HasOne<Kitap>()
            .WithMany()
            .HasForeignKey(y => y.KitapId)
            .OnDelete(DeleteBehavior.Cascade);

        // Siparis - Kullanici (N-1)
        modelBuilder.Entity<Siparis>()
            .HasOne<Kullanici>()
            .WithMany(k => k.Siparisler)
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

        // ===================== SOFT DELETE FILTER =====================

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
