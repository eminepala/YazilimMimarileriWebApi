using System.ComponentModel.DataAnnotations.Schema;
using YazilimMimarileri.Enums;

namespace YazilimMimarileri.Models;

public class Siparis
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int KullaniciId { get; set; }

    public DateTimeOffset SiparisTarihi { get; set; } = DateTimeOffset.UtcNow;
    public OdemeYontemi OdemeYontemi { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<SiparisDetay> SiparisDetaylari { get; set; } = new List<SiparisDetay>();
    public bool IsDeleted { get; set; } = false;

    
    
    
}