using YazilimMimarileri.DTOs.Kitap;

namespace YazilimMimarileri.Services.Interfaces;

public interface IKitapService
{
    Task<List<KitapResponseDto>> GetAllAsync();
    Task<KitapResponseDto?> GetByIdAsync(int id);
    Task<KitapResponseDto> CreateAsync(KitapCreateDto dto);
    Task<bool> UpdateAsync(int id, KitapUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}