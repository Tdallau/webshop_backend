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
using webshop_backend.Models.DB;

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
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            var shoppingCart = (from ShoppingCard in this.__context.ShoppingCard
                                where ShoppingCard.Id == status.ShoppingCardId
                                select ShoppingCard).FirstOrDefault();

            var shoppingCartItem = (from ShoppingCardItem in this.__context.ShoppingCardItem
                                    where ShoppingCardItem.ShoppingCardId == shoppingCart.Id
                                    select ShoppingCardItem).ToList();

            var address = (from a in this.__context.Address
                           where a.UserId == userId
                           select a).ToList();


            if (shoppingCartItem.Count != 0 && address.Count != 0)
            {

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

                        this.UpdateSales(item);

                        var price = print?.price;
                        if (price != null)
                        {
                            var orderItem = new OrderProduct() { orderId = order.id, price = (int)price, quantity = item.Quantity, PrintId = item.PrintId };
                            this.__context.Add(orderItem);
                        }
                    }
                    else
                    {
                        return StatusCode(409, new Response<string>()
                        {
                            Data = $"not enough {print.Id} more in stock! Just {print.stock} left.",
                            Success = false
                        });
                    }
                }
                foreach (var item in shoppingCartItem)
                {
                    this.__context.Remove(item);
                }
                this.__context.SaveChanges();


                this.SendConformation(order, userId);
                return Ok(new Response<string> { Data = "Your order has been placed!", Success = true });
            }
            else
            {
                return Ok(new Response<string> { Data = "Your shoppingcard is empty!!", Success = false });
            }
        }

        private void SendConformation(Order order, int userId)
        {

            User user = (from User in this.__context.User
                         where User.id == userId
                         select User).FirstOrDefault();

            Address address = (from Address in this.__context.Address
                               where Address.UserId == userId
                               select Address).FirstOrDefault();
            List<OrderTable> orderitems = (from OrderProduct in this.__context.OrderProduct
                                           join Print in this.__context.Print on OrderProduct.PrintId equals Print.Id
                                           join CartFaces in this.__context.CardFaces on Print.Card.Id equals CartFaces.card.Id
                                           where OrderProduct.orderId == order.id
                                           select new OrderTable { Name = CartFaces.name, Quantity = OrderProduct.quantity, Price = OrderProduct.price }).ToList();

            var body = OrderToCSharp.Order(user, address, orderitems);

            this.mainServcie.SendEmail("Your order has been placed!", body, true, user.email);
        }
        private void UpdateSales(ShoppingCardItem item)
        {
            var sale = (from s in this.__context.Sales
                        where s.PrintId == item.PrintId
                        select s).FirstOrDefault();

            if (sale != null)
            {
                sale.NumberOfOrders += 1;
                sale.Quantity += item.Quantity;
                this.__context.Update(sale);
            }
            else
            {
                var newSale = new Sales()
                {
                    NumberOfOrders = 1,
                    PrintId = item.PrintId,
                    Quantity = item.Quantity
                };
                this.__context.Add(newSale);
            }

            this.__context.SaveChanges();

        }
    }
}