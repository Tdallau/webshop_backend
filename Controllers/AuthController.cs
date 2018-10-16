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
        public AuthController (MainContext context) : base(context){
            this.userServices = new UserServices(this.__context);
        }

        [Route("[controller]/login")]
        [HttpPost]
        public Object Login([FromBody] LoginData loginData)
        {
            var user = this.userServices.IsValidUserAndPasswordCombination(loginData.email, loginData.password);
            
            if (user != null) {
                var userId = user.id;
                var token = this.userServices.GenerateToken(user.email,user.role);

                this.userServices.UpdateUserToken(userId,token);
                return this.CreateResponseUsingUserId<SucccessFullyLoggedIn>(new SucccessFullyLoggedIn(token,userId),userId);
            }
            return this.createResponse<SucccessFullyLoggedIn>(null,null);
        }

        [Route("[controller]/register")]
        [HttpPost]
        public Object Register([FromBody] LoginData loginData)
        {
            // return loginData;
            if (loginData.role == null) loginData.role = "User"; 
            var success = this.userServices.InsertUser(loginData.username,loginData.email,loginData.approach,loginData.password, loginData.role);
            if(success) {
                return this.Login(loginData);
            }
            return Unauthorized();
            

        }
    }
    public class SucccessFullyLoggedIn
    {
        public string Token {get;}
        public int Id {get;}
        public SucccessFullyLoggedIn(){}
        public SucccessFullyLoggedIn(string token, int id)
        {
            this.Token = token;
            this.Id = id;
        }
    }
}
