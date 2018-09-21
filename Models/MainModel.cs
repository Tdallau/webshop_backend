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
    public DbSet<Addresses> Addresses { get; set; }

    //this method is run automatically by EF the first time we run the application
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){

      //here we define the name of our database
      optionsBuilder.UseMySql(ConfigurationManager.AppSetting["DBConectionString"]);
    }
  }
}