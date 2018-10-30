using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class addblock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "blockCode",
                table: "Set",
                newName: "blockid");

            migrationBuilder.AlterColumn<string>(
                name: "blockid",
                table: "Set",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 10, nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Set_blockid",
                table: "Set",
                column: "blockid");

            migrationBuilder.AddForeignKey(
                name: "FK_Set_Block_blockid",
                table: "Set",
                column: "blockid",
                principalTable: "Block",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Set_Block_blockid",
                table: "Set");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropIndex(
                name: "IX_Set_blockid",
                table: "Set");

            migrationBuilder.RenameColumn(
                name: "blockid",
                table: "Set",
                newName: "blockCode");

            migrationBuilder.AlterColumn<string>(
                name: "blockCode",
                table: "Set",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
