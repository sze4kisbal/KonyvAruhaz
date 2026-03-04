using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvAruhaz.Doman.Entitasok;

public class Felhasznalo
{
    public int Id { get; set; }

    public string Email { get; set; } = "";
    public string JelszoHash { get; set; } = "";

    public string Szerepkor { get; set; } = "Felhasznalo"; // Felhasznalo / Admin
    public bool Aktiv { get; set; } = true;
}
