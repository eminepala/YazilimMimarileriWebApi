using YazilimMimarileri.Models;

namespace YazilimMimarileri.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(Kullanici kullanici);
}