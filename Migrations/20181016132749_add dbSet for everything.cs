using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_backend.Migrations
{
    public partial class adddbSetforeverything : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "ImagesUrl",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                });

            migrationBuilder.CreateTable(
                name: "Legalitie",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                    price = table.Column<int>(nullable: false),
                    EdhrecRank = table.Column<int>(nullable: false),
                    colorIdentityid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_Colors_colorIdentityid",
                        column: x => x.colorIdentityid,
                        principalTable: "Colors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_Color_Colors_Colorsid",
                        column: x => x.Colorsid,
                        principalTable: "Colors",
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
                    table.ForeignKey(
                        name: "FK_CardFaces_Card_cardId",
                        column: x => x.cardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardFaces_Colors_colorIndicatorid",
                        column: x => x.colorIndicatorid,
                        principalTable: "Colors",
                        principalColumn: "id",
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
                    partOneId = table.Column<string>(nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Print",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CardId = table.Column<string>(nullable: true),
                    price = table.Column<int>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Print_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
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
                name: "IX_Colors_ColorIdentityid",
                table: "Colors",
                column: "ColorIdentityid");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ColorIndicatorid",
                table: "Colors",
                column: "ColorIndicatorid");

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
                name: "IX_Print_CardId",
                table: "Print",
                column: "CardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "CardFaces");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "ImagesUrl");

            migrationBuilder.DropTable(
                name: "Legalitie");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Print");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "ColorIdentity");

            migrationBuilder.DropTable(
                name: "ColocolorIndicator");
        }
    }
}
