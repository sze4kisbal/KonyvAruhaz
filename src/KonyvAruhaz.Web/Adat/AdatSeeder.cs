using KonyvAruhaz.Infrastruktura.Adat;
using KonyvAruhaz.Doman.Entitasok;
using Microsoft.EntityFrameworkCore;

namespace KonyvAruhaz.Web.Adat;

public static class AdatSeeder
{
    public static async Task SeedAsync(IServiceProvider szolgaltato)
    {
        using var scope = szolgaltato.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<KonyvAruhazDbContext>();

        // Biztonság kedvéért: migrációk lefuttatása
        await db.Database.MigrateAsync();

        // 1) Kategóriák + 2) Könyvek
        if (!await db.Kategoriak.AnyAsync())
        {
            var k1 = new Kategoria { Nev = "Regény" };
            var k2 = new Kategoria { Nev = "Informatika" };

            db.Kategoriak.AddRange(k1, k2);
            await db.SaveChangesAsync();

            db.Konyvek.AddRange(
                new Konyv { Cim = "C# kezdőknek", Ar = 4990m, Aktiv = true, KategoriaId = k2.Id },
                new Konyv { Cim = "SQL alapok", Ar = 3990m, Aktiv = true, KategoriaId = k2.Id },
                new Konyv { Cim = "Egy jó regény", Ar = 2990m, Aktiv = true, KategoriaId = k1.Id }
            );
            await db.SaveChangesAsync();
        }

        // 3) Felhasználók (1 admin + 1 user)
        if (!await db.Set<Felhasznalo>().AnyAsync())
        {
            var admin = new Felhasznalo
            {
                Email = "admin@teszt.hu",
                JelszoHash = "admin123", // MOST még csak demo! később hash
                Szerepkor = "Admin",
                Aktiv = true
            };

            var user = new Felhasznalo
            {
                Email = "jani@teszt.hu",
                JelszoHash = "jani123", // demo
                Szerepkor = "Felhasznalo",
                Aktiv = true
            };

            db.AddRange(admin, user);
            await db.SaveChangesAsync();
        }

        // 4) 1 példa rendelés (ha még nincs)
        if (!await db.Set<Rendeles>().AnyAsync())
        {
            var user = await db.Set<Felhasznalo>()
                .FirstAsync(x => x.Email == "jani@teszt.hu");

            var konyv = await db.Konyvek.FirstAsync();

            var rendeles = new Rendeles
            {
                FelhasznaloId = user.Id,
                LetrehozasIdeje = DateTime.UtcNow,
                Allapot = "Uj",
                Tetelek = new List<RendelesTetel>
                {
                    new RendelesTetel
                    {
                        KonyvId = konyv.Id,
                        Mennyiseg = 2,
                        EgysegAr = konyv.Ar
                    }
                }
            };

            db.Add(rendeles);
            await db.SaveChangesAsync();
        }
    }
}