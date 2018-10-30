using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class betterCostStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "symbol",
                table: "Color");

            migrationBuilder.AddColumn<int>(
                name: "symbolid",
                table: "Color",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Costs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CostSymbols",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    strSymbol = table.Column<string>(nullable: true),
                    picturePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostSymbols", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SymbolsInCosts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    symbolid = table.Column<int>(nullable: true),
                    costid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymbolsInCosts", x => x.id);
                    table.ForeignKey(
                        name: "FK_SymbolsInCosts_Costs_costid",
                        column: x => x.costid,
                        principalTable: "Costs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SymbolsInCosts_CostSymbols_symbolid",
                        column: x => x.symbolid,
                        principalTable: "CostSymbols",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Color_symbolid",
                table: "Color",
                column: "symbolid");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolsInCosts_costid",
                table: "SymbolsInCosts",
                column: "costid");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolsInCosts_symbolid",
                table: "SymbolsInCosts",
                column: "symbolid");

            migrationBuilder.AddForeignKey(
                name: "FK_Color_CostSymbols_symbolid",
                table: "Color",
                column: "symbolid",
                principalTable: "CostSymbols",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Color_CostSymbols_symbolid",
                table: "Color");

            migrationBuilder.DropTable(
                name: "SymbolsInCosts");

            migrationBuilder.DropTable(
                name: "Costs");

            migrationBuilder.DropTable(
                name: "CostSymbols");

            migrationBuilder.DropIndex(
                name: "IX_Color_symbolid",
                table: "Color");

            migrationBuilder.DropColumn(
                name: "symbolid",
                table: "Color");

            migrationBuilder.AddColumn<string>(
                name: "symbol",
                table: "Color",
                nullable: true);
        }
    }
}
