using System.Net;
using System.Net.Mail;
using System.Linq;
using Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using Models.DB;
using webshop_backend.Models;
using webshop_backend;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Services
{
    public class MainServcie
    {
        private readonly MainContext __context;
        private readonly EmailSettings __EmailSettings;
        public MainServcie(IOptions<EmailSettings> settings, IOptions<Urls> urlSettings)
        {
            this.__context = new MainContext(new DbContextOptionsBuilder<MainContext>().UseMySql(
                ConfigurationManager.AppSetting.GetConnectionString("DefaultConnection")
            ).Options);
            this.__EmailSettings = settings.Value;
        }
        public async void SendEmail(string subject, string body, bool isBodyHtml, string email)
        {
            var test = this.__EmailSettings.Host;
            var smtpClient = new SmtpClient
            {
                Host = this.__EmailSettings.Host, // set your SMTP server name here
                Port = 587, // Port 
                EnableSsl = true,
                Credentials = new NetworkCredential(this.__EmailSettings.Email, this.__EmailSettings.Password)
            };
            using (var message = new MailMessage(this.__EmailSettings.Email, email)
            {
                IsBodyHtml = isBodyHtml,
                Subject = subject,
                Body = body,
            })
            {
                await smtpClient.SendMailAsync(message);
            }
        }
        
        public CardResponse GetCard(string id)
        {
            return (from Print in this.__context.Print
                    join cf in this.__context.CardFaces on Print.Card.Id equals cf.card.Id
                    join pf in this.__context.PrintFace on Print.Id equals pf.PrintId
                    join iu in this.__context.ImagesUrl on pf.id equals iu.printFace.id
                    let mana = (from Costs in this.__context.Costs
                                from CostSymbols in this.__context.CostSymbols
                                join SymbolsInCosts in this.__context.SymbolsInCosts on Costs.id equals SymbolsInCosts.cost.id
                                where Costs.id == cf.manaCost.id && SymbolsInCosts.symbol.id == CostSymbols.id
                                select CostSymbols
                                ).ToList()
                    let typeLine = (from TypesInLine in this.__context.TypesInLine
                                    join Types in this.__context.Types on TypesInLine.type.id equals Types.id
                                    join TypeLine in this.__context.TypeLine on TypesInLine.line.id equals TypeLine.id
                                    join CardFaces in this.__context.CardFaces on TypeLine.id equals CardFaces.typeLine.id
                                    where CardFaces.id == cf.id
                                    select new Typeline { TypeName = Types.typeName }).ToList()
                    let color = (from cic in this.__context.ColorsInCombinations
                                 join c in this.__context.Color on cic.color.Id equals c.Id
                                 where cic.combination.id == cf.color.id
                                 select c.symbol
                    ).ToList()
                    where Print.Id == id
                    select new CardResponse
                    {
                        Id = Print.Id,
                        Name = cf.name,
                        Loyalty = cf.loyalty,
                        Toughness = cf.toughness,
                        Power = cf.power,
                        Price = Print.price,
                        OracleText = cf.oracleText,
                        FlavorText = pf.flavorText,
                        Image = iu.large,
                        Mana = mana,
                        TypeLine = CardResponse.GetTypeLine(typeLine),
                        Color = color
                    }).FirstOrDefault();
        }


    }
    public static class QueryableExtensions
    {
        public static IQueryable<T> AsGatedNoTracking<T>(this IQueryable<T> source) where T : class
        {
            if (source.Provider is EntityQueryProvider)
                return source.AsNoTracking<T>();
            return source;
        }
        public static IQueryable<ProductList> Filter(this IQueryable<ProductList> productList)
        {

            var list = productList.AsGatedNoTracking()
            .Where(p => p.Name.Contains("Abbey") && p.Oracle.Contains("Flying"));

            return list;
        }
    }
}