using YazilimMimarileri.DTOs.Siparis;

namespace YazilimMimarileri.Services.Interfaces;

public interface ISiparisService
{
    Task<List<SiparisResponseDto>> GetAllAsync();
    Task<SiparisResponseDto> CreateAsync(SiparisCreateDto dto);
    Task<bool> UpdateAsync(int siparisId, SiparisUpdateDto dto);
    Task<bool> DeleteAsync(int siparisId);
}