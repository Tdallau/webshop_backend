using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Contexts;
using webshop_backend;

namespace webshop_backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles="Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly MainContext __context;
        public AdminController (MainContext context){
            this.__context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Hello mister Admin";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

    }
}
