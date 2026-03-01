using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KonyvAruhaz.Doman.Entitasok;

namespace KonyvAruhaz.Infrastruktura.Adat;

public class KonyvAruhazDbContext : DbContext
{
    public KonyvAruhazDbContext(DbContextOptions<KonyvAruhazDbContext> beallitasok)
        : base(beallitasok)
    {
    }
    public DbSet<Konyv> Konyvek => Set<Konyv>();
    public DbSet<Kategoria> Kategoriak => Set<Kategoria>();

    //18,2 = max 18 számjegy összesen, 2 tizedes — pénzhez jó.
    protected override void OnModelCreating(ModelBuilder modell)
    {
        base.OnModelCreating(modell);

        modell.Entity<Konyv>()
            .Property(x => x.Ar)
            .HasPrecision(18, 2);
    }



}
