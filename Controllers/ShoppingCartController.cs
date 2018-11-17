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

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [Authorize(Roles="User")]
    [ApiController]
    public class ShoppingCartController : BasicController
    {
        public ShoppingCartController(MainContext context) : base(context) { }

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
                            join ShoppingCardItem in this.__context.ShoppingCardItem on ShoppingCard.Id equals shoppingCardItem.ShoppingCardId
                            where ShoppingCard.UserId == userId && ShoppingCardItem.PrintId == shoppingCardItem.PrintId
                            select ShoppingCardItem).FirstOrDefault();

                if(query != null) {
                    query.Quantity += shoppingCardItem.Quantity;
                    this.__context.Update(query);
                    this.__context.SaveChanges();
                    return Ok();
                }
                this.__context.Add(shoppingCardItem);
                this.__context.SaveChanges();
                return Ok(new Response<string>(){ Data = "Item is added to your shoppingcart!", Success = true});
            }
            return UnprocessableEntity();

        }
    }
}