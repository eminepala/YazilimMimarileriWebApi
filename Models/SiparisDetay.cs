using System.ComponentModel.DataAnnotations.Schema;

namespace YazilimMimarileri.Models;

public class SiparisDetay
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int SiparisId { get; set; }
    public int KitapId { get; set; }
    
    public int Adet { get; set; }
    public float BirimFiyat { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}