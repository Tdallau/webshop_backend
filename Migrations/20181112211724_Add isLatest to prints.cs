using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class AddisLatesttoprints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isLatest",
                table: "Print",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isLatest",
                table: "Print");
        }
    }
}
