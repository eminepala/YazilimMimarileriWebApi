using Microsoft.AspNetCore.Mvc;
using YazilimMimarileri.Common;
using YazilimMimarileri.DTOs.Siparis;
using YazilimMimarileri.Services.Interfaces;

namespace YazilimMimarileri.Controllers;

[ApiController]
[Route("api/siparisler")]
public class SiparisController : ControllerBase
{
    private readonly ISiparisService _siparisService;

    public SiparisController(ISiparisService siparisService)
    {
        _siparisService = siparisService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _siparisService.GetAllAsync();

        return Ok(
            ApiResponse<List<SiparisResponseDto>>
                .SuccessResponse(data, "Siparişler listelendi")
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SiparisCreateDto dto)
    {
        var data = await _siparisService.CreateAsync(dto);

        return Created(
            $"api/siparisler/{data.Id}",
            ApiResponse<SiparisResponseDto>
                .SuccessResponse(data, "Sipariş oluşturuldu")
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SiparisUpdateDto dto)
    {
        var updated = await _siparisService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(
                ApiResponse<string>.FailResponse("Sipariş bulunamadı")
            );
        }

        return Ok(
            ApiResponse<string>.SuccessResponse(null!, "Sipariş güncellendi")
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _siparisService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(
                ApiResponse<string>.FailResponse("Sipariş bulunamadı")
            );
        }

        return NoContent();
    }
}