using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using webshop_backend;

namespace webshop_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private MainContext __context;
        public MainController (){
            this.__context = new MainContext();
        }

        // GET api/values
        [HttpPost]
        public ActionResult<int> Post(string token)
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
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

    }
}
