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
        public MainController (MainContext context) : base(context){}
        // GET api/values
        // [HttpPost]
        // public IActionResult Post()
        // {
        //     return this.createResponse<Test>(new Test("test"));
        // }

        // GET api/values/5
        [HttpGet("{page_size}/{page_index}")]
        public IActionResult Get(int page_size, int page_index)
        {

            // var query = (from product in this.__context.Product
            //             select product).Distinct();
            // var query = this.__context.Product.GroupBy(x => x.oracle_id).Select(x => x.FirstOrDefault());
            var query = (from Product in this.__context.Product
                        group Product by Product.oracle_id into p
                        select p.FirstOrDefault());

            
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
