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
    public class OrderController : BasicController
    {
        public OrderController(MainContext context) : base(context) { }

        [HttpPost]
        public ActionResult<Response<string>> Post([FromBody] StatusData status)
        {

            var shoppingCart = (from ShoppingCard in this.__context.ShoppingCard
                                where ShoppingCard.Id == status.ShoppingCardId
                                select ShoppingCard).FirstOrDefault();

            var shoppingCartItem = (from ShoppingCardItem in this.__context.ShoppingCardItem
                                    where ShoppingCardItem.ShoppingCardId == shoppingCart.Id
                                    select ShoppingCardItem).ToList();


            // var token = HttpContext.Request.Headers["Token"].FirstOrDefault();
            // var jwttoken = new JwtSecurityToken(token);
            // var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);
            var order = new Order() { userId = shoppingCart.UserId, addressId = 1, status = "Ordered" };
            this.__context.Add(order);
            this.__context.SaveChanges();
            foreach (var item in shoppingCartItem)
            {
                var query = (from Print in this.__context.Print
                             where Print.Id == item.PrintId
                             select Print).FirstOrDefault();
                var price = query?.price;
                if (price != null)
                {
                    var orderItem = new OrderProduct() { orderId = order.id, price = (int) price, quantity = item.Quantity, PrintId = item.PrintId };
                    this.__context.Add(orderItem);
                }
            }
            foreach (var item in shoppingCartItem)
            {
                this.__context.Remove(item);
            }
            this.__context.SaveChanges();



            return Ok(new Response<string>{ Data = "Your order has been placed!", Success = true});
        }
    }
}