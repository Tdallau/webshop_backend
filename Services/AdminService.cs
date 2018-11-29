using System;
using System.IO;
using System.Linq;
using System.Net;
using Contexts;
using Models;
using Newtonsoft.Json;

namespace webshop_backend.Services
{
    public class AdminService
    {
        public void Main(MainContext db)
        {
    
                var query = (from sets in db.Set
                             select sets).ToList();

                foreach (var set in query)
                {
                    //https://api.scryfall.com/cards/search?order=set&q=e%3A
                    Console.WriteLine($"set name: {set.name}");
                    System.Threading.Thread.Sleep(100);
                    var request = (HttpWebRequest)WebRequest.Create($"https://api.scryfall.com/cards/search?order=set&q=e%3A{set.Id}");

                    var response = (HttpWebResponse)request.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    var result = JsonConvert.DeserializeObject<CartPriceResponse>(responseString);
                    foreach (var card in result.Data)
                    {
                        this.insertPrice(card.Id, card.Oracle_id, card.Eur, db);

                    }
                    if (result.Has_more)
                    {
                        this.hasMore(result, db);
                    }

                }

        }

        private void hasMore(CartPriceResponse result, MainContext db)
        {
            System.Threading.Thread.Sleep(100);
            var requestHM = (HttpWebRequest)WebRequest.Create(result.next_page);
            var responseHM = (HttpWebResponse)requestHM.GetResponse();

            var responseStringHM = new StreamReader(responseHM.GetResponseStream()).ReadToEnd();

            var resultHM = JsonConvert.DeserializeObject<CartPriceResponse>(responseStringHM);
            foreach (var card in resultHM.Data)
            {
                this.insertPrice(card.Id, card.Oracle_id, card.Eur, db);

            }
            if(resultHM.Has_more) {
                this.hasMore(resultHM, db);
            }
        }

        private void insertPrice(string cardId, string oracleId, string price, MainContext db)
        {
            var card = (from c in db.Print
                        where c.Id == cardId && c.Card.Id == oracleId
                        select c).FirstOrDefault();
            if (card != null)
            {
                Console.WriteLine($"card id: {card.Id}");
                Console.WriteLine($"price: {price}");
                card.price = (int) decimal.Parse(price);
                db.Update(card);
                db.SaveChanges();
            }
        }
    }
}