using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class UpdatedImageUrlLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "printFaceid",
                table: "ImagesUrl",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagesUrl_printFaceid",
                table: "ImagesUrl",
                column: "printFaceid");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagesUrl_PrintFace_printFaceid",
                table: "ImagesUrl",
                column: "printFaceid",
                principalTable: "PrintFace",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagesUrl_PrintFace_printFaceid",
                table: "ImagesUrl");

            migrationBuilder.DropIndex(
                name: "IX_ImagesUrl_printFaceid",
                table: "ImagesUrl");

            migrationBuilder.DropColumn(
                name: "printFaceid",
                table: "ImagesUrl");

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
    }
}
