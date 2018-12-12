using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Contexts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.DB;
using webshop_backend;
using Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using webshop_backend.Models;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : BasicController
    {
        public MainController(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings) { }

        // GET api/values/5
        [HttpGet("{page_size}/{page_index}")]
        public ActionResult<Response<dynamic>> Get(int page_size, int page_index)
        {
            if (this.__context.ProductList.Count() != 0)
            {

                int totalCards = this.__context.ProductList.Count();
                int totalPages = totalCards % page_size == 0 ? ((int)totalCards / page_size) : (int)(totalCards / page_size + 1);   

                return Ok(new Response<dynamic>(){
                    Data = new {
                        Cards = this.__context.ProductList.Skip(page_size * (page_index - 1)).Take(page_size),
                        PageSize = page_size,
                        Page = page_index,
                        TotalPages =  totalPages
                    },
                    Success = true
                });
                // return Ok(query.Skip(page_size * (page_index - 1)).Take(page_size));
            }
            return UnprocessableEntity();
        }

        [HttpGet("{id}")]
        public IActionResult GetAction(string id)
        {
            var card = (from Print in this.__context.Print
                        join CardFaces in this.__context.CardFaces on Print.Card.Id equals CardFaces.card.Id
                        join PrintFace in this.__context.PrintFace on Print.Id equals PrintFace.PrintId
                        join ImagesUrl in this.__context.ImagesUrl on PrintFace.id equals ImagesUrl.printFace.id
                        let mana = (from Costs in this.__context.Costs
                                    from CostSymbols in this.__context.CostSymbols
                                    join SymbolsInCosts in this.__context.SymbolsInCosts on Costs.id equals SymbolsInCosts.cost.id
                                    where Costs.id == CardFaces.manaCost.id && SymbolsInCosts.symbol.id == CostSymbols.id
                                    select CostSymbols
                                    ).ToList()
                        where Print.Id == id
                        select new
                        {
                            Printid = Print.Id,
                            CardFaces.name,
                            CardFaces.loyalty,
                            CardFaces.toughness,
                            CardFaces.power,
                            Print.price,
                            CardFaces.oracleText,
                            PrintFace.flavorText,
                            Image = ImagesUrl.large,
                            CardFaceId = CardFaces.id,
                            mana = mana
                        }).FirstOrDefault();

            if (card != null)
            {
                var printfaceid = (int)card?.CardFaceId;
                var typeLine = (from TypesInLine in this.__context.TypesInLine
                                join Types in this.__context.Types on TypesInLine.type.id equals Types.id
                                join TypeLine in this.__context.TypeLine on TypesInLine.line.id equals TypeLine.id
                                join CardFaces in this.__context.CardFaces on TypeLine.id equals CardFaces.typeLine.id
                                where CardFaces.id == printfaceid
                                select new Typeline{ TypeName = Types.typeName }).ToList();

                var tl = CardResponse.GetTypeLine(typeLine);

                return Ok(new CardResponse{ Id = card.Printid, Name = card.name, Image = card.Image, FlavorText = card.flavorText, OracleText = card.oracleText, Loyalty = card.loyalty, Power = card.power, Toughness = card.toughness, Price = card.price, TypeLine = tl, Mana = card.mana});
            }

            return UnprocessableEntity();

        }
    }

    
}
