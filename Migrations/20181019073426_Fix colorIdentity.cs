using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class FixcolorIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorsInIndicator_ColorIdentity_identityid",
                table: "ColorsInIndicator");

            migrationBuilder.DropIndex(
                name: "IX_ColorsInIndicator_identityid",
                table: "ColorsInIndicator");

            migrationBuilder.DropColumn(
                name: "identityid",
                table: "ColorsInIndicator");

            migrationBuilder.AddColumn<int>(
                name: "ColorsInIndicatorid",
                table: "ColorsInIdentity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ColorsInIdentity_ColorsInIndicatorid",
                table: "ColorsInIdentity",
                column: "ColorsInIndicatorid");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorsInIdentity_ColorsInIndicator_ColorsInIndicatorid",
                table: "ColorsInIdentity",
                column: "ColorsInIndicatorid",
                principalTable: "ColorsInIndicator",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorsInIdentity_ColorsInIndicator_ColorsInIndicatorid",
                table: "ColorsInIdentity");

            migrationBuilder.DropIndex(
                name: "IX_ColorsInIdentity_ColorsInIndicatorid",
                table: "ColorsInIdentity");

            migrationBuilder.DropColumn(
                name: "ColorsInIndicatorid",
                table: "ColorsInIdentity");

            migrationBuilder.AddColumn<int>(
                name: "identityid",
                table: "ColorsInIndicator",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ColorsInIndicator_identityid",
                table: "ColorsInIndicator",
                column: "identityid");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorsInIndicator_ColorIdentity_identityid",
                table: "ColorsInIndicator",
                column: "identityid",
                principalTable: "ColorIdentity",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
