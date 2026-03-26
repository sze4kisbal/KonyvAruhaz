using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvAruhaz.Doman.Entitasok;

public class RendelesTetel
{
    public int Id { get; set; }

    public int RendelesId { get; set; }
    public Rendeles? Rendeles { get; set; }

    public int KonyvId { get; set; }
    public Konyv? Konyv { get; set; }

    public int Mennyiseg { get; set; }

    public decimal EgysegAr { get; set; }
}
