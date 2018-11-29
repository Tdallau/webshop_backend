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
        public ActionResult<Response<UserData>> Get()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            var user = (from u in this.__context.User
                        where u.id == userId
                        select new UserData(){ Email = u.email, Name = u.name, Role = u.role, Approach = u.approach}).FirstOrDefault();

            return Ok(new Response<UserData>()
            {
                Data = user,
                Success = true
            });
        }

        // GET api/values/5
        [HttpPut]
        public ActionResult<Response<string>> Put([FromBody] User user)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            var cu = (from u in this.__context.User
                      where u.id == userId
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
