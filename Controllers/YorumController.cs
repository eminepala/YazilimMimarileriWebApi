using Microsoft.AspNetCore.Mvc;
using YazilimMimarileri.Common;
using YazilimMimarileri.DTOs.Yorum;
using YazilimMimarileri.Services.Interfaces;

namespace YazilimMimarileri.Controllers;

[ApiController]
[Route("api/yorumlar")]
public class YorumController : ControllerBase
{
    private readonly IYorumService _yorumService;

    public YorumController(IYorumService yorumService)
    {
        _yorumService = yorumService;
    }

    // GET api/yorumlar
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _yorumService.GetAllAsync();

        return Ok(
            ApiResponse<List<YorumResponseDto>>
                .SuccessResponse(data, "Tüm yorumlar listelendi")
        );
    }

    // GET api/yorumlar/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _yorumService.GetByIdAsync(id);

        if (data == null)
        {
            return NotFound(
                ApiResponse<string>.FailResponse("Yorum bulunamadı")
            );
        }

        return Ok(
            ApiResponse<YorumResponseDto>.SuccessResponse(data)
        );
    }

    // GET api/yorumlar/kitap/3
    [HttpGet("kitap/{kitapId}")]
    public async Task<IActionResult> GetByKitapId(int kitapId)
    {
        var data = await _yorumService.GetByKitapIdAsync(kitapId);

        return Ok(
            ApiResponse<List<YorumResponseDto>>
                .SuccessResponse(data, "Kitaba ait yorumlar listelendi")
        );
    }

    // POST api/yorumlar
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] YorumCreateDto dto)
    {
        if (dto.Puan < 1 || dto.Puan > 5)
        {
            return BadRequest(
                ApiResponse<string>.FailResponse("Puan 1 ile 5 arasında olmalıdır")
            );
        }

        var data = await _yorumService.CreateAsync(dto);

        return Created(
            $"api/yorumlar/{data.Id}",
            ApiResponse<YorumResponseDto>.SuccessResponse(data, "Yorum eklendi")
        );
    }

    // PUT api/yorumlar/10
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] YorumUpdateDto dto)
    {
        if (dto.Puan < 1 || dto.Puan > 5)
        {
            return BadRequest(
                ApiResponse<string>.FailResponse("Puan 1 ile 5 arasında olmalıdır")
            );
        }

        var updated = await _yorumService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(
                ApiResponse<string>.FailResponse("Yorum bulunamadı")
            );
        }

        return Ok(
            ApiResponse<string>.SuccessResponse(null!, "Yorum güncellendi")
        );
    }

    // DELETE api/yorumlar/10
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _yorumService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(
                ApiResponse<string>.FailResponse("Yorum bulunamadı")
            );
        }

        return NoContent(); // 204
    }
}
