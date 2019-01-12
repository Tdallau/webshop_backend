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
using webshop_backend.Enum;
using webshop_backend.Models;

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

        [HttpPost("stock")]
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


            if (shoppingCartItem.Count != 0)
            {

                List<string> outOfStock = new List<string>();

                foreach (var item in shoppingCartItem)
                {
                    var print = (from Print in this.__context.Print
                                 join c in this.__context.CardFaces on Print.Card.Id equals c.card.Id
                                 where Print.Id == item.PrintId
                                 select new
                                 {
                                     Print.stock,
                                     c.name
                                 }).FirstOrDefault();
                    var stock = print.stock - item.Quantity;
                    if (stock <= 0)
                    {
                        outOfStock.Add(print.name + " is out of stock.");
                    }
                }

                if (outOfStock.Count > 0)
                {
                    return StatusCode(409, new Response<List<string>>()
                    {
                        Data = outOfStock,
                        Success = false
                    });
                }
                else
                {
                    return Ok(new Response<string>()
                    {
                        Data = "order can be placed",
                        Success = true
                    });
                }
            }
            else
            {
                return Ok(new Response<string> { Data = "Your shoppingcard is empty!!", Success = false });
            }
        }

        [HttpPost]
        public ActionResult<Response<string>> Order([FromBody] NewOrder order)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            var shoppingCart = (from ShoppingCard in this.__context.ShoppingCard
                                where ShoppingCard.Id == order.ShoppingCardId
                                select ShoppingCard).FirstOrDefault();

            var shoppingCartItem = (from ShoppingCardItem in this.__context.ShoppingCardItem
                                    where ShoppingCardItem.ShoppingCardId == shoppingCart.Id
                                    select ShoppingCardItem).ToList();


            var address = $"{order.Address.Street} {order.Address.Number}, {order.Address.ZipCode} {order.Address.City}";

            var newOrder = new Order(){Status = OrderStatus.Ordered,  UserId= userId, Address = address, PayMethod = order.PayMethod, Date = DateTime.Today };
            this.__context.Add(newOrder);
            this.__context.SaveChanges();

            foreach (var item in shoppingCartItem)
                        {
                            var print = (from Print in this.__context.Print
                                         where Print.Id == item.PrintId
                                         select Print).FirstOrDefault();

                            var stock = print.stock - item.Quantity;
                            print.stock = stock;

                            this.__context.Update(print);
                            this.__context.SaveChanges();

                            this.UpdateSales(item);

                            var price = print?.price;
                            if (price != null)
                            {
                                var orderItem = new OrderProduct() { orderId = newOrder.Id, price = (int)price, quantity = item.Quantity, PrintId = item.PrintId };
                                this.__context.Add(orderItem);
                            }
                        }
                        foreach (var item in shoppingCartItem)
                        {
                            this.__context.Remove(item);
                        }
                        this.__context.SaveChanges();
                        this.SendConformation(newOrder, userId);
                        return Ok(new Response<string> { Data = "Your order has been placed!", Success = true });
        }

        [HttpGet]
        public ActionResult<Response<List<Order>>> GetStatus()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            var orders = (
                from o in this.__context.Order
                where o.UserId == userId
                orderby o.Date descending
                select o 
            ).ToList();
            
            OrderStatus status;
            List<OrderReturn> orderResponse = new List<OrderReturn>();

            foreach (var item in orders)
            {
                status = (OrderStatus)item.Status;
                orderResponse.Add(
                    new OrderReturn() {
                        Address = item.Address,
                        Date = item.Date,
                        Id = item.Id,
                        PayMethod = item.PayMethod,
                        UserId = item.UserId,
                        StatusString = status.ToString(),
                        Status = item.Status
                    }
                );
            }


            return Ok(new Response<List<OrderReturn>>(){
               Data = orderResponse,
               Success = true 
            });
        }

        [HttpGet("{orderId}")]
        public ActionResult<Response<List<ResponseOrderItem>>> GetProducts(int orderId)
        {
            var items = (
                from oi in this.__context.OrderProduct
                from p in this.__context.Print
                from cf in this.__context.CardFaces
                where oi.orderId == orderId && p.Id == oi.PrintId && p.Card.Id == cf.card.Id
                select new ResponseOrderItem{ Name = cf.name, Quantity = oi.quantity, Price = oi.price, Id = oi.id }
            ).ToList();

            return Ok(
                new Response<List<ResponseOrderItem>>(){
                    Data = items,
                    Success = true
                }
            );
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
                                           where OrderProduct.orderId == order.Id
                                           select new OrderTable { Name = CartFaces.name, Quantity = OrderProduct.quantity, Price = OrderProduct.price }).ToList();

            var body = OrderToCSharp.Order(user, address, orderitems, order);

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