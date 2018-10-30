using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class addlegalitieslink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "colorsid",
                table: "Card",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "legalitiesid",
                table: "Card",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Card_colorsid",
                table: "Card",
                column: "colorsid");

            migrationBuilder.CreateIndex(
                name: "IX_Card_legalitiesid",
                table: "Card",
                column: "legalitiesid");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Colors_colorsid",
                table: "Card",
                column: "colorsid",
                principalTable: "Colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Legalitie_legalitiesid",
                table: "Card",
                column: "legalitiesid",
                principalTable: "Legalitie",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Colors_colorsid",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Legalitie_legalitiesid",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_colorsid",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_legalitiesid",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "colorsid",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "legalitiesid",
                table: "Card");
        }
    }
}
