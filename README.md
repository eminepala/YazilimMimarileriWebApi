YazilimMimarileri Web API
Proje Açıklaması:
Bu proje, .NET 9 kullanılarak geliştirilmiş bir RESTful Web API uygulamasıdır.
Amaç; katmanlı mimari prensiplerine uygun, güvenli, genişletilebilir ve bakımı kolay bir API tasarlamaktır.

Uygulama, Minimal API ve Controller tabanlı yaklaşımları birlikte kullanarak CRUD işlemlerini gerçekleştirmektedir.
Entity Framework Core ile veritabanı işlemleri yürütülmüş, JWT tabanlı kimlik doğrulama ve rol bazlı yetkilendirme uygulanmıştır.


Kullanılan Teknolojiler:
-.NET 9
-ASP.NET Core Web API
-Entity Framework Core
-SQLite
-JWT (JSON Web Token)
-Swagger / OpenAPI
-Dependency Injection
-Global Exception Handling

Mimari Yapı
Proje Katmanlı Mimari (Layered Architecture) prensibine göre tasarlanmıştır.

Katmanlar

Controllers:
HTTP isteklerini karşılar
Yetkilendirme ve yönlendirme işlemlerini yapar

Services:
İş kurallarını içerir
Controller ile Data katmanı arasında köprü görevi görür

Data
Entity Framework Core DbContext
Migration ve Seed Data işlemleri

Models:
Veritabanı entity tanımları

DTOs:
Create, Update ve Response DTO’ları
API doğrudan entity döndürmez

Entity Yapısı ve İlişkiler
Entity’ler

Kullanici
Kitap
Siparis
SiparisDetay
Yorum

İlişkiler
Kullanici -> Siparis (1-N)
Siparis -> SiparisDetay (1-N)
Kitap -> SiparisDetay (1-N)
Kitap -> Yorum (1-N)

Tüm entity’lerde:
CreatedAt
UpdatedAt
IsDeleted (Soft Delete)

Soft Delete:
Fiziksel silme yerine Soft Delete uygulanmıştır.
IsDeleted alanı kullanılır
Global Query Filter ile silinen kayıtlar otomatik olarak gizlenir

Authentication ve Authorization
JWT Authentication
Kullanıcı email bilgisi ile giriş yapar
Başarılı girişte JWT token üretilir
Token Swagger üzerinden authorize edilerek kullanılır
Role Based Authorization
Admin / User rol ayrımı yapılmıştır
[Authorize(Roles = "Admin")] attribute’u ile endpoint koruması sağlanmıştır

Seed Data
Uygulama ilk çalıştırıldığında otomatik olarak örnek veriler oluşturulur.

Seed edilen veriler:
1 adet Kullanici
1 adet Kitap
1 adet Siparis
1 adet SiparisDetay
1 adet Yorum
Seed işlemleri migration uyumlu ve deterministik tarih ile yapılmıştır.

Standart API Response Formatı:
Tüm API cevapları aşağıdaki standart formatta döner:
{
"success": true,
"message": "Bilgi veya hata mesajı",
"data": {}
}

Endpoint Listesi:
Auth
-POST /api/auth/login
-GET /api/auth/admin-only

Kullanici (Minimal API)
-GET /minimal/kullanicilar
-GET /minimal/kullanicilar/{id}
-POST /minimal/kullanicilar
-PUT /minimal/kullanicilar/{id}
-DELETE /minimal/kullanicilar/{id}

Kitap (Minimal API)
-GET /minimal/kitaplar
-POST /minimal/kitaplar

Siparis (Minimal API)
-GET /minimal/siparisler
-POST /minimal/siparisler
-PUT /minimal/siparisler/{id}
-DELETE /minimal/siparisler/{id}

Yorum (Minimal API)
-GET /minimal/yorumlar/kitap/{kitapId}
-POST /minimal/yorumlar

Swagger / OpenAPI
Swagger entegre edilmiştir
Tüm endpoint’ler Swagger UI üzerinden test edilebilir
JWT token Swagger Authorize alanı ile kullanılabilir

Swagger adresi:http://localhost:5000/swagger

Kurulum ve Çalıştırma 
Projeyi Klonla:git clone <repo-url>
Proje Dizini:cd YazilimMimarileri
Migration Oluştur:dotnet ef migrations add InitialCreate
Veritabanını Oluştur:dotnet ef database update
Uygulamayı Çalıştır:dotnet run

Logging ve Error Handling:
Global Exception Middleware kullanılmıştır
400, 401, 403, 404, 409 ve 500 HTTP status kodları doğru şekilde döndürülmektedir

Git Versiyon Kontrolü:
Proje geliştirme sürecinde düzenli commit atılmıştır
Anlamlı ve açıklayıcı commit mesajları kullanılmıştır.
