namespace YazilimMimarileri.DTOs.Siparis;

public class SiparisDetayResponseDto
{
    public int KitapId { get; set; }
    public string KitapAdi { get; set; } = string.Empty;
    public int Adet { get; set; }
    public float BirimFiyat { get; set; }
}