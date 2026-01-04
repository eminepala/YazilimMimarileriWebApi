using YazilimMimarileri.DTOs.Kullanici;

namespace YazilimMimarileri.Services.Interfaces;

public interface IKullaniciService
{
    Task<List<KullaniciResponseDto>> GetAllAsync();
    Task<KullaniciResponseDto?> GetByIdAsync(int id);
    Task<KullaniciResponseDto> CreateAsync(KullaniciCreateDto dto);
    Task<bool> UpdateAsync(int id, KullaniciUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}