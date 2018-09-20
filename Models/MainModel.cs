using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Models;

namespace Models
{
    public class MainContext : DbContext
  {
    //this is actual entity object linked to the test in our DB
    public DbSet<Test> TestDB { get; set; }


    //this method is run automatically by EF the first time we run the application
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
      //here we define the name of our database
      optionsBuilder.UseMySql("User ID=root;Password=projectC;Host=localhost;Port=32779;Database=ProjectC;Pooling=true;");
    }
  }
}