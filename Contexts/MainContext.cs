using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Models.DB;
using webshop_backend;

namespace Contexts
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options): base(options)
        {
        }
        //this is actual entity object linked to the test in our DB
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Parts> Parts { get; set; }
        public DbSet<Legalitie> Legalitie { get; set; }
        public DbSet<ColorIndicator> ColocolorIndicator { get; set; }
        public DbSet<ColorIdentity> ColorIdentity { get; set; }
        public DbSet<ImagesUrl> ImagesUrl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parts>()
                .HasOne(p => p.partOne)
                .WithMany(b => b.all_parts)
                .HasForeignKey(p => p.partOneId);
        }
    }
}