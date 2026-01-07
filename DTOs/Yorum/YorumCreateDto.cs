namespace YazilimMimarileri.DTOs.Yorum;

public class YorumCreateDto
{
    public int KitapId { get; set; }
    public string Icerik { get; set; } = string.Empty;
    public int Puan { get; set; } // 1–5
}