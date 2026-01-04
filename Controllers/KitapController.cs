using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var kitaplar = await _kitapService.GetAllAsync();

        return Ok(new
        {
            success = true,
            message = "Kitaplar listelendi",
            data = kitaplar
        });
    }
}