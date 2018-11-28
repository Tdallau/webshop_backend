using System;
using System.Linq;
using Contexts;

namespace webshop_backend.Services
{
    public class StockService
    {
        public static void SetRandomStock(MainContext context)
        {
            var prints = (from p in context.Print
                          select p).ToList();

            for (int i = 0; i < prints.Count; i++)
            {
                Random rnd = new Random();
                prints[i].stock = rnd.Next(0,20);
                context.Update(prints[i]);
            }
            context.SaveChanges();


        }

        public static void UpdateStockById(MainContext context, string printId, int stock) {
            var print = (from p in context.Print
                        where p.Id == printId
                        select p).FirstOrDefault();
            if(print != null) {
                print.stock = stock;
                context.Update(print);
                context.SaveChanges();
            }
        }

    }
}