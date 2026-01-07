namespace YazilimMimarileri.DTOs.Yorum;

public class YorumResponseDto
{
    public int Id { get; set; }
    public int KitapId { get; set; }
    public string Icerik { get; set; } = string.Empty;
    public int Puan { get; set; }
    public DateTime CreatedAt { get; set; }
}