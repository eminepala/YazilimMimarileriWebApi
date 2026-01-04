using Microsoft.EntityFrameworkCore;
using YazilimMimarileri.Data;
using YazilimMimarileri.DTOs.Siparis;
using YazilimMimarileri.Models;
using YazilimMimarileri.Services.Interfaces;

namespace YazilimMimarileri.Services;
public class SiparisService : ISiparisService
{
    private readonly AppDbContext _context;

    public SiparisService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SiparisResponseDto>> GetAllAsync()
    {
        return await _context.Siparisler
            .Include(s => s.SiparisDetaylari)
            .ThenInclude(sd => sd.Kitap)
            .Select(s => new SiparisResponseDto
            {
                Id = s.Id,
                KullaniciId = s.KullaniciId,
                SiparisTarihi = s.SiparisTarihi,
                OdemeYontemi = s.OdemeYontemi,
                Detaylar = s.SiparisDetaylari.Select(sd => new SiparisDetayResponseDto
                {
                    KitapId = sd.KitapId,
                    KitapAdi = sd.Kitap.Ad,
                    Adet = sd.Adet,
                    BirimFiyat = sd.BirimFiyat
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<SiparisResponseDto> CreateAsync(SiparisCreateDto dto)
    {
        var kullanici = await _context.Kullanicilar.FindAsync(dto.KullaniciId)
            ?? throw new Exception("Kullanıcı bulunamadı");

        var kitap = await _context.Kitaplar.FindAsync(dto.KitapId)
            ?? throw new Exception("Kitap bulunamadı");

        if (kitap.Stok < dto.Adet)
            throw new Exception("Yetersiz stok");

        kitap.Stok -= dto.Adet;

        var siparis = new Siparis
        {
            KullaniciId = dto.KullaniciId
        };

        siparis.SiparisDetaylari.Add(new SiparisDetay
        {
            KitapId = dto.KitapId,
            Adet = dto.Adet,
            BirimFiyat = kitap.Fiyat
        });

        _context.Siparisler.Add(siparis);
        await _context.SaveChangesAsync();

        return new SiparisResponseDto
        {
            Id = siparis.Id,
            KullaniciId = siparis.KullaniciId,
            SiparisTarihi = siparis.SiparisTarihi,
            OdemeYontemi = siparis.OdemeYontemi,
            Detaylar = siparis.SiparisDetaylari.Select(sd => new SiparisDetayResponseDto
            {
                KitapId = sd.KitapId,
                KitapAdi = kitap.Ad,
                Adet = sd.Adet,
                BirimFiyat = sd.BirimFiyat
            }).ToList()
        };
    }
    public async Task<bool> UpdateAsync(int siparisId, SiparisUpdateDto dto)
    {
        var siparis = await _context.Siparisler
            .Include(s => s.SiparisDetaylari)
            .FirstOrDefaultAsync(s => s.Id == siparisId);

        if (siparis == null)
            return false;

        var detay = siparis.SiparisDetaylari.First();
        var kitap = await _context.Kitaplar.FindAsync(detay.KitapId)
                    ?? throw new Exception("Kitap bulunamadı");

        // eski adedi stoğa iade et
        kitap.Stok += detay.Adet;

        if (kitap.Stok < dto.Adet)
            throw new Exception("Yetersiz stok");

        // yeni adedi düş
        kitap.Stok -= dto.Adet;

        detay.Adet = dto.Adet;
        detay.BirimFiyat = kitap.Fiyat;
        siparis.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(int siparisId)
    {
        var siparis = await _context.Siparisler
            .Include(s => s.SiparisDetaylari)
            .FirstOrDefaultAsync(s => s.Id == siparisId);

        if (siparis == null)
            return false;

        foreach (var detay in siparis.SiparisDetaylari)
        {
            var kitap = await _context.Kitaplar.FindAsync(detay.KitapId);
            if (kitap != null)
            {
                kitap.Stok += detay.Adet;
            }
        }

        _context.Siparisler.Remove(siparis);
        await _context.SaveChangesAsync();

        return true;
    }

    
}
