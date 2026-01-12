using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YazilimMimarileri.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kitaplar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: false),
                    Yazar = table.Column<string>(type: "TEXT", nullable: false),
                    Aciklama = table.Column<string>(type: "TEXT", nullable: false),
                    Fiyat = table.Column<float>(type: "REAL", nullable: false),
                    Stok = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitaplar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: false),
                    Soyad = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Yas = table.Column<int>(type: "INTEGER", nullable: false),
                    Adres = table.Column<string>(type: "TEXT", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "TEXT", nullable: false),
                    SifreHash = table.Column<string>(type: "TEXT", nullable: false),
                    Rol = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Yorumlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KitapId = table.Column<int>(type: "INTEGER", nullable: false),
                    Icerik = table.Column<string>(type: "TEXT", nullable: false),
                    Puan = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yorumlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yorumlar_Kitaplar_KitapId",
                        column: x => x.KitapId,
                        principalTable: "Kitaplar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Siparisler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KullaniciId = table.Column<int>(type: "INTEGER", nullable: false),
                    SiparisTarihi = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    OdemeYontemi = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparisler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Siparisler_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiparisDetaylari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SiparisId = table.Column<int>(type: "INTEGER", nullable: false),
                    KitapId = table.Column<int>(type: "INTEGER", nullable: false),
                    Adet = table.Column<int>(type: "INTEGER", nullable: false),
                    BirimFiyat = table.Column<float>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiparisDetaylari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiparisDetaylari_Kitaplar_KitapId",
                        column: x => x.KitapId,
                        principalTable: "Kitaplar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiparisDetaylari_Siparisler_SiparisId",
                        column: x => x.SiparisId,
                        principalTable: "Siparisler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kitaplar",
                columns: new[] { "Id", "Aciklama", "Ad", "CreatedAt", "Fiyat", "IsDeleted", "Stok", "UpdatedAt", "Yazar" },
                values: new object[] { 1, "Küçük Prens, Antoine de Saint-Exupéry tarafından yazılmış klasik bir eserdir.", "Küçük Prens", new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 105f, false, 10, null, "Antoine de Saint-Exupéry" });

            migrationBuilder.InsertData(
                table: "Kullanicilar",
                columns: new[] { "Id", "Ad", "Adres", "CreatedAt", "Email", "IsDeleted", "KullaniciAdi", "Rol", "SifreHash", "Soyad", "UpdatedAt", "Yas" },
                values: new object[] { 1, "Emine", "Denizli", new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "emine@test.com", false, "emine", "User", "SEED_HASH", "PALA", null, 21 });

            migrationBuilder.InsertData(
                table: "Siparisler",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "KullaniciId", "OdemeYontemi", "SiparisTarihi", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null });

            migrationBuilder.InsertData(
                table: "Yorumlar",
                columns: new[] { "Id", "CreatedAt", "Icerik", "IsDeleted", "KitapId", "Puan", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Her yaştaki kişi okumalı", false, 1, 5, null });

            migrationBuilder.InsertData(
                table: "SiparisDetaylari",
                columns: new[] { "Id", "Adet", "BirimFiyat", "CreatedAt", "IsDeleted", "KitapId", "SiparisId", "UpdatedAt" },
                values: new object[] { 1, 1, 105f, new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetaylari_KitapId",
                table: "SiparisDetaylari",
                column: "KitapId");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetaylari_SiparisId",
                table: "SiparisDetaylari",
                column: "SiparisId");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_KullaniciId",
                table: "Siparisler",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_KitapId",
                table: "Yorumlar",
                column: "KitapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiparisDetaylari");

            migrationBuilder.DropTable(
                name: "Yorumlar");

            migrationBuilder.DropTable(
                name: "Siparisler");

            migrationBuilder.DropTable(
                name: "Kitaplar");

            migrationBuilder.DropTable(
                name: "Kullanicilar");
        }
    }
}
