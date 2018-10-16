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

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [Authorize(Roles="User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MainContext __context;
        public UserController (MainContext context){
            this.__context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "end";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

    }
}
