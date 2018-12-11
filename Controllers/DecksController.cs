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

            var Decks = (from d in this.__context.Decks
                         join p in this.__context.Print on d.Commander equals p.Id
                         join pf in this.__context.PrintFace on p.Id equals pf.PrintId
                         join iu in this.__context.ImagesUrl on pf.id equals iu.printFace.id
                         select new DeckResponse()
                         {
                             Name = d.Name,
                             Image = iu.art_crop,
                             FullImage = iu.normal,
                             Id = d.Id

                         }).ToList();

            return Ok(new Response<List<DeckResponse>>(){
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
                                select new Typeline{ TypeName = Types.typeName }
                            ).ToList()
                            let mana = (from Costs in this.__context.Costs
                                    from CostSymbols in this.__context.CostSymbols
                                    join SymbolsInCosts in this.__context.SymbolsInCosts on Costs.id equals SymbolsInCosts.cost.id
                                    where Costs.id == cf.manaCost.id && SymbolsInCosts.symbol.id == CostSymbols.id
                                    select CostSymbols
                            ).ToList()
                            where cd.deck.Id == d.Id
                            select new CardResponse() {
                                Id = p.Id,
                                FlavorText = PrintFace.flavorText,
                                TypeLine = CardResponse.GetTypeLine(tl),
                                Image = ImagesUrl.small,
                                Loyalty = cf.loyalty,
                                Mana = mana,
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
                            Id = d.Id,
                            Image = Commanderiu.art_crop,
                            FullImage = Commanderiu.normal,
                            Cards = cards
                        }
                        ).FirstOrDefault();

            return Ok(new Response<DeckResponse>() {
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

            var Decks = (from d in this.__context.Decks
                         select d).ToList();

            return Ok(new Response<string>() {
                Data = "Deck is created!!",
                Success = true
            });
        }
    
        [HttpPost("addCard")]
        public ActionResult<Response<string>> AddNewCard([FromBody] NewCardForDeck card) {
            var CardForDeck = new CardsDeck() {
                DeckId = card.DeckId,
                PrintId = card.PrintId
            };
            this.__context.Add(CardForDeck);
            this.__context.SaveChanges();

            return Ok(new Response<string>() {
                Data = "Card is added to your deck",
                Success = true
            });
        }
    }
}