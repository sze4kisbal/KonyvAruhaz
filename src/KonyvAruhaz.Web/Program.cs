using KonyvAruhaz.Infrastruktura.Adat;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Össze kötés
builder.Services.AddDbContext<KonyvAruhazDbContext>(opcio =>
{
    opcio.UseSqlServer(builder.Configuration.GetConnectionString("AlapAdatbazis"));
});

var app = builder.Build();
//Sajat teszt
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<KonyvAruhaz.Infrastruktura.Adat.KonyvAruhazDbContext>();

    if (!db.Kategoriak.Any())
    {
        var kategoria = new KonyvAruhaz.Doman.Entitasok.Kategoria { Nev = "Regény" };
        db.Kategoriak.Add(kategoria);

        db.Konyvek.Add(new KonyvAruhaz.Doman.Entitasok.Konyv
        {
            Cim = "Teszt könyv",
            Ar = 3990,
            Kategoria = kategoria,
            Aktiv = true
        });

        db.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
