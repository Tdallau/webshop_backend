using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Models.DB;
using webshop_backend;
using Models;
using webshop_backend.Models.DB;

namespace Contexts
{
    public class MainContext : DbContext
    {

        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<ShoppingCard> ShoppingCard { get; set; }
        public DbSet<ShoppingCardItem> ShoppingCardItem { get; set; }
        public DbSet<Parts> Parts { get; set; }
        public DbSet<Legalitie> Legalitie { get; set; }
        public DbSet<ImagesUrl> ImagesUrl { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<CardFace> CardFaces { get; set; }
        public DbSet<Print> Print { get; set; }
        public DbSet<PrintFace> PrintFace { get; set; }
        public DbSet<Set> Set { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<TypesInLine> TypesInLine { get; set; }
        public DbSet<TypeLine> TypeLine { get; set; }
        public DbSet<CardInSet> CardsInSets { get; set; }
        public DbSet<Legalities> Legalities { get; set; }
        public DbSet<CostSymbols> CostSymbols { get; set; }
        public DbSet<Block> Block { get; set; }
        public DbSet<Costs> Costs { get; set; }
        public DbSet<SymbolsInCosts> SymbolsInCosts { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ColorCombinations> ColorCombinations { get; set; }
        public DbSet<ColorsInCombinations> ColorsInCombinations { get; set; }
        public DbSet<Decks> Decks {get; set;}
        public DbSet<CardsDeck> CardsDecks {get; set;}

        public DbQuery<ProductList> ProductList {get; set;}
        //*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parts>()
                .HasOne(p => p.partOne)
                .WithMany(b => b.allParts);

            modelBuilder
                .Query<ProductList>().ToView("View_productList");
            /*
           modelBuilder.Entity<CardInSet>()
               .HasKey(ma => new { ma.card, ma.set });
           /*
           modelBuilder.Entity<CardInSet>()
               .HasOne(m => m.card)
               .WithMany(r => r.availableSets);
               //.HasForeignKey(m => m.card);
           modelBuilder.Entity<CardInSet>()
               .HasOne(a => a.set)
               .WithMany(r => r.Cards);
               //.HasForeignKey(a => a.set);
          // */
        }

        //this method is run automatically by EF the first time we run the application
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {

        //     //here we define the name of our database
        //     optionsBuilder.UseMySql(ConfigurationManager.AppSetting["DBConectionString"]);
        // }
    }
}