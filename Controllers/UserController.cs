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
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webshop_backend.Enum;

namespace webshop_backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class UserController : BasicController
    {
        public UserController(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings)
        {
        }

        // GET api/values
        [HttpGet]
        public ActionResult<Response<User>> Get()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var user = UserData.FromToken(userToken);

            var userReturn = (
                from u in this.__context.User
                where u.id == user.UserId
                select u
            ).FirstOrDefault();

            return Ok(new Response<User>()
            {
                Data = userReturn,
                Success = true
            });
        }

        // GET api/values/5
        [HttpPut]
        public ActionResult<Response<string>> Put([FromBody] User user)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var cu = UserData.FromToken(userToken);

            if (user.email != "" && user.name != "" &&  user.approach != "" && user.role.ToString() == cu.Role)
            { 

                var u = (
                    from us in this.__context.User
                    where us.id == cu.UserId
                    select us
                ).FirstOrDefault();

                u.email = user.email;
                u.name = user.name;
                u.approach = user.approach;

                this.__context.Update(u);
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
