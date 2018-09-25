using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class ChangedMinunitToMinUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Minunit",
                table: "Product",
                newName: "MinUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinUnit",
                table: "Product",
                newName: "Minunit");
        }
    }
}
