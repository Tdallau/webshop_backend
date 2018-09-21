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
    [Authorize]
    [ApiController]
    public class MainController : ControllerBase
    {
        private MainContext __context;
        public MainController (){
            this.__context = new MainContext();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {

            //     User test = new User(){Name = "hallo", Email = "hallo@hallo.com", Gender = "Male", Password = "test123"};

            //     this.__context.Add(test);
            //     this.__context.SaveChanges();

            return "end";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
