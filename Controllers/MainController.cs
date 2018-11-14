using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Contexts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.DB;
using webshop_backend;
using Models;
using Microsoft.AspNetCore.Cors;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : BasicController
    {
        public MainController(MainContext context) : base(context) { }
        // GET api/values
        [HttpPost]
        public IActionResult Post([FromBody] string token)
        {
            return this.createResponse<Test>(new Test("test"), token); ;
        }

        // GET api/values/5
        [HttpGet("getProducts/{page_size}/{page_index}")]
        public IActionResult Get(int page_size, int page_index)
        {

            var query = from Print in this.__context.Print
                        join CardFaces in this.__context.CardFaces on Print.Card.Id equals CardFaces.card.Id
                        join PrintFace in this.__context.PrintFace on Print.Id equals PrintFace.PrintId
                        join ImagesUrl in this.__context.ImagesUrl on PrintFace.id equals ImagesUrl.printFace.id
                        where Print.price != null && Print.isLatest
                        select new {Print.Id, CardFaces.name, Print.price, ImagesUrl.normal};


            return Ok(query.Skip(page_size * (page_index - 1)).Take(page_size));
        }
    }
    public class Test
    {
        public string Data { get; }
        public Test() { }
        public Test(string data)
        {
            this.Data = data;
        }
    }
}
