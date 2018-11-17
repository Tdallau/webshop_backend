using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Contexts;
using Services;
using webshop_backend;
using Microsoft.AspNetCore.Cors;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    public class AuthController : BasicController
    {
        private UserServices userServices;
        public AuthController(MainContext context) : base(context)
        {
            this.userServices = new UserServices(this.__context);
        }

        [Route("[controller]/login")]
        [HttpPost]
        public ActionResult<Response<SucccessFullyLoggedIn>> Login([FromBody] LoginData loginData)
        {
            var user = this.userServices.IsValidUserAndPasswordCombination(loginData.email, loginData.password);

            if (user != null)
            {
                var userId = user.id;

                var shoppingCartId = (from sc in this.__context.ShoppingCard
                                      where sc.UserId == userId
                                      select sc.Id).FirstOrDefault();

                var responseUser = new UserData() { Name = user.name, UserId = user.id, Email = user.email, Role = user.role, ShoppingCartId = shoppingCartId };
                var token = responseUser.ToToken();
                var userData = UserData.FromToken(token);

                return Ok(new Response<SucccessFullyLoggedIn>()
                {
                    Data = new SucccessFullyLoggedIn() { User = userData, Token = token },
                    Success = true
                });
            }

            return Ok(new Response<string>()
            {
                Data = "User not found",
                Success = false
            });
        }

        [Route("[controller]/register")]
        [HttpPost]
        public ActionResult<Response<string>> Register([FromBody] LoginData loginData)
        {
            // return loginData;
            if (loginData.role == null) loginData.role = "User";
            var success = this.userServices.InsertUser(loginData.username, loginData.email, loginData.approach, loginData.password, loginData.role);
            if (success)
            {
                return Ok(new Response<string>() { Data = "succesfull registerd!", Success = true });
            }
            return Ok(new Response<string>()
            {
                Data = "there is already an account with this email address",
                Success = false
            });


        }
    }

}
