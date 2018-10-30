using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class addstuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "printId",
                table: "ImagesUrl",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagesUrl_printId",
                table: "ImagesUrl",
                column: "printId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesUrl_Print_printId",
                table: "ImagesUrl",
                column: "printId",
                principalTable: "Print",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagesUrl_Print_printId",
                table: "ImagesUrl");

            migrationBuilder.DropIndex(
                name: "IX_ImagesUrl_printId",
                table: "ImagesUrl");

            migrationBuilder.DropColumn(
                name: "printId",
                table: "ImagesUrl");
        }
    }
}
