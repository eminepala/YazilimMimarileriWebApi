using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YazilimMimarileri.Common;
using YazilimMimarileri.Data;
using YazilimMimarileri.DTOs.Auth;
using YazilimMimarileri.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;





namespace YazilimMimarileri.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthController(AppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var kullanici = await _context.Kullanicilar
            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (kullanici == null)
        {
            return Unauthorized(
                ApiResponse<string>.FailResponse("Kullanıcı bulunamadı")
            );
        }

        var token = _jwtService.GenerateToken(kullanici);

        return Ok(
            ApiResponse<object>.SuccessResponse(new
            {
                accessToken = token
            }, "Giriş başarılı")
        );
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnly()
    {
        return Ok(
            ApiResponse<string>.SuccessResponse(
                "Sadece Admin erişebilir",
                "Yetkilendirme başarılı"
            )
        );
    }
}