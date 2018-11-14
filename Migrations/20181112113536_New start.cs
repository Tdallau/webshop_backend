using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class Newstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 10, nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    symbol = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    landType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

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
                name: "Languages",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Legalities",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 10, nullable: true),
                    backgroundColor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legalities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PrintFace",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PrintId = table.Column<string>(nullable: true),
                    flavorText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintFace", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TypeLine",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeLine", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    typeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    approach = table.Column<string>(nullable: true),
                    role = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    salt = table.Column<string>(nullable: true),
                    token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Set",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 6, nullable: false),
                    name = table.Column<string>(nullable: true),
                    setType = table.Column<string>(nullable: true),
                    releasedAt = table.Column<int>(nullable: false),
                    blockid = table.Column<string>(nullable: true),
                    paretnSetCode = table.Column<string>(nullable: true),
                    cardCount = table.Column<int>(nullable: false),
                    foilOnly = table.Column<bool>(nullable: false),
                    iconSVG = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Set_Block_blockid",
                        column: x => x.blockid,
                        principalTable: "Block",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Legalitie",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    standardid = table.Column<int>(nullable: true),
                    futureid = table.Column<int>(nullable: true),
                    frontierid = table.Column<int>(nullable: true),
                    modernid = table.Column<int>(nullable: true),
                    legacyid = table.Column<int>(nullable: true),
                    pauperid = table.Column<int>(nullable: true),
                    vintageid = table.Column<int>(nullable: true),
                    pennyid = table.Column<int>(nullable: true),
                    commanderid = table.Column<int>(nullable: true),
                    one_v_oneid = table.Column<int>(nullable: true),
                    duelid = table.Column<int>(nullable: true),
                    brawlid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legalitie", x => x.id);
                    table.ForeignKey(
                        name: "FK_Legalitie_Legalities_brawlid",
                        column: x => x.brawlid,
                        principalTable: "Legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Legalitie_Legalities_commanderid",
                        column: x => x.commanderid,
                        principalTable: "Legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Legalitie_Legalities_duelid",
                        column: x => x.duelid,
                        principalTable: "Legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Legalitie_Legalities_frontierid",
                        column: x => x.frontierid,
                        principalTable: "Legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Legalitie_Legalities_futureid",
                        column: x => x.futureid,
                        principalTable: "Legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Legalitie_Legalities_legacyid",
                        column: x => x.legacyid,
                        principalTable: "Legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Legalitie_Legalities_modernid",
                        column: x => x.modernid,
                        principalTable: "Legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Legalitie_Legalities_one_v_oneid",
                        column: x => x.one_v_oneid,
                        principalTable: "Legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Legalitie_Legalities_pauperid",
                        column: x => x.pauperid,
                        principalTable: "Legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Legalitie_Legalities_pennyid",
                        column: x => x.pennyid,
                        principalTable: "Legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Legalitie_Legalities_standardid",
                        column: x => x.standardid,
                        principalTable: "Legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Legalitie_Legalities_vintageid",
                        column: x => x.vintageid,
                        principalTable: "Legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImagesUrl",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    printFaceid = table.Column<int>(nullable: true),
                    small = table.Column<string>(nullable: true),
                    normal = table.Column<string>(nullable: true),
                    large = table.Column<string>(nullable: true),
                    png = table.Column<string>(nullable: true),
                    art_crop = table.Column<string>(nullable: true),
                    border_crop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesUrl", x => x.id);
                    table.ForeignKey(
                        name: "FK_ImagesUrl_PrintFace_printFaceid",
                        column: x => x.printFaceid,
                        principalTable: "PrintFace",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypesInLine",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    typeid = table.Column<int>(nullable: true),
                    lineid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesInLine", x => x.id);
                    table.ForeignKey(
                        name: "FK_TypesInLine_TypeLine_lineid",
                        column: x => x.lineid,
                        principalTable: "TypeLine",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TypesInLine_Types_typeid",
                        column: x => x.typeid,
                        principalTable: "Types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ZipCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(nullable: false),
                    addressId = table.Column<int>(nullable: false),
                    status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK_Order_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    legalitiesid = table.Column<int>(nullable: true),
                    EdhrecRank = table.Column<int>(nullable: true),
                    colorIdentityid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_ColorCombinations_colorIdentityid",
                        column: x => x.colorIdentityid,
                        principalTable: "ColorCombinations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Card_Legalitie_legalitiesid",
                        column: x => x.legalitiesid,
                        principalTable: "Legalitie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CardFaces",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cardId = table.Column<string>(nullable: true),
                    colorIndicatorid = table.Column<int>(nullable: true),
                    manaCostid = table.Column<int>(nullable: true),
                    colorid = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    typeLineid = table.Column<int>(nullable: true),
                    oracleText = table.Column<string>(nullable: true),
                    power = table.Column<string>(nullable: true),
                    toughness = table.Column<string>(nullable: true),
                    loyalty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardFaces", x => x.id);
                    table.ForeignKey(
                        name: "FK_CardFaces_Card_cardId",
                        column: x => x.cardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardFaces_ColorCombinations_colorIndicatorid",
                        column: x => x.colorIndicatorid,
                        principalTable: "ColorCombinations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardFaces_ColorCombinations_colorid",
                        column: x => x.colorid,
                        principalTable: "ColorCombinations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardFaces_Costs_manaCostid",
                        column: x => x.manaCostid,
                        principalTable: "Costs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardFaces_TypeLine_typeLineid",
                        column: x => x.typeLineid,
                        principalTable: "TypeLine",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    orderId = table.Column<int>(nullable: false),
                    cardId = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Card_cardId",
                        column: x => x.cardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Order_orderId",
                        column: x => x.orderId,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    partOneId = table.Column<string>(nullable: true),
                    partTwoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Parts_Card_partOneId",
                        column: x => x.partOneId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parts_Card_partTwoId",
                        column: x => x.partTwoId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Print",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CardId = table.Column<string>(nullable: true),
                    price = table.Column<int>(nullable: true),
                    setId = table.Column<string>(nullable: true),
                    foil = table.Column<bool>(nullable: false),
                    nonfoil = table.Column<bool>(nullable: false),
                    oversized = table.Column<bool>(nullable: false),
                    borderColor = table.Column<string>(nullable: true),
                    collectorsNumber = table.Column<string>(nullable: true),
                    fullArt = table.Column<bool>(nullable: false),
                    languageid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Print", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Print_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Print_Languages_languageid",
                        column: x => x.languageid,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Print_Set_setId",
                        column: x => x.setId,
                        principalTable: "Set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_colorIdentityid",
                table: "Card",
                column: "colorIdentityid");

            migrationBuilder.CreateIndex(
                name: "IX_Card_legalitiesid",
                table: "Card",
                column: "legalitiesid");

            migrationBuilder.CreateIndex(
                name: "IX_CardFaces_cardId",
                table: "CardFaces",
                column: "cardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardFaces_colorIndicatorid",
                table: "CardFaces",
                column: "colorIndicatorid");

            migrationBuilder.CreateIndex(
                name: "IX_CardFaces_colorid",
                table: "CardFaces",
                column: "colorid");

            migrationBuilder.CreateIndex(
                name: "IX_CardFaces_manaCostid",
                table: "CardFaces",
                column: "manaCostid");

            migrationBuilder.CreateIndex(
                name: "IX_CardFaces_typeLineid",
                table: "CardFaces",
                column: "typeLineid");

            migrationBuilder.CreateIndex(
                name: "IX_CardsInSets_cardId",
                table: "CardsInSets",
                column: "cardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardsInSets_setId",
                table: "CardsInSets",
                column: "setId");

            migrationBuilder.CreateIndex(
                name: "IX_ColorsInCombinations_colorId",
                table: "ColorsInCombinations",
                column: "colorId");

            migrationBuilder.CreateIndex(
                name: "IX_ColorsInCombinations_combinationid",
                table: "ColorsInCombinations",
                column: "combinationid");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesUrl_printFaceid",
                table: "ImagesUrl",
                column: "printFaceid");

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
                name: "IX_Order_userId",
                table: "Order",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_cardId",
                table: "OrderProduct",
                column: "cardId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_orderId",
                table: "OrderProduct",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_partOneId",
                table: "Parts",
                column: "partOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_partTwoId",
                table: "Parts",
                column: "partTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Print_CardId",
                table: "Print",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Print_languageid",
                table: "Print",
                column: "languageid");

            migrationBuilder.CreateIndex(
                name: "IX_Print_setId",
                table: "Print",
                column: "setId");

            migrationBuilder.CreateIndex(
                name: "IX_Set_blockid",
                table: "Set",
                column: "blockid");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolsInCosts_costid",
                table: "SymbolsInCosts",
                column: "costid");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolsInCosts_symbolid",
                table: "SymbolsInCosts",
                column: "symbolid");

            migrationBuilder.CreateIndex(
                name: "IX_TypesInLine_lineid",
                table: "TypesInLine",
                column: "lineid");

            migrationBuilder.CreateIndex(
                name: "IX_TypesInLine_typeid",
                table: "TypesInLine",
                column: "typeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "CardFaces");

            migrationBuilder.DropTable(
                name: "CardsInSets");

            migrationBuilder.DropTable(
                name: "ColorsInCombinations");

            migrationBuilder.DropTable(
                name: "ImagesUrl");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Print");

            migrationBuilder.DropTable(
                name: "SymbolsInCosts");

            migrationBuilder.DropTable(
                name: "TypesInLine");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "PrintFace");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Set");

            migrationBuilder.DropTable(
                name: "Costs");

            migrationBuilder.DropTable(
                name: "CostSymbols");

            migrationBuilder.DropTable(
                name: "TypeLine");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ColorCombinations");

            migrationBuilder.DropTable(
                name: "Legalitie");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "Legalities");
        }
    }
}
