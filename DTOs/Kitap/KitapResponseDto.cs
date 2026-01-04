namespace YazilimMimarileri.DTOs.Kitap;

public class KitapResponseDto
{
    public int Id { get; set; }
    public string Ad { get; set; } = null!;
    public string Yazar { get; set; } = null!;
    public string Aciklama { get; set; } = null!;
    public float Fiyat { get; set; }
    public int Stok { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}