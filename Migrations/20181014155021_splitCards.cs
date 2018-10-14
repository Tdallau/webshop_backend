using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class splitCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    salt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
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
                name: "OrderProduct",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    orderId = table.Column<int>(nullable: false),
                    productId = table.Column<int>(nullable: false),
                    price = table.Column<double>(nullable: false),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Order_orderId",
                        column: x => x.orderId,
                        principalTable: "Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardFaces",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cardId = table.Column<string>(nullable: true),
                    colorIndicatorid = table.Column<int>(nullable: true),
                    manaCost = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    oracleText = table.Column<string>(nullable: true),
                    power = table.Column<string>(nullable: true),
                    toughness = table.Column<string>(nullable: true),
                    loyalty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardFaces", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    typeName = table.Column<string>(nullable: true),
                    CardFaceid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.id);
                    table.ForeignKey(
                        name: "FK_Types_CardFaces_CardFaceid",
                        column: x => x.CardFaceid,
                        principalTable: "CardFaces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    partOneId = table.Column<string>(nullable: true),
                    partTwoId = table.Column<string>(nullable: true),
                    Productid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Print",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CardId = table.Column<string>(nullable: true),
                    price = table.Column<int>(nullable: false),
                    setId = table.Column<string>(nullable: true),
                    foil = table.Column<bool>(nullable: false),
                    nonfoil = table.Column<bool>(nullable: false),
                    oversized = table.Column<bool>(nullable: false),
                    borderColor = table.Column<string>(nullable: true),
                    collectorsNumber = table.Column<string>(nullable: true),
                    fullArt = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Print", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImagesUrl",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    printId = table.Column<string>(nullable: true),
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
                        name: "FK_ImagesUrl_Print_printId",
                        column: x => x.printId,
                        principalTable: "Print",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Set",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 4, nullable: false),
                    name = table.Column<string>(nullable: true),
                    setType = table.Column<string>(nullable: true),
                    releasedAt = table.Column<int>(nullable: false),
                    blockCode = table.Column<string>(nullable: true),
                    paretnSetCode = table.Column<string>(nullable: true),
                    cardCount = table.Column<int>(nullable: false),
                    foilOnly = table.Column<bool>(nullable: false),
                    iconSVG = table.Column<string>(nullable: true),
                    CardId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    legalitiesid = table.Column<int>(nullable: true),
                    EdhrecRank = table.Column<int>(nullable: false),
                    colorsid = table.Column<int>(nullable: true),
                    colorIdentityid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    symbol = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    landType = table.Column<string>(nullable: true),
                    Colorsid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 36, nullable: false),
                    lang = table.Column<string>(nullable: true),
                    oracle_id = table.Column<string>(nullable: true),
                    foil = table.Column<bool>(nullable: false),
                    loyalty = table.Column<string>(nullable: true),
                    mana_cost = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    nonfoil = table.Column<bool>(nullable: false),
                    oracle_text = table.Column<string>(nullable: true),
                    power = table.Column<string>(nullable: true),
                    reserved = table.Column<string>(nullable: true),
                    toughness = table.Column<string>(nullable: true),
                    type_line = table.Column<string>(nullable: true),
                    price = table.Column<string>(nullable: true),
                    image_urisid = table.Column<int>(nullable: true),
                    rarity = table.Column<string>(nullable: true),
                    set = table.Column<string>(nullable: true),
                    setName = table.Column<string>(nullable: true),
                    color_identityid = table.Column<int>(nullable: true),
                    color_indicatorid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                    table.ForeignKey(
                        name: "FK_Product_ImagesUrl_image_urisid",
                        column: x => x.image_urisid,
                        principalTable: "ImagesUrl",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Productid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.id);
                    table.ForeignKey(
                        name: "FK_Colors_Product_Productid",
                        column: x => x.Productid,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Legalitie",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    productId = table.Column<string>(nullable: true),
                    standard = table.Column<string>(nullable: true),
                    future = table.Column<string>(nullable: true),
                    frontier = table.Column<string>(nullable: true),
                    modern = table.Column<string>(nullable: true),
                    legacy = table.Column<string>(nullable: true),
                    pauper = table.Column<string>(nullable: true),
                    vintage = table.Column<string>(nullable: true),
                    penny = table.Column<string>(nullable: true),
                    commander = table.Column<string>(nullable: true),
                    one_v_one = table.Column<string>(nullable: true),
                    duel = table.Column<string>(nullable: true),
                    brawl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legalitie", x => x.id);
                    table.ForeignKey(
                        name: "FK_Legalitie_Product_productId",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "id",
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
                name: "IX_Card_colorsid",
                table: "Card",
                column: "colorsid");

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
                name: "IX_Color_Colorsid",
                table: "Color",
                column: "Colorsid");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_Productid",
                table: "Colors",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesUrl_printId",
                table: "ImagesUrl",
                column: "printId");

            migrationBuilder.CreateIndex(
                name: "IX_Legalitie_productId",
                table: "Legalitie",
                column: "productId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_userId",
                table: "Order",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_orderId",
                table: "OrderProduct",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_Productid",
                table: "Parts",
                column: "Productid");

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
                name: "IX_Print_setId",
                table: "Print",
                column: "setId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_color_identityid",
                table: "Product",
                column: "color_identityid");

            migrationBuilder.CreateIndex(
                name: "IX_Product_color_indicatorid",
                table: "Product",
                column: "color_indicatorid");

            migrationBuilder.CreateIndex(
                name: "IX_Product_image_urisid",
                table: "Product",
                column: "image_urisid");

            migrationBuilder.CreateIndex(
                name: "IX_Set_CardId",
                table: "Set",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Types_CardFaceid",
                table: "Types",
                column: "CardFaceid");

            migrationBuilder.AddForeignKey(
                name: "FK_CardFaces_Colors_colorIndicatorid",
                table: "CardFaces",
                column: "colorIndicatorid",
                principalTable: "Colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardFaces_Card_cardId",
                table: "CardFaces",
                column: "cardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Card_partOneId",
                table: "Parts",
                column: "partOneId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Card_partTwoId",
                table: "Parts",
                column: "partTwoId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_Product_Productid",
                table: "Parts",
                column: "Productid",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Print_Card_CardId",
                table: "Print",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Print_Set_setId",
                table: "Print",
                column: "setId",
                principalTable: "Set",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Set_Card_CardId",
                table: "Set",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Card_Legalitie_legalitiesid",
                table: "Card",
                column: "legalitiesid",
                principalTable: "Legalitie",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Color_Colors_Colorsid",
                table: "Color",
                column: "Colorsid",
                principalTable: "Colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Colors_color_identityid",
                table: "Product",
                column: "color_identityid",
                principalTable: "Colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Colors_color_indicatorid",
                table: "Product",
                column: "color_indicatorid",
                principalTable: "Colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Colors_colorIdentityid",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Colors_colorsid",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Colors_color_identityid",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Colors_color_indicatorid",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Legalitie_legalitiesid",
                table: "Card");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "PrintFace");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "CardFaces");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Legalitie");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ImagesUrl");

            migrationBuilder.DropTable(
                name: "Print");

            migrationBuilder.DropTable(
                name: "Set");

            migrationBuilder.DropTable(
                name: "Card");
        }
    }
}
