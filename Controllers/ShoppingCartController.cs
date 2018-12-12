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

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class ShoppingCartController : BasicController
    {
        public ShoppingCartController(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings) { }

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
                        select new { Id = ShoppingCardItem.PrintId, CardFaces.name, ShoppingCardItem.Quantity, PriceNum = Print.price, PriceTotal = "", Price = "", PriceTotalNum = -1 };


            return Ok(query);
        }

        [HttpPost]
        public ActionResult<Response<string>> Post([FromBody] ShoppingCardItem shoppingCardItem)
        {

            if (shoppingCardItem != null)
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var userToken = token.Split(' ')[1];
                var jwttoken = new JwtSecurityToken(userToken);
                var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

                var query = (from ShoppingCard in this.__context.ShoppingCard
                             join ShoppingCardItem in this.__context.ShoppingCardItem on ShoppingCard.Id equals ShoppingCardItem.ShoppingCardId
                             where ShoppingCard.UserId == userId && ShoppingCardItem.PrintId == shoppingCardItem.PrintId
                             select ShoppingCardItem).FirstOrDefault();

                var print = (from p in this.__context.Print
                             where p.Id == shoppingCardItem.PrintId
                             select p).FirstOrDefault();

                if (query != null)
                {
                    var stock = print.stock - (shoppingCardItem.Quantity - query.Quantity);
                    if (shoppingCardItem.Quantity > 0)
                    {
                        if (stock > 0)
                        {
                            query.Quantity = shoppingCardItem.Quantity;
                            this.__context.Update(query);
                            this.__context.SaveChanges();
                            return Ok(new Response<string>() { Data = "Item is added to your shoppingcart!", Success = true });
                        }
                        return Ok(new Response<string>()
                        {
                            Data = "not enough products in stock",
                            Success = false
                        });

                    }
                    
                    return Ok(new Response<string>() { Data = "You can not go below 1.", Success = true });
                }
                if (print.stock > 0)
                {
                    this.__context.Add(shoppingCardItem);
                    this.__context.SaveChanges();
                    return Ok(new Response<string>() { Data = "Item is added to your shoppingcart!", Success = true });
                }
                return Ok(new Response<string>() { Data = "Item is out of stock!", Success = false });

            }
            return UnprocessableEntity();

        }
        [HttpDelete]
        public ActionResult<Response<string>> DeleteItem([FromBody] ShoppingCardItem shoppingCardItem) {

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

            return Ok(new Response<string>(){
                Data = "Item is removed from your Shoppingcart!!",
                Success = true
            });
        }
    }

}