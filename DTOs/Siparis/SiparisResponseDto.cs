using YazilimMimarileri.Enums;

namespace YazilimMimarileri.DTOs.Siparis;

public class SiparisResponseDto
{
    public int Id { get; set; }
    public int KullaniciId { get; set; }
    public DateTimeOffset SiparisTarihi { get; set; }
    public OdemeYontemi OdemeYontemi { get; set; }

    public List<SiparisDetayResponseDto> Detaylar { get; set; }
        = new();
}