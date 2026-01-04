using Microsoft.AspNetCore.Mvc;
using YazilimMimarileri.Common;
using YazilimMimarileri.DTOs.Kullanici;
using YazilimMimarileri.Services.Interfaces;

namespace YazilimMimarileri.Controllers;

[ApiController]
[Route("api/kullanicilar")]
public class KullaniciController : ControllerBase
{
    private readonly IKullaniciService _kullaniciService;

    public KullaniciController(IKullaniciService kullaniciService)
    {
        _kullaniciService = kullaniciService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _kullaniciService.GetAllAsync();

        return Ok(
            ApiResponse<List<KullaniciResponseDto>>
                .SuccessResponse(data, "Kullanıcılar listelendi")
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _kullaniciService.GetByIdAsync(id);

        if (data == null)
        {
            return NotFound(
                ApiResponse<string>.FailResponse("Kullanıcı bulunamadı")
            );
        }

        return Ok(
            ApiResponse<KullaniciResponseDto>.SuccessResponse(data)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create(KullaniciCreateDto dto)
    {
        var data = await _kullaniciService.CreateAsync(dto);

        return Created("",
            ApiResponse<KullaniciResponseDto>
                .SuccessResponse(data, "Kullanıcı oluşturuldu")
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, KullaniciUpdateDto dto)
    {
        var updated = await _kullaniciService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(
                ApiResponse<string>.FailResponse("Kullanıcı bulunamadı")
            );
        }

        return Ok(
            ApiResponse<string>.SuccessResponse(null!, "Kullanıcı güncellendi")
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _kullaniciService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(
                ApiResponse<string>.FailResponse("Kullanıcı bulunamadı")
            );
        }

        return NoContent(); // 204 – body yok, REST'e uygun
    }
}
