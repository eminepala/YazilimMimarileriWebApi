using Microsoft.EntityFrameworkCore;
using YazilimMimarileri.Data;
using YazilimMimarileri.DTOs.Yorum;
using YazilimMimarileri.Models;
using YazilimMimarileri.Services.Interfaces;

namespace YazilimMimarileri.Services;

public class YorumService : IYorumService
{
    private readonly AppDbContext _context;

    public YorumService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<YorumResponseDto>> GetByKitapIdAsync(int kitapId)
    {
        return await _context.Yorumlar
            .Where(y => y.KitapId == kitapId)
            .Select(y => new YorumResponseDto
            {
                Id = y.Id,
                KitapId = y.KitapId,
                Icerik = y.Icerik,
                Puan = y.Puan,
                CreatedAt = y.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<YorumResponseDto> CreateAsync(YorumCreateDto dto)
    {
        var kitapVarMi = await _context.Kitaplar.AnyAsync(k => k.Id == dto.KitapId);
        if (!kitapVarMi)
            throw new Exception("Kitap bulunamadı");

        var yorum = new Yorum
        {
            KitapId = dto.KitapId,
            Icerik = dto.Icerik,
            Puan = dto.Puan
        };

        _context.Yorumlar.Add(yorum);
        await _context.SaveChangesAsync();

        return new YorumResponseDto
        {
            Id = yorum.Id,
            KitapId = yorum.KitapId,
            Icerik = yorum.Icerik,
            Puan = yorum.Puan,
            CreatedAt = yorum.CreatedAt
        };
    }

    public async Task<bool> UpdateAsync(int id, YorumUpdateDto dto)
    {
        var yorum = await _context.Yorumlar.FindAsync(id);
        if (yorum == null)
            return false;

        yorum.Icerik = dto.Icerik;
        yorum.Puan = dto.Puan;
        yorum.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var yorum = await _context.Yorumlar.FindAsync(id);
        if (yorum == null)
            return false;

        _context.Yorumlar.Remove(yorum);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<YorumResponseDto>> GetAllAsync()
    {
        return await _context.Yorumlar
            .Select(y => new YorumResponseDto
            {
                Id = y.Id,
                KitapId = y.KitapId,
                Icerik = y.Icerik,
                Puan = y.Puan,
                CreatedAt = y.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<YorumResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Yorumlar
            .Where(y => y.Id == id)
            .Select(y => new YorumResponseDto
            {
                Id = y.Id,
                KitapId = y.KitapId,
                Icerik = y.Icerik,
                Puan = y.Puan,
                CreatedAt = y.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

}
