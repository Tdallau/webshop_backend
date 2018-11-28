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
using Models.DB;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MainContext __context;
        public UserController(MainContext context)
        {
            this.__context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "end";
        }

        // GET api/values/5
        [HttpPut]
        public ActionResult<Response<string>> Put([FromBody] User user)
        {
            var cu = (from u in this.__context.User
                      where u.id == user.id
                      select u).FirstOrDefault();

            if (user.email != "" && user.name != "" && user.role == cu.role)
            {
                this.__context.Update(user);
                this.__context.SaveChanges();
                return Ok(new Response<string>()
                {
                    Data = "Your account is updated!",
                    Success = true
                });
            }

            return Ok(new Response<string>()
            {
                Data = "Some settings where not valid, account is not updated!!",
                Success = false
            });
        }

    }
}
