using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.DB;
using Contexts;
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
        public MainController (MainContext context) : base(context){}
        // GET api/values
        [HttpPost]
        public IActionResult Post([FromBody] string token)
        {
            return this.createResponse<Test>(new Test("test"), token);; 
        }

        // GET api/values/5
        [HttpPost("getProducts")]
        public IActionResult Get(int page_size, int page_index)
        {

            var query = from product in this.__context.Product
                        /*from colorIdentity in this.__context.ColorIdentity
                        from colorIndicator in this.__context.ColocolorIndicator
                        from parts in this.__context.Parts*/
                        /*where legalitie.productId == product.id && parts.partOneId == product.id
                              && colorIdentity.productId == product.id */
                        select product;
                                    /*parts, colorIdentity, colorIndicator*/
            
            return Ok(query.Skip(page_size * (page_index -1)).Take(page_size));
        }
    }
    public class Test
    {
        public string Data {get;}
        public Test(){}
        public Test(string data)
        {
            this.Data =data;
        }
    }
}
