using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class hopefullyfinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Colors_colorIdentityid",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Colors_colorsid",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_CardFaces_Colors_colorIndicatorid",
                table: "CardFaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Color_Colors_Colorsid",
                table: "Color");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_Color_Colorsid",
                table: "Color");

            migrationBuilder.DropIndex(
                name: "IX_Card_colorsid",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "brawl",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "commander",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "duel",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "frontier",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "future",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "legacy",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "modern",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "one_v_one",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "pauper",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "penny",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "standard",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "vintage",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "Colorsid",
                table: "Color");

            migrationBuilder.DropColumn(
                name: "colorsid",
                table: "Card");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Set",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "setId",
                table: "Print",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "brawlid",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "commanderid",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "duelid",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "frontierid",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "futureid",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "legacyid",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "modernid",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "one_v_oneid",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pauperid",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pennyid",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "standardid",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "vintageid",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColorIdentityid",
                table: "ColorIdentity",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "setId",
                table: "CardsInSets",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ColorsInIdentity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    colorId = table.Column<int>(nullable: true),
                    identityid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorsInIdentity", x => x.id);
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

            migrationBuilder.CreateTable(
                name: "ColorsInIndicator",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    colorId = table.Column<int>(nullable: true),
                    identityid = table.Column<int>(nullable: true),
                    ColorIndicatorid = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_ColorsInIndicator_ColorIdentity_identityid",
                        column: x => x.identityid,
                        principalTable: "ColorIdentity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Legalities",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legalities", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_brawlid",
                table: "Legalitie",
                column: "brawlid");

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_commanderid",
                table: "Legalitie",
                column: "commanderid");

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_duelid",
                table: "Legalitie",
                column: "duelid");

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_frontierid",
                table: "Legalitie",
                column: "frontierid");

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_futureid",
                table: "Legalitie",
                column: "futureid");

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_legacyid",
                table: "Legalitie",
                column: "legacyid");

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_modernid",
                table: "Legalitie",
                column: "modernid");

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_one_v_oneid",
                table: "Legalitie",
                column: "one_v_oneid");

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_pauperid",
                table: "Legalitie",
                column: "pauperid");

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_pennyid",
                table: "Legalitie",
                column: "pennyid");

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_standardid",
                table: "Legalitie",
                column: "standardid");

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_vintageid",
                table: "Legalitie",
                column: "vintageid");

            migrationBuilder.CreateIndex(
                name: "IX_ColorIdentity_ColorIdentityid",
                table: "ColorIdentity",
                column: "ColorIdentityid");

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

            migrationBuilder.CreateIndex(
                name: "IX_ColorsInIndicator_identityid",
                table: "ColorsInIndicator",
                column: "identityid");

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
                name: "FK_ColorIdentity_ColorIdentity_ColorIdentityid",
                table: "ColorIdentity",
                column: "ColorIdentityid",
                principalTable: "ColorIdentity",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Legalitie_Legalities_brawlid",
                table: "Legalitie",
                column: "brawlid",
                principalTable: "Legalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Legalitie_Legalities_commanderid",
                table: "Legalitie",
                column: "commanderid",
                principalTable: "Legalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Legalitie_Legalities_duelid",
                table: "Legalitie",
                column: "duelid",
                principalTable: "Legalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Legalitie_Legalities_frontierid",
                table: "Legalitie",
                column: "frontierid",
                principalTable: "Legalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Legalitie_Legalities_futureid",
                table: "Legalitie",
                column: "futureid",
                principalTable: "Legalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Legalitie_Legalities_legacyid",
                table: "Legalitie",
                column: "legacyid",
                principalTable: "Legalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Legalitie_Legalities_modernid",
                table: "Legalitie",
                column: "modernid",
                principalTable: "Legalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Legalitie_Legalities_one_v_oneid",
                table: "Legalitie",
                column: "one_v_oneid",
                principalTable: "Legalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Legalitie_Legalities_pauperid",
                table: "Legalitie",
                column: "pauperid",
                principalTable: "Legalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Legalitie_Legalities_pennyid",
                table: "Legalitie",
                column: "pennyid",
                principalTable: "Legalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Legalitie_Legalities_standardid",
                table: "Legalitie",
                column: "standardid",
                principalTable: "Legalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Legalitie_Legalities_vintageid",
                table: "Legalitie",
                column: "vintageid",
                principalTable: "Legalities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_ColorIdentity_colorIdentityid",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_CardFaces_ColocolorIndicator_colorIndicatorid",
                table: "CardFaces");

            migrationBuilder.DropForeignKey(
                name: "FK_ColorIdentity_ColorIdentity_ColorIdentityid",
                table: "ColorIdentity");

            migrationBuilder.DropForeignKey(
                name: "FK_Legalitie_Legalities_brawlid",
                table: "Legalitie");

            migrationBuilder.DropForeignKey(
                name: "FK_Legalitie_Legalities_commanderid",
                table: "Legalitie");

            migrationBuilder.DropForeignKey(
                name: "FK_Legalitie_Legalities_duelid",
                table: "Legalitie");

            migrationBuilder.DropForeignKey(
                name: "FK_Legalitie_Legalities_frontierid",
                table: "Legalitie");

            migrationBuilder.DropForeignKey(
                name: "FK_Legalitie_Legalities_futureid",
                table: "Legalitie");

            migrationBuilder.DropForeignKey(
                name: "FK_Legalitie_Legalities_legacyid",
                table: "Legalitie");

            migrationBuilder.DropForeignKey(
                name: "FK_Legalitie_Legalities_modernid",
                table: "Legalitie");

            migrationBuilder.DropForeignKey(
                name: "FK_Legalitie_Legalities_one_v_oneid",
                table: "Legalitie");

            migrationBuilder.DropForeignKey(
                name: "FK_Legalitie_Legalities_pauperid",
                table: "Legalitie");

            migrationBuilder.DropForeignKey(
                name: "FK_Legalitie_Legalities_pennyid",
                table: "Legalitie");

            migrationBuilder.DropForeignKey(
                name: "FK_Legalitie_Legalities_standardid",
                table: "Legalitie");

            migrationBuilder.DropForeignKey(
                name: "FK_Legalitie_Legalities_vintageid",
                table: "Legalitie");

            migrationBuilder.DropTable(
                name: "ColorsInIdentity");

            migrationBuilder.DropTable(
                name: "ColorsInIndicator");

            migrationBuilder.DropTable(
                name: "Legalities");

            migrationBuilder.DropIndex(
                name: "IX_Legalitie_brawlid",
                table: "Legalitie");

            migrationBuilder.DropIndex(
                name: "IX_Legalitie_commanderid",
                table: "Legalitie");

            migrationBuilder.DropIndex(
                name: "IX_Legalitie_duelid",
                table: "Legalitie");

            migrationBuilder.DropIndex(
                name: "IX_Legalitie_frontierid",
                table: "Legalitie");

            migrationBuilder.DropIndex(
                name: "IX_Legalitie_futureid",
                table: "Legalitie");

            migrationBuilder.DropIndex(
                name: "IX_Legalitie_legacyid",
                table: "Legalitie");

            migrationBuilder.DropIndex(
                name: "IX_Legalitie_modernid",
                table: "Legalitie");

            migrationBuilder.DropIndex(
                name: "IX_Legalitie_one_v_oneid",
                table: "Legalitie");

            migrationBuilder.DropIndex(
                name: "IX_Legalitie_pauperid",
                table: "Legalitie");

            migrationBuilder.DropIndex(
                name: "IX_Legalitie_pennyid",
                table: "Legalitie");

            migrationBuilder.DropIndex(
                name: "IX_Legalitie_standardid",
                table: "Legalitie");

            migrationBuilder.DropIndex(
                name: "IX_Legalitie_vintageid",
                table: "Legalitie");

            migrationBuilder.DropIndex(
                name: "IX_ColorIdentity_ColorIdentityid",
                table: "ColorIdentity");

            migrationBuilder.DropColumn(
                name: "brawlid",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "commanderid",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "duelid",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "frontierid",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "futureid",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "legacyid",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "modernid",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "one_v_oneid",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "pauperid",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "pennyid",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "standardid",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "vintageid",
                table: "Legalitie");

            migrationBuilder.DropColumn(
                name: "ColorIdentityid",
                table: "ColorIdentity");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Set",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<string>(
                name: "setId",
                table: "Print",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "brawl",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "commander",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "duel",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "frontier",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "future",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "legacy",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "modern",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "one_v_one",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pauper",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "penny",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "standard",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "vintage",
                table: "Legalitie",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Colorsid",
                table: "Color",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "setId",
                table: "CardsInSets",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "colorsid",
                table: "Card",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ColorIdentityid = table.Column<int>(nullable: true),
                    ColorIndicatorid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.id);
                    table.ForeignKey(
                        name: "FK_Colors_ColorIdentity_ColorIdentityid",
                        column: x => x.ColorIdentityid,
                        principalTable: "ColorIdentity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Colors_ColocolorIndicator_ColorIndicatorid",
                        column: x => x.ColorIndicatorid,
                        principalTable: "ColocolorIndicator",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Color_Colorsid",
                table: "Color",
                column: "Colorsid");

            migrationBuilder.CreateIndex(
                name: "IX_Card_colorsid",
                table: "Card",
                column: "colorsid");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ColorIdentityid",
                table: "Colors",
                column: "ColorIdentityid");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ColorIndicatorid",
                table: "Colors",
                column: "ColorIndicatorid");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Colors_colorIdentityid",
                table: "Card",
                column: "colorIdentityid",
                principalTable: "Colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Colors_colorsid",
                table: "Card",
                column: "colorsid",
                principalTable: "Colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardFaces_Colors_colorIndicatorid",
                table: "CardFaces",
                column: "colorIndicatorid",
                principalTable: "Colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Color_Colors_Colorsid",
                table: "Color",
                column: "Colorsid",
                principalTable: "Colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
