using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class Fixcosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "manaCost",
                table: "CardFaces");

            migrationBuilder.AddColumn<int>(
                name: "manaCostid",
                table: "CardFaces",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardFaces_manaCostid",
                table: "CardFaces",
                column: "manaCostid");

            migrationBuilder.AddForeignKey(
                name: "FK_CardFaces_Costs_manaCostid",
                table: "CardFaces",
                column: "manaCostid",
                principalTable: "Costs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardFaces_Costs_manaCostid",
                table: "CardFaces");

            migrationBuilder.DropIndex(
                name: "IX_CardFaces_manaCostid",
                table: "CardFaces");

            migrationBuilder.DropColumn(
                name: "manaCostid",
                table: "CardFaces");

            migrationBuilder.AddColumn<string>(
                name: "manaCost",
                table: "CardFaces",
                nullable: true);
        }
    }
}
