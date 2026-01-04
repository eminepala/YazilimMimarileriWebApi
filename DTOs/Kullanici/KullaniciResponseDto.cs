namespace YazilimMimarileri.DTOs.Kullanici;

public class KullaniciResponseDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
    public string Soyad { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int Yas { get; set; }
    public string Adres { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}