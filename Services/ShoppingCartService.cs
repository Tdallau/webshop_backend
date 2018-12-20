using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Contexts;
using Models.DB;
using System.Collections.Generic;

namespace webshop_backend.Services
{
    public class ShoppingCartService
    {
        private readonly MainContext __context;
        public ShoppingCartService() {
            this.__context = new MainContext(new DbContextOptionsBuilder<MainContext>().UseMySql(
                ConfigurationManager.AppSetting.GetConnectionString("DefaultConnection")
            ).Options);
        }
        public bool UpdateShoppingCart(int userId, ShoppingCardItem shoppingCardItem) {
            if (shoppingCardItem != null)
            {
                

                var shoppingCart = (from ShoppingCard in this.__context.ShoppingCard
                             join ShoppingCardItem in this.__context.ShoppingCardItem on ShoppingCard.Id equals ShoppingCardItem.ShoppingCardId
                             where ShoppingCard.UserId == userId && ShoppingCardItem.PrintId == shoppingCardItem.PrintId
                             select ShoppingCardItem).FirstOrDefault();

                var print = this.GetPrint(shoppingCardItem.PrintId);

                if (shoppingCart != null)
                {
                    var stock = print.stock - (shoppingCardItem.Quantity - shoppingCart.Quantity);
                    if (shoppingCardItem.Quantity > 0)
                    {
                        if (stock >= 0)
                        {
                            shoppingCart.Quantity = shoppingCardItem.Quantity;
                            this.__context.Update(shoppingCart);
                            this.__context.SaveChanges();
                            return true;
                        }
                        return false;

                    }
                    
                    return false;
                }
                if (print.stock > 0)
                {
                    this.__context.Add(shoppingCardItem);
                    this.__context.SaveChanges();
                    return true;
                }
                return false;

            }
            return false;
        }

        public List<string> UpdateShoppingCartRange(int userId, ShoppingCardItem[] shoppingCardItems) {
            List<string> notInStock = new List<string>();
            foreach (var shoppingCardItem in shoppingCardItems)
            {
                if(!this.UpdateShoppingCart(userId, shoppingCardItem)) {
                    notInStock.Add(shoppingCardItem.PrintId);
                }
            }
            return notInStock;
        }

        private Print GetPrint(string printId) {
            return (from p in this.__context.Print
                             where p.Id == printId
                             select p).FirstOrDefault();
        } 
    }
}