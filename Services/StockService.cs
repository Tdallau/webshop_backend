using System;
using System.Linq;
using Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using webshop_backend.Controllers;

namespace webshop_backend.Services
{
    public class StockService
    {
        public static void SetRandomStock()
        {
            using (MainContext context = new MainContext(new DbContextOptionsBuilder<MainContext>().UseMySql(
                ConfigurationManager.AppSetting.GetConnectionString("DefaultConnection")
            ).Options))
            {
                var prints = (from p in context.Print
                              select p).ToList();

                for (int i = 0; i < prints.Count; i++)
                {
                    Random rnd = new Random();
                    prints[i].stock = rnd.Next(0, 20);
                    context.Update(prints[i]);
                }
                context.SaveChanges();
                CardsController.NeedUpdate = true;
            }



        }

        public static void UpdateStockById(string printId, int stock)
        {
            using (MainContext context = new MainContext(new DbContextOptionsBuilder<MainContext>().UseMySql(
                ConfigurationManager.AppSetting.GetConnectionString("DefaultConnection")
            ).Options))
            {
                var print = (from p in context.Print
                             where p.Id == printId
                             select p).FirstOrDefault();
                if (print != null)
                {
                    print.stock = stock;
                    context.Update(print);
                    context.SaveChanges();
                    CardsController.NeedUpdate = true;
                }
            }
        }

    }
}