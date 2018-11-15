using Contexts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Models.DB;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Models;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController: BasicController {
        public OrderController(MainContext context) : base(context) { }

        [HttpPost]
        public ActionResult<StatusData> Post([FromBody] StatusData status) {

            var shoppingCart = (from ShoppingCard in this.__context.ShoppingCard
                                where ShoppingCard.Id == status.ShoppingCardId
                                select ShoppingCard).FirstOrDefault();
            
            shoppingCart.Status = status.Status;

            var token = HttpContext.Request.Headers["Token"].FirstOrDefault();
            var jwttoken = new JwtSecurityToken(token);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            var newShopingCart = new ShoppingCard(){ UserId = userId, Status = "Waiting"};

            this.__context.Add(newShopingCart);
            this.__context.Update(shoppingCart);
            this.__context.SaveChanges();

            return Ok(status);
        }
    }
}