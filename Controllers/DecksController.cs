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
        public ActionResult<List<Decks>> Get()
        {

            var Decks = (from d in this.__context.Decks
                         select d).ToList();

            return Ok(Decks);
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

            return Ok(Decks);
        }
    }
}