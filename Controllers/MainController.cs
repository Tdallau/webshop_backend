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

namespace webshop_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly MainContext __context;
        public MainController (MainContext context){
            this.__context = context;
        }

        // GET api/values
        [HttpPost]
        public ActionResult<int> Post([FromBody] string token)
        {
            if(token != null && token != "") {
                var query = from user in this.__context.User
                            where user.token == token
                            select user.id;
                var userId = query.ToArray();
                if(userId.Length == 1) {
                    return userId[0];
                }
                return -1;
            }
            return -1;
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
}
