using Microsoft.EntityFrameworkCore;
using YazilimMimarileri.Data;
using YazilimMimarileri.DTOs.Kitap;
using YazilimMimarileri.Models;
using YazilimMimarileri.Services.Interfaces;

namespace YazilimMimarileri.Services;

public class KitapService : IKitapService
{
    private readonly AppDbContext _context;

    public KitapService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<KitapResponseDto>> GetAllAsync()
    {
        return await _context.Kitaplar
            .Select(k => new KitapResponseDto
            {
                Id = k.Id,
                Ad = k.Ad,
                Yazar = k.Yazar,
                Aciklama = k.Aciklama,
                Fiyat = k.Fiyat,
                Stok = k.Stok,
                CreatedAt = k.CreatedAt,
                UpdatedAt = k.UpdatedAt
            })
            .ToListAsync();
    }

    public async Task<KitapResponseDto?> GetByIdAsync(int id)
    {
        var kitap = await _context.Kitaplar.FindAsync(id);
        if (kitap == null) return null;

        return new KitapResponseDto
        {
            Id = kitap.Id,
            Ad = kitap.Ad,
            Yazar = kitap.Yazar,
            Aciklama = kitap.Aciklama,
            Fiyat = kitap.Fiyat,
            Stok = kitap.Stok,
            CreatedAt = kitap.CreatedAt,
            UpdatedAt = kitap.UpdatedAt
        };
    }

    public async Task<KitapResponseDto> CreateAsync(KitapCreateDto dto)
    {
        var kitap = new Kitap
        {
            Ad = dto.Ad,
            Yazar = dto.Yazar,
            Aciklama = dto.Aciklama,
            Fiyat = dto.Fiyat,
            Stok = dto.Stok
        };

        _context.Kitaplar.Add(kitap);
        await _context.SaveChangesAsync();

        return new KitapResponseDto
        {
            Id = kitap.Id,
            Ad = kitap.Ad,
            Yazar = kitap.Yazar,
            Aciklama = kitap.Aciklama,
            Fiyat = kitap.Fiyat,
            Stok = kitap.Stok,
            CreatedAt = kitap.CreatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, KitapUpdateDto dto)
    {
        var kitap = await _context.Kitaplar.FindAsync(id);
        if (kitap == null) return false;

        kitap.Ad = dto.Ad;
        kitap.Yazar = dto.Yazar;
        kitap.Aciklama = dto.Aciklama;
        kitap.Fiyat = dto.Fiyat;
        kitap.Stok = dto.Stok;
        kitap.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var kitap = await _context.Kitaplar.FindAsync(id);
        if (kitap == null) return false;

        _context.Kitaplar.Remove(kitap);
        await _context.SaveChangesAsync();
        return true;
    }
}
