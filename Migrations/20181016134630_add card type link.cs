using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class addcardtypelink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardFaceid",
                table: "Types",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Types_CardFaceid",
                table: "Types",
                column: "CardFaceid");

            migrationBuilder.AddForeignKey(
                name: "FK_Types_CardFaces_CardFaceid",
                table: "Types",
                column: "CardFaceid",
                principalTable: "CardFaces",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Types_CardFaces_CardFaceid",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_CardFaceid",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "CardFaceid",
                table: "Types");
        }
    }
}
