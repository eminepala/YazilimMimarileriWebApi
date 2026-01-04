namespace YazilimMimarileri.DTOs.Kitap;

public class KitapUpdateDto
{
    public string Ad { get; set; } = null!;
    public string Yazar { get; set; } = null!;
    public string Aciklama { get; set; } = null!;
    public float Fiyat { get; set; }
    public int Stok { get; set; }
}