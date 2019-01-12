using Contexts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Models.DB;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using webshop_backend.Services;
using System.Collections.Generic;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class ShoppingCartController : BasicController
    {
        private readonly ShoppingCartService shoppingCartService;
        public ShoppingCartController(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings)
        {
            this.shoppingCartService = new ShoppingCartService();
        }

        [HttpGet]
        public ActionResult<ShoppingCard> Get()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            var query = from ShoppingCard in this.__context.ShoppingCard
                        join ShoppingCardItem in this.__context.ShoppingCardItem on ShoppingCard.Id equals ShoppingCardItem.ShoppingCardId
                        join Print in this.__context.Print on ShoppingCardItem.PrintId equals Print.Id
                        join CardFaces in this.__context.CardFaces on Print.Card.Id equals CardFaces.card.Id
                        where ShoppingCard.UserId == userId
                        select new { Id = ShoppingCardItem.PrintId, CardFaces.name, ShoppingCardItem.Quantity, PriceNum = Print.price};


            var test = query.GroupBy(v => v.Id)
                            .Select(v => new { v.Key, Name = v.Select(w => w.name), PriceNum = v.Select(w => w.PriceNum), Quantity = v.Select(w => w.Quantity) })
                            .ToList()
                            .Select(v => new
                            {
                                Id = v.Key,
                                Name = string.Join(" // ", v.Name),
                                PriceNum = v.PriceNum.FirstOrDefault(),
                                Quantity = v.Quantity.FirstOrDefault(),
                                PriceTotal = "", 
                                Price = "", 
                                PriceTotalNum = -1
                            }
            );

            return Ok(test);
        }

        [HttpPost]
        public ActionResult<Response<string>> Post([FromBody] ShoppingCardItem shoppingCardItem)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            if (this.shoppingCartService.UpdateShoppingCart(userId, shoppingCardItem))
            {
                return Ok(
                    new Response<string>()
                    {
                        Data = "Card is added to your shoppingcart!!",
                        Success = true
                    }
                );
            }
            return StatusCode(
                409,
                new Response<string>()
                {
                    Data = "Somthing went wrong. Most likly this item is not longer in stock.",
                    Success = false
                }
            );


        }
        [HttpPost("range")]
        public ActionResult<Response<List<string>>> PostRange([FromBody] ShoppingCardItem[] shoppingCardItems)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);


            return Ok(
                new Response<List<string>>()
                {
                    Data = this.shoppingCartService.UpdateShoppingCartRange(userId, shoppingCardItems),
                    Success = true
                }
            );

        }
        [HttpDelete]
        public ActionResult<Response<string>> DeleteItem([FromBody] ShoppingCardItem shoppingCardItem)
        {

            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            var product = (
                from ShoppingCard in this.__context.ShoppingCard
                join ShoppingCardItem in this.__context.ShoppingCardItem on ShoppingCard.Id equals ShoppingCardItem.ShoppingCardId
                where ShoppingCard.UserId == userId &&
                      ShoppingCardItem.PrintId == shoppingCardItem.PrintId
                select ShoppingCardItem
            ).FirstOrDefault();

            this.__context.Remove(product);
            this.__context.SaveChanges();

            return Ok(new Response<string>()
            {
                Data = "Item is removed from your Shoppingcart!!",
                Success = true
            });
        }
    }

}