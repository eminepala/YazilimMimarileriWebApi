namespace YazilimMimarileri.DTOs.Kullanici;

public class KullaniciUpdateDto
{
    public string Ad { get; set; } = null!;
    public string Soyad { get; set; } = null!;
    public int Yas { get; set; }
    public string Adres { get; set; } = null!;
}