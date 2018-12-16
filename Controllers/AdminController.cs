using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using webshop_backend;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Options;
using webshop_backend.Services;
using Hangfire;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    // [Authorize(Roles="Admin")]
    [ApiController]
    public class AdminController : BasicController
    {
        public AdminController (MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings){
        }

        // GET api/admin/updateStock
        [HttpPost("stock")]
        public ActionResult<Response<string>> InsertRandomStock()
        {
            BackgroundJob.Enqueue(() => InsertStock());
            return Ok(new Response<string>(){
                Data = "Stock Is filled with random values!!",
                Success = true
            });
        }

        [HttpGet("cards")]
        public ActionResult<Response<string>> InsertCartPrice()
        {
            BackgroundJob.Enqueue(() => InsertPrice());
            return Ok(new Response<string>(){
                Data = "Price is updating",
                Success = true
            });
        }

        // GET api/values/5
        [HttpGet("stock/cards/{id}/{stock}")]
        public ActionResult<Response<string>> UpdateStock(string id, int stock)
        {
            StockService.UpdateStockById(this.__context, id, stock);
            return Ok(new Response<string>(){
                Data = "Stock Is Updated!",
                Success = true
            });
        }

        public void InsertStock() {
            StockService.SetRandomStock(this.__context);
        }

        public void InsertPrice() {
            var admin = new AdminService();
            admin.Main(this.__context);
        }

    }
}
