using KonyvAruhaz.Infrastruktura.Adat;
using KonyvAruhaz.Web.Adat;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC + API
builder.Services.AddControllersWithViews();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB
builder.Services.AddDbContext<KonyvAruhazDbContext>(opcio =>
{
    opcio.UseSqlServer(builder.Configuration.GetConnectionString("AlapAdatbazis"));
});

var app = builder.Build();

// Seed + migráció (csak EZ kell, a "Sajat teszt" blokkot töröljük)
await AdatSeeder.SeedAsync(app.Services);

// Swagger UI csak Developmentben
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// API route-ok (ApiController-ekhez)
app.MapControllers();

app.Run();