using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvAruhaz.Doman.Entitasok;

public class Rendeles
{
    public int Id { get; set; }

    public int FelhasznaloId { get; set; }
    public Felhasznalo? Felhasznalo { get; set; }

    public DateTime LetrehozasIdeje { get; set; } = DateTime.UtcNow;

    public string Allapot { get; set; } = "Uj"; // Uj, FeldolgozasAlatt, Feladva, Teljesitve, Torolve

    public List<RendelesTetel> Tetelek { get; set; } = new();
}
