using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class lastadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "setId",
                table: "Print",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "partTwoId",
                table: "Parts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Print_setId",
                table: "Print",
                column: "setId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_partTwoId",
                table: "Parts",
                column: "partTwoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Card_partTwoId",
                table: "Parts",
                column: "partTwoId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Print_Set_setId",
                table: "Print",
                column: "setId",
                principalTable: "Set",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_Card_partTwoId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_Print_Set_setId",
                table: "Print");

            migrationBuilder.DropIndex(
                name: "IX_Print_setId",
                table: "Print");

            migrationBuilder.DropIndex(
                name: "IX_Parts_partTwoId",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "setId",
                table: "Print");

            migrationBuilder.DropColumn(
                name: "partTwoId",
                table: "Parts");
        }
    }
}
