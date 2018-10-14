using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Models;
using webshop_backend;

namespace Models
{
    public class MainContext : DbContext
    {
        //this is actual entity object linked to the test in our DB
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Parts> Parts { get; set; }
        public DbSet<Legalitie> Legalitie { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<ImagesUrl> ImagesUrl { get; set; }
        public DbSet<Card> Card {get; set;}
        public DbSet<CardFace> CardFaces {get; set;}
        public DbSet<Print> Print {get; set;}
        public DbSet<PrintFace> PrintFace {get; set;}
        public DbSet<Set> Set {get; set;}
        public DbSet<Type> Types {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parts>()
                .HasOne(p => p.partOne)
                .WithMany(b => b.allParts);
        }

        //this method is run automatically by EF the first time we run the application
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //here we define the name of our database
            optionsBuilder.UseMySql(ConfigurationManager.AppSetting["DBConectionString"]);
        }
    }
}