using Microsoft.AspNetCore.Mvc;
using YazilimMimarileri.Common;
using YazilimMimarileri.DTOs.Kitap;
using YazilimMimarileri.Services.Interfaces;

namespace YazilimMimarileri.Controllers;

[ApiController]
[Route("api/kitaplar")]
public class KitapController : ControllerBase
{
    private readonly IKitapService _kitapService;

    public KitapController(IKitapService kitapService)
    {
        _kitapService = kitapService;
    }

    // GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var kitaplar = await _kitapService.GetAllAsync();

        return Ok(
            ApiResponse<List<KitapResponseDto>>
                .SuccessResponse(kitaplar, "Kitaplar listelendi")
        );
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var kitap = await _kitapService.GetByIdAsync(id);

        if (kitap == null)
        {
            return NotFound(
                ApiResponse<string>.FailResponse("Kitap bulunamadı")
            );
        }

        return Ok(
            ApiResponse<KitapResponseDto>.SuccessResponse(kitap)
        );
    }

    // CREATE
    [HttpPost]
    public async Task<IActionResult> Create(KitapCreateDto dto)
    {
        var created = await _kitapService.CreateAsync(dto);

        return Created(
            $"/api/kitaplar/{created.Id}",
            ApiResponse<KitapResponseDto>
                .SuccessResponse(created, "Kitap oluşturuldu")
        );
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, KitapUpdateDto dto)
    {
        var updated = await _kitapService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(
                ApiResponse<string>.FailResponse("Kitap bulunamadı")
            );
        }

        return Ok(
            ApiResponse<string>
                .SuccessResponse(null!, "Kitap güncellendi")
        );
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _kitapService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(
                ApiResponse<string>.FailResponse("Kitap bulunamadı")
            );
        }

        return NoContent(); // 204
    }
}
