using Microsoft.EntityFrameworkCore;
using YazilimMimarileri.Data;
using YazilimMimarileri.Services;
using YazilimMimarileri.Services.Interfaces;
using System.Text.Json.Serialization;
using YazilimMimarileri.Common;
using YazilimMimarileri.DTOs.Kullanici;
using YazilimMimarileri.DTOs.Siparis;
using YazilimMimarileri.DTOs.Yorum;
using YazilimMimarileri.DTOs.Kitap;


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

app.MapGet("/minimal/siparisler", async (ISiparisService service) =>
{
    var data = await service.GetAllAsync();

    return Results.Ok(
        ApiResponse<object>.SuccessResponse(data, "Siparişler listelendi")
    );
});

app.MapGet("/minimal/kitaplar", async (IKitapService service) =>
{
    var data = await service.GetAllAsync();

    return Results.Ok(
        ApiResponse<object>.SuccessResponse(data, "Kitaplar listelendi")
    );
});
app.MapGet("/minimal/kitaplar/{id:int}", async (int id, IKitapService service) =>
{
    var data = await service.GetByIdAsync(id);

    if (data == null)
        return Results.NotFound(
            ApiResponse<string>.FailResponse("Kitap bulunamadı")
        );

    return Results.Ok(
        ApiResponse<object>.SuccessResponse(data, "Kitap getirildi")
    );
});
app.MapPost("/minimal/kitaplar", async (KitapCreateDto dto, IKitapService service) =>
{
    var created = await service.CreateAsync(dto);

    return Results.Created(
        $"/minimal/kitaplar/{created.Id}",
        ApiResponse<object>.SuccessResponse(created, "Kitap oluşturuldu")
    );
});
app.MapPut("/minimal/kitaplar/{id:int}", async (
    int id,
    KitapUpdateDto dto,
    IKitapService service) =>
{
    var updated = await service.UpdateAsync(id, dto);

    if (!updated)
        return Results.NotFound(
            ApiResponse<string>.FailResponse("Kitap bulunamadı")
        );

    return Results.Ok(
        ApiResponse<string>.SuccessResponse(null!, "Kitap güncellendi")
    );
});
app.MapDelete("/minimal/kitaplar/{id:int}", async (int id, IKitapService service) =>
{
    var deleted = await service.DeleteAsync(id);

    if (!deleted)
        return Results.NotFound(
            ApiResponse<string>.FailResponse("Kitap bulunamadı")
        );

    return Results.NoContent();
});



// POST /minimal/siparisler
app.MapPost("/minimal/siparisler", async (
    SiparisCreateDto dto,
    ISiparisService service) =>
{
    var created = await service.CreateAsync(dto);

    return Results.Created(
        $"/minimal/siparisler/{created.Id}",
        ApiResponse<object>.SuccessResponse(created, "Sipariş oluşturuldu")
    );
});


// PUT /minimal/siparisler/{id}
app.MapPut("/minimal/siparisler/{id:int}", async (
    int id,
    SiparisUpdateDto dto,
    ISiparisService service) =>
{
    var updated = await service.UpdateAsync(id, dto);

    if (!updated)
    {
        return Results.NotFound(
            ApiResponse<string>.FailResponse("Sipariş bulunamadı")
        );
    }

    return Results.Ok(
        ApiResponse<string>.SuccessResponse(null!, "Sipariş güncellendi")
    );
});


// DELETE /minimal/siparisler/{id}
app.MapDelete("/minimal/siparisler/{id:int}", async (
    int id,
    ISiparisService service) =>
{
    var deleted = await service.DeleteAsync(id);

    if (!deleted)
    {
        return Results.NotFound(
            ApiResponse<string>.FailResponse("Sipariş bulunamadı")
        );
    }

    return Results.NoContent();
});

