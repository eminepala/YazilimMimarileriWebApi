using Microsoft.EntityFrameworkCore;
using YazilimMimarileri.Data;
using YazilimMimarileri.Services;
using YazilimMimarileri.Services.Interfaces;
using System.Text.Json.Serialization;
using YazilimMimarileri.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IKitapService, KitapService>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();
builder.Services.AddScoped<ISiparisService, SiparisService>();
builder.Services.AddScoped<IYorumService, YorumService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "YazilimMimarileri API v1");
});

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();