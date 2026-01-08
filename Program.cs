using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using YazilimMimarileri.Common;
using YazilimMimarileri.Data;
using YazilimMimarileri.Services;
using YazilimMimarileri.Services.Interfaces;
using YazilimMimarileri.DTOs.Kullanici;
using YazilimMimarileri.DTOs.Kitap;
using YazilimMimarileri.DTOs.Siparis;
using YazilimMimarileri.DTOs.Yorum;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddScoped<IKitapService, KitapService>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();
builder.Services.AddScoped<ISiparisService, SiparisService>();
builder.Services.AddScoped<IYorumService, YorumService>();
builder.Services.AddScoped<IJwtService, JwtService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();



app.MapGet("/minimal/kullanicilar", async (IKullaniciService s) =>
    Results.Ok(ApiResponse<object>.SuccessResponse(await s.GetAllAsync()))
);

app.MapGet("/minimal/kullanicilar/{id:int}", async (int id, IKullaniciService s) =>
{
    var data = await s.GetByIdAsync(id);
    return data == null
        ? Results.NotFound(ApiResponse<string>.FailResponse("Kullanıcı bulunamadı"))
        : Results.Ok(ApiResponse<object>.SuccessResponse(data));
});

app.MapPost("/minimal/kullanicilar", async (KullaniciCreateDto dto, IKullaniciService s) =>
    Results.Created("", ApiResponse<object>.SuccessResponse(await s.CreateAsync(dto)))
);

app.MapPut("/minimal/kullanicilar/{id:int}", async (int id, KullaniciUpdateDto dto, IKullaniciService s) =>
{
    var ok = await s.UpdateAsync(id, dto);
    return ok
        ? Results.Ok(ApiResponse<string>.SuccessResponse(null!, "Güncellendi"))
        : Results.NotFound(ApiResponse<string>.FailResponse("Bulunamadı"));
});

app.MapDelete("/minimal/kullanicilar/{id:int}", async (int id, IKullaniciService s) =>
{
    var ok = await s.DeleteAsync(id);
    return ok ? Results.NoContent() : Results.NotFound();
});


app.MapGet("/minimal/kitaplar", async (IKitapService s) =>
    Results.Ok(ApiResponse<object>.SuccessResponse(await s.GetAllAsync()))
);

app.MapPost("/minimal/kitaplar", async (KitapCreateDto dto, IKitapService s) =>
    Results.Created("", ApiResponse<object>.SuccessResponse(await s.CreateAsync(dto)))
);



app.MapGet("/minimal/siparisler", async (ISiparisService s) =>
    Results.Ok(ApiResponse<object>.SuccessResponse(await s.GetAllAsync()))
);

app.MapPost("/minimal/siparisler", async (SiparisCreateDto dto, ISiparisService s) =>
    Results.Created("", ApiResponse<object>.SuccessResponse(await s.CreateAsync(dto)))
);

app.MapPut("/minimal/siparisler/{id:int}", async (int id, SiparisUpdateDto dto, ISiparisService s) =>
{
    var ok = await s.UpdateAsync(id, dto);
    return ok
        ? Results.Ok(ApiResponse<string>.SuccessResponse(null!, "Güncellendi"))
        : Results.NotFound();
});

app.MapDelete("/minimal/siparisler/{id:int}", async (int id, ISiparisService s) =>
{
    var ok = await s.DeleteAsync(id);
    return ok ? Results.NoContent() : Results.NotFound();
});



app.MapGet("/minimal/yorumlar/kitap/{kitapId:int}", async (int kitapId, IYorumService s) =>
    Results.Ok(ApiResponse<object>.SuccessResponse(await s.GetByKitapIdAsync(kitapId)))
);

app.MapPost("/minimal/yorumlar", async (YorumCreateDto dto, IYorumService s) =>
    Results.Created("", ApiResponse<object>.SuccessResponse(await s.CreateAsync(dto)))
);



app.MapControllers();

app.Run();