// GET /minimal/yorumlar/kitap/{kitapId}
app.MapGet("/minimal/yorumlar/kitap/{kitapId:int}", async (
    int kitapId,
    IYorumService service) =>
{
    var data = await service.GetByKitapIdAsync(kitapId);

    return Results.Ok(
        ApiResponse<object>.SuccessResponse(data, "Yorumlar listelendi")
    );
});


// POST /minimal/yorumlar
app.MapPost("/minimal/yorumlar", async (
    YorumCreateDto dto,
    IYorumService service) =>
{
    if (dto.Puan < 1 || dto.Puan > 5)
    {
        return Results.BadRequest(
            ApiResponse<string>.FailResponse("Puan 1 ile 5 arasında olmalıdır")
        );
    }

    var created = await service.CreateAsync(dto);

    return Results.Created(
        $"/minimal/yorumlar/{created.Id}",
        ApiResponse<object>.SuccessResponse(created, "Yorum eklendi")
    );
});


// PUT /minimal/yorumlar/{id}
app.MapPut("/minimal/yorumlar/{id:int}", async (
    int id,
    YorumUpdateDto dto,
    IYorumService service) =>
{
    if (dto.Puan < 1 || dto.Puan > 5)
    {
        return Results.BadRequest(
            ApiResponse<string>.FailResponse("Puan 1 ile 5 arasında olmalıdır")
        );
    }

    var updated = await service.UpdateAsync(id, dto);

    if (!updated)
    {
        return Results.NotFound(
            ApiResponse<string>.FailResponse("Yorum bulunamadı")
        );
    }

    return Results.Ok(
        ApiResponse<string>.SuccessResponse(null!, "Yorum güncellendi")
    );
});


// DELETE /minimal/yorumlar/{id}
app.MapDelete("/minimal/yorumlar/{id:int}", async (
    int id,
    IYorumService service) =>
{
    var deleted = await service.DeleteAsync(id);

    if (!deleted)
    {
        return Results.NotFound(
            ApiResponse<string>.FailResponse("Yorum bulunamadı")
        );
    }

    return Results.NoContent();
});

app.MapControllers();

app.MapGet("/minimal/kullanicilar", async (IKullaniciService service) =>
{
    var data = await service.GetAllAsync();
    return Results.Ok(
        ApiResponse<object>.SuccessResponse(data, "Kullanıcılar listelendi")
    );
});

app.MapGet("/minimal/kullanicilar/{id:int}", async (int id, IKullaniciService service) =>
{
    var data = await service.GetByIdAsync(id);

    if (data == null)
        return Results.NotFound(
            ApiResponse<string>.FailResponse("Kullanıcı bulunamadı")
        );

    return Results.Ok(
        ApiResponse<object>.SuccessResponse(data, "Kullanıcı getirildi")
    );
});

app.MapPost("/minimal/kullanicilar", async (KullaniciCreateDto dto, IKullaniciService service) =>
{
    var created = await service.CreateAsync(dto);

    return Results.Created(
        $"/minimal/kullanicilar/{created.Id}",
        ApiResponse<object>.SuccessResponse(created, "Kullanıcı oluşturuldu")
    );
});

app.MapPut("/minimal/kullanicilar/{id:int}", async (int id, KullaniciUpdateDto dto, IKullaniciService service) =>
{
    var updated = await service.UpdateAsync(id, dto);

    if (!updated)
        return Results.NotFound(
            ApiResponse<string>.FailResponse("Kullanıcı bulunamadı")
        );

    return Results.Ok(
        ApiResponse<string>.SuccessResponse(null!, "Kullanıcı güncellendi")
    );
});

app.MapDelete("/minimal/kullanicilar/{id:int}", async (int id, IKullaniciService service) =>
{
    var deleted = await service.DeleteAsync(id);

    if (!deleted)
        return Results.NotFound(
            ApiResponse<string>.FailResponse("Kullanıcı bulunamadı")
        );

    return Results.NoContent();
});

app.Run();