using KonyvAruhaz.Infrastruktura.Adat;
using KonyvAruhaz.Doman.Entitasok;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KonyvAruhaz.Web.Controllers.Api;

[ApiController]
[Route("api/kategoriak")]
public class KategoriakApiController : ControllerBase
{
    private readonly KonyvAruhazDbContext _db;

    public KategoriakApiController(KonyvAruhazDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Lista()
    {
        var kategoriak = await _db.Kategoriak
            .Select(k => new
            {
                k.Id,
                k.Nev
            })
            .ToListAsync();

        return Ok(kategoriak);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Egy(int id)
    {
        var kategoria = await _db.Kategoriak
            .Where(k => k.Id == id)
            .Select(k => new
            {
                k.Id,
                k.Nev
            })
            .FirstOrDefaultAsync();

        if (kategoria == null)
            return NotFound();

        return Ok(kategoria);
    }

    [HttpPost]
    public async Task<IActionResult> Letrehozas([FromBody] Kategoria ujKategoria)
    {
        _db.Kategoriak.Add(ujKategoria);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Egy), new { id = ujKategoria.Id }, ujKategoria);
    }
}
