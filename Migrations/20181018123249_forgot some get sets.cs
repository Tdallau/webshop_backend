using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class forgotsomegetsets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "backgroundColor",
                table: "Legalities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Legalities",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "backgroundColor",
                table: "Legalities");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Legalities");
        }
    }
}
