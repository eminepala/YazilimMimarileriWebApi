using Microsoft.EntityFrameworkCore;
using YazilimMimarileri.Data;
using YazilimMimarileri.Services.Interfaces;
using YazilimMimarileri.Services;




var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IKitapService, KitapService>();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"CONNECTION STRING: {conn}");
app.MapGet("/minimal/kitaplar", async (IKitapService kitapService) =>
{
    var kitaplar = await kitapService.GetAllAsync();

    return Results.Ok(new
    {
        success = true,
        message = "Kitaplar listelendi (Minimal API)",
        data = kitaplar
    });
});


app.Run();