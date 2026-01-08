using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YazilimMimarileri.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Yorumlar",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Siparisler",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SiparisDetaylari",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Kullanicilar",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Kitaplar",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Yorumlar");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Siparisler");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SiparisDetaylari");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Kitaplar");
        }
    }
}
