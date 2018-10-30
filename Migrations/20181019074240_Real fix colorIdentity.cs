using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class RealfixcolorIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorIdentity_ColorIdentity_ColorIdentityid",
                table: "ColorIdentity");

            migrationBuilder.DropIndex(
                name: "IX_ColorIdentity_ColorIdentityid",
                table: "ColorIdentity");

            migrationBuilder.DropColumn(
                name: "ColorIdentityid",
                table: "ColorIdentity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorIdentityid",
                table: "ColorIdentity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ColorIdentity_ColorIdentityid",
                table: "ColorIdentity",
                column: "ColorIdentityid");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorIdentity_ColorIdentity_ColorIdentityid",
                table: "ColorIdentity",
                column: "ColorIdentityid",
                principalTable: "ColorIdentity",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
