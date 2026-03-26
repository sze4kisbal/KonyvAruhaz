using KonyvAruhaz.Infrastruktura.Adat;
using KonyvAruhaz.Doman.Entitasok;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KonyvAruhaz.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KonyvekController : ControllerBase
{
    private readonly KonyvAruhazDbContext _db;

    public KonyvekController(KonyvAruhazDbContext db)
    {
        _db = db;
    }

    // GET: api/konyvek
    [HttpGet]
    public async Task<IActionResult> GetKonyvek()
    {
        var lista = await _db.Konyvek
            .Include(k => k.Kategoria)
            .Select(k => new
            {
                id = k.Id,
                cim = k.Cim,
                ar = k.Ar,
                kategoria = k.Kategoria.Nev
            })
            .ToListAsync();

        return Ok(lista);
    }

    // GET: api/konyvek/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetKonyv(int id)
    {
        var konyv = await _db.Konyvek
            .Include(k => k.Kategoria)
            .Where(k => k.Id == id)
            .Select(k => new
            {
                id = k.Id,
                cim = k.Cim,
                ar = k.Ar,
                kategoria = k.Kategoria.Nev
            })
            .FirstOrDefaultAsync();

        if (konyv == null)
            return NotFound();

        return Ok(konyv);
    }

    // POST: api/konyvek
    [HttpPost]
    public async Task<IActionResult> CreateKonyv([FromBody] Konyv ujKonyv)
    {
        _db.Konyvek.Add(ujKonyv);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetKonyv), new { id = ujKonyv.Id }, ujKonyv);
    }

    // PUT: api/konyvek/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateKonyv(int id, [FromBody] Konyv modositott)
    {
        var konyv = await _db.Konyvek.FindAsync(id);

        if (konyv == null)
            return NotFound();

        konyv.Cim = modositott.Cim;
        konyv.Ar = modositott.Ar;
        konyv.KategoriaId = modositott.KategoriaId;
        konyv.Aktiv = modositott.Aktiv;

        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/konyvek/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteKonyv(int id)
    {
        var konyv = await _db.Konyvek.FindAsync(id);

        if (konyv == null)
            return NotFound();

        _db.Konyvek.Remove(konyv);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}