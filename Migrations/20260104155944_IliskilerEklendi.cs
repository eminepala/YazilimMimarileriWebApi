using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YazilimMimarileri.Migrations
{
    /// <inheritdoc />
    public partial class IliskilerEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KitapId1",
                table: "Yorumlar",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KullaniciId1",
                table: "Siparisler",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KitapId1",
                table: "SiparisDetaylari",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SiparisId1",
                table: "SiparisDetaylari",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_KitapId1",
                table: "Yorumlar",
                column: "KitapId1");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_KullaniciId1",
                table: "Siparisler",
                column: "KullaniciId1");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetaylari_KitapId1",
                table: "SiparisDetaylari",
                column: "KitapId1");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetaylari_SiparisId1",
                table: "SiparisDetaylari",
                column: "SiparisId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SiparisDetaylari_Kitaplar_KitapId1",
                table: "SiparisDetaylari",
                column: "KitapId1",
                principalTable: "Kitaplar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiparisDetaylari_Siparisler_SiparisId1",
                table: "SiparisDetaylari",
                column: "SiparisId1",
                principalTable: "Siparisler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Siparisler_Kullanicilar_KullaniciId1",
                table: "Siparisler",
                column: "KullaniciId1",
                principalTable: "Kullanicilar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Yorumlar_Kitaplar_KitapId1",
                table: "Yorumlar",
                column: "KitapId1",
                principalTable: "Kitaplar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiparisDetaylari_Kitaplar_KitapId1",
                table: "SiparisDetaylari");

            migrationBuilder.DropForeignKey(
                name: "FK_SiparisDetaylari_Siparisler_SiparisId1",
                table: "SiparisDetaylari");

            migrationBuilder.DropForeignKey(
                name: "FK_Siparisler_Kullanicilar_KullaniciId1",
                table: "Siparisler");

            migrationBuilder.DropForeignKey(
                name: "FK_Yorumlar_Kitaplar_KitapId1",
                table: "Yorumlar");

            migrationBuilder.DropIndex(
                name: "IX_Yorumlar_KitapId1",
                table: "Yorumlar");

            migrationBuilder.DropIndex(
                name: "IX_Siparisler_KullaniciId1",
                table: "Siparisler");

            migrationBuilder.DropIndex(
                name: "IX_SiparisDetaylari_KitapId1",
                table: "SiparisDetaylari");

            migrationBuilder.DropIndex(
                name: "IX_SiparisDetaylari_SiparisId1",
                table: "SiparisDetaylari");

            migrationBuilder.DropColumn(
                name: "KitapId1",
                table: "Yorumlar");

            migrationBuilder.DropColumn(
                name: "KullaniciId1",
                table: "Siparisler");

            migrationBuilder.DropColumn(
                name: "KitapId1",
                table: "SiparisDetaylari");

            migrationBuilder.DropColumn(
                name: "SiparisId1",
                table: "SiparisDetaylari");
        }
    }
}
