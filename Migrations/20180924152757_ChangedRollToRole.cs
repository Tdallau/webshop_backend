using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class ChangedRollToRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Roll",
                table: "User",
                newName: "Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "User",
                newName: "Roll");
        }
    }
}
