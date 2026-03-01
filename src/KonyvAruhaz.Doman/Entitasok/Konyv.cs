using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvAruhaz.Doman.Entitasok;

public class Konyv
{
    public int Id { get; set; }

    public string Cim { get; set; } = "";
    public decimal Ar { get; set; }

    public int KategoriaId { get; set; }
    public Kategoria? Kategoria { get; set; }

    public bool Aktiv { get; set; } = true;
}