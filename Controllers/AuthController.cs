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
using Microsoft.Extensions.Options;
using Models.DB;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    public class AuthController : BasicController
    {
        private UserServices userServices;
        private Urls urlSettings;
        public AuthController(MainContext context, IOptions<EmailSettings> emailSettings, IOptions<Urls> urlSettings) : base(context, emailSettings, urlSettings)
        {
            this.userServices = new UserServices(this.__context, emailSettings, urlSettings);
            this.urlSettings = urlSettings.Value;
        }

        [Route("[controller]/login")]
        [HttpPost]
        public ActionResult<Response<SucccessFullyLoggedIn>> Login([FromBody] LoginData loginData)
        {
            var user = this.userServices.IsValidUserAndPasswordCombination(loginData.Email, loginData.Password);

            if (user != null)
            {
                if (user.active == true)
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
                else
                {
                    return Ok(new Response<string>()
                    {
                        Data = "Account is not activated yet view your email to activate your account.",
                        Success = false
                    });
                }

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
            if (loginData.Role == null) loginData.Role = "User";
            var success = this.userServices.InsertUser(loginData);
            if (success)
            {
                return Ok(new Response<string>()
                {
                    Data = "succesfull registerd!",
                    Success = true
                });
            }
            return Ok(new Response<string>()
            {
                Data = "There is already an account with this email address.",
                Success = false
            });


        }
        [Route("[controller]/{id}")]
        [HttpGet]
        public ActionResult<Response<string>> Put(int id)
        {

            var query = (from user in this.__context.User
                         where user.id == id
                         select user).FirstOrDefault();

            if (query != null)
            {
                if(!query.active) {

                    var address = new Address(){ 
                        UserId = query.id,
                        ZipCode = "2904CX",
                        City = "Capelle",
                        Street = "Reggedal",
                        Number = 10
                    };

                    query.active = true;
                    this.__context.Update(query);
                    this.__context.Add(address);
                    this.__context.SaveChanges();
                    return Redirect(this.urlSettings.FrontendUrl);
                }
                return Redirect(this.urlSettings.FrontendUrl);
            } 

            return Redirect(this.urlSettings.FrontendUrl);

            
        }
    }

}
