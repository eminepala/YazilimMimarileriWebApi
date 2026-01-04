namespace YazilimMimarileri.DTOs.Kullanici;

public class KullaniciCreateDto
{
    public string Ad { get; set; } = null!;
    public string Soyad { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Yas { get; set; }
    public string Adres { get; set; } = null!;
}