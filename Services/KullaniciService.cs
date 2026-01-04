using Microsoft.EntityFrameworkCore;
using YazilimMimarileri.Data;
using YazilimMimarileri.DTOs.Kullanici;
using YazilimMimarileri.Models;
using YazilimMimarileri.Services.Interfaces;

namespace YazilimMimarileri.Services;

public class KullaniciService : IKullaniciService
{
    private readonly AppDbContext _context;

    public KullaniciService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<KullaniciResponseDto>> GetAllAsync()
    {
        return await _context.Kullanicilar
            .Select(k => new KullaniciResponseDto
            {
                Id = k.Id,
                Ad = k.Ad,
                Soyad = k.Soyad,
                Email = k.Email,
                Yas = k.Yas,
                Adres = k.Adres,
                CreatedAt = k.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<KullaniciResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Kullanicilar
            .Where(k => k.Id == id)
            .Select(k => new KullaniciResponseDto
            {
                Id = k.Id,
                Ad = k.Ad,
                Soyad = k.Soyad,
                Email = k.Email,
                Yas = k.Yas,
                Adres = k.Adres,
                CreatedAt = k.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<KullaniciResponseDto> CreateAsync(KullaniciCreateDto dto)
    {
        var kullanici = new Kullanici
        {
            Ad = dto.Ad,
            Soyad = dto.Soyad,
            Email = dto.Email,
            Yas = dto.Yas,
            Adres = dto.Adres
        };

        _context.Kullanicilar.Add(kullanici);
        await _context.SaveChangesAsync();

        return new KullaniciResponseDto
        {
            Id = kullanici.Id,
            Ad = kullanici.Ad,
            Soyad = kullanici.Soyad,
            Email = kullanici.Email,
            Yas = kullanici.Yas,
            Adres = kullanici.Adres,
            CreatedAt = kullanici.CreatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, KullaniciUpdateDto dto)
    {
        var kullanici = await _context.Kullanicilar.FindAsync(id);
        if (kullanici == null) return false;

        kullanici.Ad = dto.Ad;
        kullanici.Soyad = dto.Soyad;
        kullanici.Yas = dto.Yas;
        kullanici.Adres = dto.Adres;
        kullanici.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var kullanici = await _context.Kullanicilar.FindAsync(id);
        if (kullanici == null) return false;

        _context.Kullanicilar.Remove(kullanici);
        await _context.SaveChangesAsync();
        return true;
    }
}
