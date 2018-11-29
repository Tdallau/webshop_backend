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
using System.Collections.Generic;
using webshop_backend.html.order;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class OrderController : BasicController
    {
        public OrderController(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings)
        {

        }

        [HttpPost]
        public ActionResult<Response<string>> Post([FromBody] StatusData status)
        {

            var shoppingCart = (from ShoppingCard in this.__context.ShoppingCard
                                where ShoppingCard.Id == status.ShoppingCardId
                                select ShoppingCard).FirstOrDefault();

            var shoppingCartItem = (from ShoppingCardItem in this.__context.ShoppingCardItem
                                    where ShoppingCardItem.ShoppingCardId == shoppingCart.Id
                                    select ShoppingCardItem).ToList();

            var order = new Order() { userId = shoppingCart.UserId, addressId = 1, status = "Ordered" };
            this.__context.Add(order);
            this.__context.SaveChanges();
            foreach (var item in shoppingCartItem)
            {
                var print = (from Print in this.__context.Print
                             where Print.Id == item.PrintId
                             select Print).FirstOrDefault();
                var stock = print.stock - item.Quantity;
                if (stock >= 0)
                {
                    print.stock = stock;
                    this.__context.Update(print);
                    this.__context.SaveChanges();

                    var price = print?.price;
                    if (price != null)
                    {
                        var orderItem = new OrderProduct() { orderId = order.id, price = (int)price, quantity = item.Quantity, PrintId = item.PrintId };
                        this.__context.Add(orderItem);
                    }
                }
                else
                {
                    return Ok(new Response<string>()
                    {
                        Data = $"not enough {print.Id} more in stock! Just {print.stock} over.",
                        Success = false
                    });
                }
            }
            foreach (var item in shoppingCartItem)
            {
                this.__context.Remove(item);
            }
            this.__context.SaveChanges();


            this.SendConformation(order);
            return Ok(new Response<string> { Data = "Your order has been placed!", Success = true });
        }

        private void SendConformation(Order order)
        {
            User user = (from User in this.__context.User
                         where User.id == order.userId
                         select User).FirstOrDefault();

            Address address = (from Address in this.__context.Address
                               where Address.UserId == order.userId
                               select Address).FirstOrDefault();
            List<OrderTable> orderitems = (from OrderProduct in this.__context.OrderProduct
                                           join Print in this.__context.Print on OrderProduct.PrintId equals Print.Id
                                           join CartFaces in this.__context.CardFaces on Print.Card.Id equals CartFaces.card.Id
                                           where OrderProduct.orderId == order.id
                                           select new OrderTable { Name = CartFaces.name, Quantity = OrderProduct.quantity, Price = OrderProduct.price }).ToList();

            var body = OrderToCSharp.Order(user, address, orderitems);

            this.mainServcie.SendEmail("Your order has been placed!", body, true, user.email);


        }
    }
}