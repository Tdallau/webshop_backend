using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using Models.DB;
using Newtonsoft.Json;
using webshop_backend.Controllers;

namespace webshop_backend.Services
{
    public class PriceService
    {
        public void PriceInsert()
        {
            using (MainContext db = new MainContext(new DbContextOptionsBuilder<MainContext>().UseMySql(
                ConfigurationManager.AppSetting.GetConnectionString("DefaultConnection")
            ).Options))
            {

                var query = (from sets in db.Set
                             select sets).ToList();
                int i = 0;
                Parallel.ForEach<Set>(query, set =>
                {
                    Console.WriteLine($"set name: {set.name} - {(i * 100) / query.Count}% done");
                    this.hasMore($"https://api.scryfall.com/cards/search?order=set&q=e%3A{set.Id}");
                    i++;
                });
                CardsController.NeedUpdate = true;
            }
        }

        private void hasMore(string url)
        {
            System.Threading.Thread.Sleep(100);
            var requestHM = (HttpWebRequest)WebRequest.Create(url);
            var responseHM = (HttpWebResponse)requestHM.GetResponse();

            var responseStringHM = new StreamReader(responseHM.GetResponseStream()).ReadToEnd();

            var resultHM = JsonConvert.DeserializeObject<CartPriceResponse>(responseStringHM);
            this.InsertPriceInDB(resultHM.Data);
            if (resultHM.Has_more)
            {
                this.hasMore(resultHM.next_page);
            }
        }

        private void InsertPriceInDB(List<Data> prices)
        {
            System.Threading.Thread.Sleep(10000);
            using (MainContext db = new MainContext(new DbContextOptionsBuilder<MainContext>().UseMySql(
                ConfigurationManager.AppSetting.GetConnectionString("DefaultConnection")
            ).Options))
            {

                var query = (from Print in db.Print
                             join p in prices on Print.Id equals p.Id
                             where Print.Card.Id == p.Oracle_id
                             select new Print
                             {
                                 Id = Print.Id,
                                 price = p.Eur != null ? (int)(decimal.Parse(p.Eur)) : 0,
                                 foil = Print.foil,
                                 nonfoil = Print.nonfoil,
                                 oversized = Print.oversized,
                                 borderColor = Print.borderColor,
                                 collectorsNumber = Print.collectorsNumber,
                                 fullArt = Print.fullArt
                             });
                db.UpdateRange(query);
                db.SaveChanges();
                db.Dispose();
            }
        }
    }
}