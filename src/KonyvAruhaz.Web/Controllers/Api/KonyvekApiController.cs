using KonyvAruhaz.Infrastruktura.Adat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KonyvAruhaz.Web.Controllers.Api;

[ApiController]
[Route("api/konyvek")]
public class KonyvekApiController : ControllerBase
{
    private readonly KonyvAruhazDbContext _db;

    public KonyvekApiController(KonyvAruhazDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Lista()
    {
        var lista = await _db.Konyvek
            .Include(x => x.Kategoria)
            .Where(x => x.Aktiv)
            .Select(x => new
            {
                x.Id,
                x.Cim,
                x.Ar,
                Kategoria = x.Kategoria!.Nev
            })
            .ToListAsync();

        return Ok(lista);
    }
}
