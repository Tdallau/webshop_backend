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

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles="Admin")]
    [ApiController]
    public class AdminController : BasicController
    {
        public AdminController (MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings){
        }

        // GET api/admin/updateStock
        [HttpGet]
        public ActionResult<Response<string>> InsertRandomStock()
        {
            StockService.SetRandomStock(this.__context);
            return Ok(new Response<string>(){
                Data = "Stock Is filled with random values!!",
                Success = true
            });
        }

        // GET api/values/5
        [HttpGet("{id}/{stock}")]
        public ActionResult<Response<string>> UpdateStock(string id, int stock)
        {
            StockService.UpdateStockById(this.__context, id, stock);
            return Ok(new Response<string>(){
                Data = "Stock Is Updated!",
                Success = true
            });
        }

    }
}
