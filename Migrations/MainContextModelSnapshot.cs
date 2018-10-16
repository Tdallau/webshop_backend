﻿// <auto-generated />
using Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace webshop_backend.Migrations
{
    [DbContext(typeof(MainContext))]
    partial class MainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Models.DB.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<int>("Number");

                    b.Property<string>("Street");

                    b.Property<int>("UserId");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Models.Card", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<int>("EdhrecRank");

                    b.Property<int?>("colorIdentityid");

                    b.Property<int?>("colorsid");

                    b.Property<int?>("legalitiesid");

                    b.HasKey("Id");

                    b.HasIndex("colorIdentityid");

                    b.HasIndex("colorsid");

                    b.HasIndex("legalitiesid");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("Models.CardFace", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("cardId");

                    b.Property<int?>("colorIndicatorid");

                    b.Property<string>("loyalty");

                    b.Property<string>("manaCost");

                    b.Property<string>("name");

                    b.Property<string>("oracleText");

                    b.Property<string>("power");

                    b.Property<string>("toughness");

                    b.HasKey("id");

                    b.HasIndex("cardId");

                    b.HasIndex("colorIndicatorid");

                    b.ToTable("CardFaces");
                });

            modelBuilder.Entity("Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Colorsid");

                    b.Property<string>("landType");

                    b.Property<string>("name");

                    b.Property<string>("symbol");

                    b.HasKey("Id");

                    b.HasIndex("Colorsid");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("Models.DB.Colors", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Productid");

                    b.HasKey("id");

                    b.HasIndex("Productid");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Models.DB.ImagesUrl", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("art_crop");

                    b.Property<string>("border_crop");

                    b.Property<string>("large");

                    b.Property<string>("normal");

                    b.Property<string>("png");

                    b.Property<string>("printId");

                    b.Property<string>("small");

                    b.HasKey("id");

                    b.HasIndex("printId");

                    b.ToTable("ImagesUrl");
                });

            modelBuilder.Entity("Models.DB.Legalitie", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("brawl");

                    b.Property<string>("commander");

                    b.Property<string>("duel");

                    b.Property<string>("frontier");

                    b.Property<string>("future");

                    b.Property<string>("legacy");

                    b.Property<string>("modern");

                    b.Property<string>("one_v_one");

                    b.Property<string>("pauper");

                    b.Property<string>("penny");

                    b.Property<string>("productId");

                    b.Property<string>("standard");

                    b.Property<string>("vintage");

                    b.HasKey("id");

                    b.HasIndex("productId")
                        .IsUnique();

                    b.ToTable("Legalitie");
                });

            modelBuilder.Entity("Models.DB.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("addressId");

                    b.Property<string>("status");

                    b.Property<int>("userId");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Models.DB.OrderProduct", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("orderId");

                    b.Property<double>("price");

                    b.Property<int>("productId");

                    b.Property<int>("quantity");

                    b.HasKey("id");

                    b.HasIndex("orderId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("Models.DB.Parts", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Productid");

                    b.Property<string>("partOneId");

                    b.Property<string>("partTwoId");

                    b.HasKey("id");

                    b.HasIndex("Productid");

                    b.HasIndex("partOneId");

                    b.HasIndex("partTwoId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("Models.Print", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("CardId");

                    b.Property<string>("borderColor");

                    b.Property<string>("collectorsNumber");

                    b.Property<bool>("foil");

                    b.Property<bool>("fullArt");

                    b.Property<bool>("nonfoil");

                    b.Property<bool>("oversized");

                    b.Property<int>("price");

                    b.Property<string>("setId");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("setId");

                    b.ToTable("Print");
                });

            modelBuilder.Entity("Models.PrintFace", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PrintId");

                    b.Property<string>("flavorText");

                    b.HasKey("id");

                    b.ToTable("PrintFace");
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<int?>("color_identityid");

                    b.Property<int?>("color_indicatorid");

                    b.Property<bool>("foil");

                    b.Property<int?>("image_urisid");

                    b.Property<string>("lang");

                    b.Property<string>("loyalty");

                    b.Property<string>("mana_cost");

                    b.Property<string>("name");

                    b.Property<bool>("nonfoil");

                    b.Property<string>("oracle_id");

                    b.Property<string>("oracle_text");

                    b.Property<string>("power");

                    b.Property<string>("price");

                    b.Property<string>("rarity");

                    b.Property<string>("reserved");

                    b.Property<string>("set");

                    b.Property<string>("setName");

                    b.Property<string>("toughness");

                    b.Property<string>("type_line");

                    b.HasKey("id");

                    b.HasIndex("color_identityid");

                    b.HasIndex("color_indicatorid");

                    b.HasIndex("image_urisid");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Models.Set", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(4);

                    b.Property<string>("CardId");

                    b.Property<string>("blockCode");

                    b.Property<int>("cardCount");

                    b.Property<bool>("foilOnly");

                    b.Property<string>("iconSVG");

                    b.Property<string>("name");

                    b.Property<string>("paretnSetCode");

                    b.Property<int>("releasedAt");

                    b.Property<string>("setType");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("Set");
                });

            modelBuilder.Entity("Models.Type", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CardFaceid");

                    b.Property<string>("typeName");

                    b.HasKey("id");

                    b.HasIndex("CardFaceid");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("approach");

                    b.Property<string>("email");

                    b.Property<string>("name");

                    b.Property<string>("password");

                    b.Property<string>("role");

                    b.Property<string>("salt");

                    b.Property<string>("token");

                    b.HasKey("id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Models.DB.Address", b =>
                {
                    b.HasOne("Models.DB.User")
                        .WithMany("addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Card", b =>
                {
                    b.HasOne("Models.Colors", "colorIdentity")
                        .WithMany()
                        .HasForeignKey("colorIdentityid");

                    b.HasOne("Models.Colors", "colors")
                        .WithMany()
                        .HasForeignKey("colorsid");

                    b.HasOne("Models.Legalitie", "legalities")
                        .WithMany()
                        .HasForeignKey("legalitiesid");
                });

            modelBuilder.Entity("Models.CardFace", b =>
                {
                    b.HasOne("Models.Card", "card")
                        .WithMany()
                        .HasForeignKey("cardId");

                    b.HasOne("Models.Colors", "colorIndicator")
                        .WithMany()
                        .HasForeignKey("colorIndicatorid");
                });

            modelBuilder.Entity("Models.Color", b =>
                {
                    b.HasOne("Models.Colors")
                        .WithMany("color")
                        .HasForeignKey("Colorsid");
                });

            modelBuilder.Entity("Models.DB.Colors", b =>
                {
                    b.HasOne("Models.DB.Product")
                        .WithMany("colors")
                        .HasForeignKey("Productid");
                });

            modelBuilder.Entity("Models.DB.ImagesUrl", b =>
                {
                    b.HasOne("Models.Print", "print")
                        .WithMany()
                        .HasForeignKey("printId");
                });

            modelBuilder.Entity("Models.DB.Legalitie", b =>
                {
                    b.HasOne("Models.DB.Product")
                        .WithOne("legalities")
                        .HasForeignKey("Models.DB.Legalitie", "productId");
                });

            modelBuilder.Entity("Models.DB.Order", b =>
                {
                    b.HasOne("Models.DB.User")
                        .WithMany("orders")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.DB.OrderProduct", b =>
                {
                    b.HasOne("Models.DB.Order")
                        .WithMany("orderProducts")
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.DB.Parts", b =>
                {
                    b.HasOne("Models.Product")
                        .WithMany("all_parts")
                        .HasForeignKey("Productid");

                    b.HasOne("Models.Card", "partOne")
                        .WithMany("allParts")
                        .HasForeignKey("partOneId");

                    b.HasOne("Models.Card", "partTwo")
                        .WithMany()
                        .HasForeignKey("partTwoId");
                });

            modelBuilder.Entity("Models.Print", b =>
                {
                    b.HasOne("Models.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");

                    b.HasOne("Models.Set", "set")
                        .WithMany()
                        .HasForeignKey("setId");
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.HasOne("Models.Colors", "color_identity")
                        .WithMany()
                        .HasForeignKey("color_identityid");

                    b.HasOne("Models.Colors", "color_indicator")
                        .WithMany()
                        .HasForeignKey("color_indicatorid");

                    b.HasOne("Models.ImagesUrl", "image_uris")
                        .WithMany()
                        .HasForeignKey("image_urisid");
                });

            modelBuilder.Entity("Models.Set", b =>
                {
                    b.HasOne("Models.Card")
                        .WithMany("availableSets")
                        .HasForeignKey("CardId");
                });

            modelBuilder.Entity("Models.Type", b =>
                {
                    b.HasOne("Models.CardFace")
                        .WithMany("typeLine")
                        .HasForeignKey("CardFaceid");
                });
#pragma warning restore 612, 618
        }
    }
}
