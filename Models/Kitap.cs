using System.ComponentModel.DataAnnotations.Schema;

namespace YazilimMimarileri.Models;

public class Kitap
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Ad { get; set; }
    public required string Yazar { get; set; }
    public required string Aciklama { get; set; }

    public float Fiyat { get; set; }
    public int Stok { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<Yorum> Yorumlar { get; set; } = new List<Yorum>();
    public ICollection<SiparisDetay> SiparisDetaylari { get; set; } = new List<SiparisDetay>();
    public bool IsDeleted { get; set; } = false;

}