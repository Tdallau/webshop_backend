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
    [Migration("20181016134545_add legalities link")]
    partial class addlegalitieslink
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

            modelBuilder.Entity("Models.DB.Card", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<int>("EdhrecRank");

                    b.Property<int?>("colorIdentityid");

                    b.Property<int?>("colorsid");

                    b.Property<int?>("legalitiesid");

                    b.Property<int>("price");

                    b.HasKey("Id");

                    b.HasIndex("colorIdentityid");

                    b.HasIndex("colorsid");

                    b.HasIndex("legalitiesid");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("Models.DB.CardFace", b =>
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

                    b.Property<int?>("Colorsid");

                    b.Property<string>("landType");

                    b.Property<string>("name");

                    b.Property<string>("symbol");

                    b.HasKey("Id");

                    b.HasIndex("Colorsid");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("Models.DB.ColorIdentity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("id");

                    b.ToTable("ColorIdentity");
                });

            modelBuilder.Entity("Models.DB.ColorIndicator", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("id");

                    b.ToTable("ColocolorIndicator");
                });

            modelBuilder.Entity("Models.DB.Colors", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ColorIdentityid");

                    b.Property<int?>("ColorIndicatorid");

                    b.HasKey("id");

                    b.HasIndex("ColorIdentityid");

                    b.HasIndex("ColorIndicatorid");

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

                    b.Property<string>("small");

                    b.HasKey("id");

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

                    b.Property<string>("standard");

                    b.Property<string>("vintage");

                    b.HasKey("id");

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

                    b.HasKey("id");

                    b.HasIndex("partOneId");

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

                    b.Property<bool>("nonfoil");

                    b.Property<bool>("oversized");

                    b.Property<int>("price");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

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
                        .HasMaxLength(4);

                    b.Property<string>("blockCode");

                    b.Property<int>("cardCount");

                    b.Property<bool>("foilOnly");

                    b.Property<string>("iconSVG");

                    b.Property<string>("name");

                    b.Property<string>("paretnSetCode");

                    b.Property<int>("releasedAt");

                    b.Property<string>("setType");

                    b.HasKey("Id");

                    b.ToTable("Set");
                });

            modelBuilder.Entity("Models.DB.Type", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("typeName");

                    b.HasKey("id");

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
                    b.HasOne("Models.DB.Colors", "colorIdentity")
                        .WithMany()
                        .HasForeignKey("colorIdentityid");

                    b.HasOne("Models.DB.Colors", "colors")
                        .WithMany()
                        .HasForeignKey("colorsid");

                    b.HasOne("Models.DB.Legalitie", "legalities")
                        .WithMany()
                        .HasForeignKey("legalitiesid");
                });

            modelBuilder.Entity("Models.DB.CardFace", b =>
                {
                    b.HasOne("Models.DB.Card", "card")
                        .WithMany()
                        .HasForeignKey("cardId");

                    b.HasOne("Models.DB.Colors", "colorIndicator")
                        .WithMany()
                        .HasForeignKey("colorIndicatorid");
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

            modelBuilder.Entity("Models.DB.Color", b =>
                {
                    b.HasOne("Models.DB.Colors")
                        .WithMany("color")
                        .HasForeignKey("Colorsid");
                });

            modelBuilder.Entity("Models.DB.Colors", b =>
                {
                    b.HasOne("Models.DB.ColorIdentity")
                        .WithMany("Color")
                        .HasForeignKey("ColorIdentityid");

                    b.HasOne("Models.DB.ColorIndicator")
                        .WithMany("Colors")
                        .HasForeignKey("ColorIndicatorid");
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
                });

            modelBuilder.Entity("Models.DB.Print", b =>
                {
                    b.HasOne("Models.DB.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");
                });
#pragma warning restore 612, 618
        }
    }
}
