using System.ComponentModel.DataAnnotations.Schema;

namespace YazilimMimarileri.Models;

public class Yorum
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

 
    public int KitapId { get; set; }

    public required string Icerik { get; set; }
    public int Puan { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public Kitap Kitap { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;

}