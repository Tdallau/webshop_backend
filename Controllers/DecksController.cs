using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Models.DB;
using webshop_backend.Models;
using webshop_backend.Models.DB;

namespace webshop_backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class DecksController : BasicController
    {
        public DecksController(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings) { }

        [HttpGet]
        public ActionResult<Response<List<Decks>>> Get()
        {

            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            var Decks = (from d in this.__context.Decks
                         join p in this.__context.Print on d.Commander equals p.Id
                         join pf in this.__context.PrintFace on p.Id equals pf.PrintId
                         join iu in this.__context.ImagesUrl on pf.id equals iu.printFace.id
                         where d.UserId == userId
                         select new DeckResponse()
                         {
                             Name = d.Name,
                             Image = iu.art_crop,
                             FullImage = iu.normal,
                             Id = d.Id

                         }).ToList();

            return Ok(new Response<List<DeckResponse>>()
            {
                Data = Decks,
                Success = true
            });
        }

        [HttpGet("{deckId}")]
        public ActionResult<Response<DeckResponse>> GetDeckById(int deckId)
        {

            var deck = (from d in this.__context.Decks
                        join Commander in this.__context.Print on d.Commander equals Commander.Id
                        join Commanderpf in this.__context.PrintFace on Commander.Id equals Commanderpf.PrintId
                        join Commanderiu in this.__context.ImagesUrl on Commanderpf.id equals Commanderiu.printFace.id
                        join Commandercf in this.__context.CardFaces on Commander.Card.Id equals Commandercf.card.Id
                        let cards = (
                            from cd in this.__context.CardsDeck
                            join p in this.__context.Print on cd.print.Id equals p.Id
                            join PrintFace in this.__context.PrintFace on p.Id equals PrintFace.PrintId
                            join cf in this.__context.CardFaces on p.Card.Id equals cf.card.Id
                            join ImagesUrl in this.__context.ImagesUrl on PrintFace.id equals ImagesUrl.printFace.id
                            let tl = (from TypesInLine in this.__context.TypesInLine
                                      join Types in this.__context.Types on TypesInLine.type.id equals Types.id
                                      join TypeLine in this.__context.TypeLine on TypesInLine.line.id equals TypeLine.id
                                      join CardFaces in this.__context.CardFaces on TypeLine.id equals CardFaces.typeLine.id
                                      where CardFaces.id == cf.id
                                      select new Typeline { TypeName = Types.typeName }
                            ).ToList()
                            let mana = (from Costs in this.__context.Costs
                                        from CostSymbols in this.__context.CostSymbols
                                        join SymbolsInCosts in this.__context.SymbolsInCosts on Costs.id equals SymbolsInCosts.cost.id
                                        where Costs.id == cf.manaCost.id && SymbolsInCosts.symbol.id == CostSymbols.id
                                        select CostSymbols
                            ).ToList()
                            let color = (from cic in this.__context.ColorsInCombinations
                                        join c in this.__context.Color on cic.color.Id equals c.Id
                                        where cic.combination.id == cf.color.id
                                        select c.symbol
                            ).ToList()
                            where cd.deck.Id == d.Id
                            select new CardResponse()
                            {
                                Id = p.Id,
                                FlavorText = PrintFace.flavorText,
                                TypeLine = CardResponse.GetTypeLine(tl),
                                Image = ImagesUrl.small,
                                Loyalty = cf.loyalty,
                                Mana = mana,
                                Color = color,
                                Name = cf.name,
                                OracleText = cf.oracleText,
                                Power = cf.power,
                                Toughness = cf.toughness,
                                Price = p.price
                            }
                        ).ToList()
                        where d.Id == deckId
                        select new DeckResponseWithCards()
                        {
                            Name = d.Name,
                            CommanderName = Commandercf.name,
                            Id = d.Id,
                            Image = Commanderiu.art_crop,
                            FullImage = Commanderiu.normal,
                            Cards = cards
                        }
                        ).FirstOrDefault();

            return Ok(new Response<DeckResponse>()
            {
                Data = deck,
                Success = true
            });
        }

        [HttpPost]
        public ActionResult<List<Decks>> Post([FromBody] Decks deck)
        {

            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            var newDeck = new Decks()
            {
                Commander = deck.Commander,
                SecondaryCommander = deck.SecondaryCommander == "" ? null : deck.SecondaryCommander,
                Name = deck.Name,
                UserId = userId
            };

            this.__context.Add(newDeck);
            this.__context.SaveChanges();

            return Ok(new Response<dynamic>()
            {
                Data = new
                {
                    DeckId = newDeck.Id
                },
                Success = true
            });
        }

        [HttpPost("{deckId}/cards")]
        public ActionResult<Response<string>> AddNewCard(int deckId, [FromBody] DeckIncome print)
        {
            var CardForDeck = new CardsDeck()
            {
                DeckId = deckId,
                PrintId = print.PrintId
            };
            this.__context.Add(CardForDeck);
            this.__context.SaveChanges();

            return Ok(new Response<string>()
            {
                Data = "Card is added to your deck",
                Success = true
            });
        }

        [HttpDelete("{deckId}/cards")]
        public ActionResult<Response<string>> DeleteCardFromDeck(int deckId, [FromBody] DeckIncome print)
        {
            var card = (
                from c in this.__context.CardsDeck
                where c.DeckId == deckId && c.PrintId == print.PrintId
                select c
            ).FirstOrDefault();
            this.__context.Remove(card);
            this.__context.SaveChanges();

            return Ok(new Response<string>()
            {
                Data = "Card deleted from your deck.",
                Success = true
            });
        }

        [HttpPost("{deckId}/shopping-cart")]
        public ActionResult<Response<List<string>>> OrderCardFromDecks(int deckId)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            var cardsInDeck = (
                from cd in this.__context.CardsDeck
                where cd.DeckId == deckId
                select cd
            ).ToList();

            var notInStock = new List<string>();
            foreach (var card in cardsInDeck)
            {
                var print = (
                    from p in this.__context.Print
                    where p.Id == card.PrintId
                    select p
                ).FirstOrDefault();

                if ((print.stock - 1) > 0)
                {
                    var shoppingCart = (
                        from sc in this.__context.ShoppingCard
                        where sc.UserId == userId
                        select sc.Id
                    ).FirstOrDefault();

                    var AllShoppingCartItems = (
                        from sci in this.__context.ShoppingCardItem
                        where sci.ShoppingCardId == shoppingCart && sci.PrintId == print.Id
                        select sci
                    ).FirstOrDefault();

                    ShoppingCardItem shoppingCartItem;

                    if (AllShoppingCartItems == null)
                    {
                        shoppingCartItem = new ShoppingCardItem()
                        {
                            ShoppingCardId = shoppingCart,
                            PrintId = card.PrintId,
                            Quantity = 1
                        };

                        this.__context.Add(shoppingCartItem);
                        this.__context.SaveChanges();
                    }

                }
                else
                {
                    // var cardNotInStock = (
                    //     from cf in this.__context.CardFaces
                    //     where cf.card.Id == print.Card.Id
                    //     select cf.name
                    // ).FirstOrDefault();
                    notInStock.Add(print.Id);
                }
            }

            return Ok(new Response<List<string>>()
            {
                Data = notInStock,
                Success = true
            });
        }
    }
}