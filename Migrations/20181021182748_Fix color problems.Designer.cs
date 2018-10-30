﻿// <auto-generated />
using System;
using Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace webshop_backend.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20181021182748_Fix color problems")]
    partial class Fixcolorproblems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Models.DB.Block", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("Block");
                });

            modelBuilder.Entity("Models.DB.Card", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<int?>("EdhrecRank");

                    b.Property<int?>("colorIdentityid");

                    b.Property<int?>("legalitiesid");

                    b.HasKey("Id");

                    b.HasIndex("colorIdentityid");

                    b.HasIndex("legalitiesid");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("Models.DB.CardFace", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("cardId");

                    b.Property<int?>("colorIndicatorid");

                    b.Property<int?>("colorid");

                    b.Property<string>("loyalty");

                    b.Property<int?>("manaCostid");

                    b.Property<string>("name");

                    b.Property<string>("oracleText");

                    b.Property<string>("power");

                    b.Property<string>("toughness");

                    b.HasKey("id");

                    b.HasIndex("cardId");

                    b.HasIndex("colorIndicatorid");

                    b.HasIndex("colorid");

                    b.HasIndex("manaCostid");

                    b.ToTable("CardFaces");
                });

            modelBuilder.Entity("Models.DB.CardInSet", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("cardId");

                    b.Property<string>("setId");

                    b.HasKey("id");

                    b.HasIndex("cardId");

                    b.HasIndex("setId");

                    b.ToTable("CardsInSets");
                });

            modelBuilder.Entity("Models.DB.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("landType");

                    b.Property<string>("name");

                    b.Property<string>("symbol");

                    b.HasKey("Id");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("Models.DB.ColorCombinations", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("id");

                    b.ToTable("ColorCombinations");
                });

            modelBuilder.Entity("Models.DB.ColorsInCombinations", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("colorId");

                    b.Property<int?>("combinationid");

                    b.HasKey("id");

                    b.HasIndex("colorId");

                    b.HasIndex("combinationid");

                    b.ToTable("ColorsInCombinations");
                });

            modelBuilder.Entity("Models.DB.Costs", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("id");

                    b.ToTable("Costs");
                });

            modelBuilder.Entity("Models.DB.CostSymbols", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("picturePath");

                    b.Property<string>("strSymbol");

                    b.HasKey("id");

                    b.ToTable("CostSymbols");
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

            modelBuilder.Entity("Models.DB.Language", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("code");

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Models.DB.Legalitie", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("brawlid");

                    b.Property<int?>("commanderid");

                    b.Property<int?>("duelid");

                    b.Property<int?>("frontierid");

                    b.Property<int?>("futureid");

                    b.Property<int?>("legacyid");

                    b.Property<int?>("modernid");

                    b.Property<int?>("one_v_oneid");

                    b.Property<int?>("pauperid");

                    b.Property<int?>("pennyid");

                    b.Property<int?>("standardid");

                    b.Property<int?>("vintageid");

                    b.HasKey("id");

                    b.HasIndex("brawlid");

                    b.HasIndex("commanderid");

                    b.HasIndex("duelid");

                    b.HasIndex("frontierid");

                    b.HasIndex("futureid");

                    b.HasIndex("legacyid");

                    b.HasIndex("modernid");

                    b.HasIndex("one_v_oneid");

                    b.HasIndex("pauperid");

                    b.HasIndex("pennyid");

                    b.HasIndex("standardid");

                    b.HasIndex("vintageid");

                    b.ToTable("Legalitie");
                });

            modelBuilder.Entity("Models.DB.Legalities", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("backgroundColor");

                    b.Property<string>("name")
                        .HasMaxLength(10);

                    b.HasKey("id");

                    b.ToTable("Legalities");
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

                    b.Property<string>("cardId");

                    b.Property<int>("orderId");

                    b.Property<double>("price");

                    b.Property<int>("quantity");

                    b.HasKey("id");

                    b.HasIndex("cardId");

                    b.HasIndex("orderId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("Models.DB.Parts", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("partOneId");

                    b.Property<string>("partTwoId");

                    b.HasKey("id");

                    b.HasIndex("partOneId");

                    b.HasIndex("partTwoId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("Models.DB.Print", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("CardId");

                    b.Property<string>("borderColor");

                    b.Property<string>("collectorsNumber");

                    b.Property<bool>("foil");

                    b.Property<bool>("fullArt");

                    b.Property<int?>("languageid");

                    b.Property<bool>("nonfoil");

                    b.Property<bool>("oversized");

                    b.Property<int?>("price");

                    b.Property<string>("setId");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("languageid");

                    b.HasIndex("setId");

                    b.ToTable("Print");
                });

            modelBuilder.Entity("Models.DB.PrintFace", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PrintId");

                    b.Property<string>("flavorText");

                    b.HasKey("id");

                    b.ToTable("PrintFace");
                });

            modelBuilder.Entity("Models.DB.Set", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(6);

                    b.Property<string>("blockid");

                    b.Property<int>("cardCount");

                    b.Property<bool>("foilOnly");

                    b.Property<string>("iconSVG");

                    b.Property<string>("name");

                    b.Property<string>("paretnSetCode");

                    b.Property<int>("releasedAt");

                    b.Property<string>("setType");

                    b.HasKey("Id");

                    b.HasIndex("blockid");

                    b.ToTable("Set");
                });

            modelBuilder.Entity("Models.DB.SymbolsInCosts", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("costid");

                    b.Property<int?>("symbolid");

                    b.HasKey("id");

                    b.HasIndex("costid");

                    b.HasIndex("symbolid");

                    b.ToTable("SymbolsInCosts");
                });

            modelBuilder.Entity("Models.DB.Type", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CardFaceid");

                    b.Property<string>("typeName");

                    b.HasKey("id");

                    b.HasIndex("CardFaceid");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Models.DB.User", b =>
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

            modelBuilder.Entity("Models.DB.Card", b =>
                {
                    b.HasOne("Models.DB.ColorCombinations", "colorIdentity")
                        .WithMany()
                        .HasForeignKey("colorIdentityid");

                    b.HasOne("Models.DB.Legalitie", "legalities")
                        .WithMany()
                        .HasForeignKey("legalitiesid");
                });

            modelBuilder.Entity("Models.DB.CardFace", b =>
                {
                    b.HasOne("Models.DB.Card", "card")
                        .WithMany()
                        .HasForeignKey("cardId");

                    b.HasOne("Models.DB.ColorCombinations", "colorIndicator")
                        .WithMany()
                        .HasForeignKey("colorIndicatorid");

                    b.HasOne("Models.DB.ColorCombinations", "color")
                        .WithMany()
                        .HasForeignKey("colorid");

                    b.HasOne("Models.DB.Costs", "manaCost")
                        .WithMany()
                        .HasForeignKey("manaCostid");
                });

            modelBuilder.Entity("Models.DB.CardInSet", b =>
                {
                    b.HasOne("Models.DB.Card", "card")
                        .WithMany("availableSets")
                        .HasForeignKey("cardId");

                    b.HasOne("Models.DB.Set", "set")
                        .WithMany("Cards")
                        .HasForeignKey("setId");
                });

            modelBuilder.Entity("Models.DB.ColorsInCombinations", b =>
                {
                    b.HasOne("Models.DB.Color", "color")
                        .WithMany()
                        .HasForeignKey("colorId");

                    b.HasOne("Models.DB.ColorCombinations", "combination")
                        .WithMany()
                        .HasForeignKey("combinationid");
                });

            modelBuilder.Entity("Models.DB.ImagesUrl", b =>
                {
                    b.HasOne("Models.DB.Print", "print")
                        .WithMany()
                        .HasForeignKey("printId");
                });

            modelBuilder.Entity("Models.DB.Legalitie", b =>
                {
                    b.HasOne("Models.DB.Legalities", "brawl")
                        .WithMany()
                        .HasForeignKey("brawlid");

                    b.HasOne("Models.DB.Legalities", "commander")
                        .WithMany()
                        .HasForeignKey("commanderid");

                    b.HasOne("Models.DB.Legalities", "duel")
                        .WithMany()
                        .HasForeignKey("duelid");

                    b.HasOne("Models.DB.Legalities", "frontier")
                        .WithMany()
                        .HasForeignKey("frontierid");

                    b.HasOne("Models.DB.Legalities", "future")
                        .WithMany()
                        .HasForeignKey("futureid");

                    b.HasOne("Models.DB.Legalities", "legacy")
                        .WithMany()
                        .HasForeignKey("legacyid");

                    b.HasOne("Models.DB.Legalities", "modern")
                        .WithMany()
                        .HasForeignKey("modernid");

                    b.HasOne("Models.DB.Legalities", "one_v_one")
                        .WithMany()
                        .HasForeignKey("one_v_oneid");

                    b.HasOne("Models.DB.Legalities", "pauper")
                        .WithMany()
                        .HasForeignKey("pauperid");

                    b.HasOne("Models.DB.Legalities", "penny")
                        .WithMany()
                        .HasForeignKey("pennyid");

                    b.HasOne("Models.DB.Legalities", "standard")
                        .WithMany()
                        .HasForeignKey("standardid");

                    b.HasOne("Models.DB.Legalities", "vintage")
                        .WithMany()
                        .HasForeignKey("vintageid");
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
                    b.HasOne("Models.DB.Card", "card")
                        .WithMany()
                        .HasForeignKey("cardId");

                    b.HasOne("Models.DB.Order")
                        .WithMany("orderProducts")
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.DB.Parts", b =>
                {
                    b.HasOne("Models.DB.Card", "partOne")
                        .WithMany("allParts")
                        .HasForeignKey("partOneId");

                    b.HasOne("Models.DB.Card", "partTwo")
                        .WithMany()
                        .HasForeignKey("partTwoId");
                });

            modelBuilder.Entity("Models.DB.Print", b =>
                {
                    b.HasOne("Models.DB.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");

                    b.HasOne("Models.DB.Language", "language")
                        .WithMany()
                        .HasForeignKey("languageid");

                    b.HasOne("Models.DB.Set", "set")
                        .WithMany()
                        .HasForeignKey("setId");
                });

            modelBuilder.Entity("Models.DB.Set", b =>
                {
                    b.HasOne("Models.DB.Block", "block")
                        .WithMany()
                        .HasForeignKey("blockid");
                });

            modelBuilder.Entity("Models.DB.SymbolsInCosts", b =>
                {
                    b.HasOne("Models.DB.Costs", "cost")
                        .WithMany("Symbols")
                        .HasForeignKey("costid");

                    b.HasOne("Models.DB.CostSymbols", "symbol")
                        .WithMany()
                        .HasForeignKey("symbolid");
                });

            modelBuilder.Entity("Models.DB.Type", b =>
                {
                    b.HasOne("Models.DB.CardFace")
                        .WithMany("typeLine")
                        .HasForeignKey("CardFaceid");
                });
#pragma warning restore 612, 618
        }
    }
}
