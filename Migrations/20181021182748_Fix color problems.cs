using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class Fixcolorproblems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_ColorIdentity_colorIdentityid",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_CardFaces_ColocolorIndicator_colorIndicatorid",
                table: "CardFaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Color_CostSymbols_symbolid",
                table: "Color");

            migrationBuilder.DropTable(
                name: "ColorsInIdentity");

            migrationBuilder.DropTable(
                name: "ColorsInIndicator");

            migrationBuilder.DropTable(
                name: "ColorIdentity");

            migrationBuilder.DropTable(
                name: "ColocolorIndicator");

            migrationBuilder.DropIndex(
                name: "IX_Color_symbolid",
                table: "Color");

            migrationBuilder.DropColumn(
                name: "symbolid",
                table: "Color");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Card");

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "Print",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "symbol",
                table: "Color",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "colorid",
                table: "CardFaces",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EdhrecRank",
                table: "Card",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "ColorCombinations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorCombinations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ColorsInCombinations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    colorId = table.Column<int>(nullable: true),
                    combinationid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorsInCombinations", x => x.id);
                    table.ForeignKey(
                        name: "FK_ColorsInCombinations_Color_colorId",
                        column: x => x.colorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ColorsInCombinations_ColorCombinations_combinationid",
                        column: x => x.combinationid,
                        principalTable: "ColorCombinations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardFaces_colorid",
                table: "CardFaces",
                column: "colorid");

            migrationBuilder.CreateIndex(
                name: "IX_ColorsInCombinations_colorId",
                table: "ColorsInCombinations",
                column: "colorId");

            migrationBuilder.CreateIndex(
                name: "IX_ColorsInCombinations_combinationid",
                table: "ColorsInCombinations",
                column: "combinationid");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_ColorCombinations_colorIdentityid",
                table: "Card",
                column: "colorIdentityid",
                principalTable: "ColorCombinations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardFaces_ColorCombinations_colorIndicatorid",
                table: "CardFaces",
                column: "colorIndicatorid",
                principalTable: "ColorCombinations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardFaces_ColorCombinations_colorid",
                table: "CardFaces",
                column: "colorid",
                principalTable: "ColorCombinations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_ColorCombinations_colorIdentityid",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_CardFaces_ColorCombinations_colorIndicatorid",
                table: "CardFaces");

            migrationBuilder.DropForeignKey(
                name: "FK_CardFaces_ColorCombinations_colorid",
                table: "CardFaces");

            migrationBuilder.DropTable(
                name: "ColorsInCombinations");

            migrationBuilder.DropTable(
                name: "ColorCombinations");

            migrationBuilder.DropIndex(
                name: "IX_CardFaces_colorid",
                table: "CardFaces");

            migrationBuilder.DropColumn(
                name: "symbol",
                table: "Color");

            migrationBuilder.DropColumn(
                name: "colorid",
                table: "CardFaces");

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "Print",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "symbolid",
                table: "Color",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EdhrecRank",
                table: "Card",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "Card",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ColocolorIndicator",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColocolorIndicator", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ColorIdentity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorIdentity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ColorsInIndicator",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ColorIndicatorid = table.Column<int>(nullable: true),
                    colorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorsInIndicator", x => x.id);
                    table.ForeignKey(
                        name: "FK_ColorsInIndicator_ColocolorIndicator_ColorIndicatorid",
                        column: x => x.ColorIndicatorid,
                        principalTable: "ColocolorIndicator",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ColorsInIndicator_Color_colorId",
                        column: x => x.colorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ColorsInIdentity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ColorsInIndicatorid = table.Column<int>(nullable: true),
                    colorId = table.Column<int>(nullable: true),
                    identityid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorsInIdentity", x => x.id);
                    table.ForeignKey(
                        name: "FK_ColorsInIdentity_ColorsInIndicator_ColorsInIndicatorid",
                        column: x => x.ColorsInIndicatorid,
                        principalTable: "ColorsInIndicator",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ColorsInIdentity_Color_colorId",
                        column: x => x.colorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ColorsInIdentity_ColorIdentity_identityid",
                        column: x => x.identityid,
                        principalTable: "ColorIdentity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Color_symbolid",
                table: "Color",
                column: "symbolid");

            migrationBuilder.CreateIndex(
                name: "IX_ColorsInIdentity_ColorsInIndicatorid",
                table: "ColorsInIdentity",
                column: "ColorsInIndicatorid");

            migrationBuilder.CreateIndex(
                name: "IX_ColorsInIdentity_colorId",
                table: "ColorsInIdentity",
                column: "colorId");

            migrationBuilder.CreateIndex(
                name: "IX_ColorsInIdentity_identityid",
                table: "ColorsInIdentity",
                column: "identityid");

            migrationBuilder.CreateIndex(
                name: "IX_ColorsInIndicator_ColorIndicatorid",
                table: "ColorsInIndicator",
                column: "ColorIndicatorid");

            migrationBuilder.CreateIndex(
                name: "IX_ColorsInIndicator_colorId",
                table: "ColorsInIndicator",
                column: "colorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_ColorIdentity_colorIdentityid",
                table: "Card",
                column: "colorIdentityid",
                principalTable: "ColorIdentity",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardFaces_ColocolorIndicator_colorIndicatorid",
                table: "CardFaces",
                column: "colorIndicatorid",
                principalTable: "ColocolorIndicator",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Color_CostSymbols_symbolid",
                table: "Color",
                column: "symbolid",
                principalTable: "CostSymbols",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
