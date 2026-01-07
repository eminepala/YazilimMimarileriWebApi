using YazilimMimarileri.DTOs.Yorum;

namespace YazilimMimarileri.Services.Interfaces;

public interface IYorumService
{
    Task<List<YorumResponseDto>> GetByKitapIdAsync(int kitapId);
    Task<YorumResponseDto> CreateAsync(YorumCreateDto dto);
    Task<bool> UpdateAsync(int id, YorumUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}