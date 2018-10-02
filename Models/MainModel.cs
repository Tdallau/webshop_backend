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
        public DbSet<ColocolorIndicator> ColocolorIndicator { get; set; }
        public DbSet<ColorIdentity> ColorIdentity { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<ImagesUrl> ImagesUrl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parts>()
                .HasOne(p => p.partOne)
                .WithMany(b => b.all_parts)
                .HasForeignKey(p => p.partOneId);
        }

        //this method is run automatically by EF the first time we run the application
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //here we define the name of our database
            optionsBuilder.UseMySql(ConfigurationManager.AppSetting["DBConectionString"]);
        }
    }
}