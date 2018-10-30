using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class addcardInSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardsInSets",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cardId = table.Column<string>(nullable: true),
                    setId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardsInSets", x => x.id);
                    table.ForeignKey(
                        name: "FK_CardsInSets_Card_cardId",
                        column: x => x.cardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardsInSets_Set_setId",
                        column: x => x.setId,
                        principalTable: "Set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardsInSets_cardId",
                table: "CardsInSets",
                column: "cardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardsInSets_setId",
                table: "CardsInSets",
                column: "setId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardsInSets");
        }
    }
}
