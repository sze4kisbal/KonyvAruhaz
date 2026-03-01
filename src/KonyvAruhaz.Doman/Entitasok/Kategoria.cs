using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvAruhaz.Doman.Entitasok;

public class Kategoria
{
    public int Id { get; set; }
    public string Nev { get; set; } = "";

    public List<Konyv> Konyvek { get; set; } = new();
}
