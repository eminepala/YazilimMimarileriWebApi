using System.ComponentModel.DataAnnotations.Schema;

namespace YazilimMimarileri.Models;

public class Kullanici
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Ad { get; set; }
    public required string Soyad { get; set; }
    public required string Email { get; set; }
    public int Yas { get; set; }
    public required string Adres { get; set; }

    //JWT alanları
    public string KullaniciAdi { get; set; } = null!;
    public string SifreHash { get; set; } = null!;
    public string Rol { get; set; } = "User";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Siparis> Siparisler { get; set; } = new List<Siparis>();

    public bool IsDeleted { get; set; } = false;
}