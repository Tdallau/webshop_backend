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

namespace webshop_backend.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private MainContext __context;
        private UserServices userServices;
        public AuthController (){
            this.__context = new MainContext();
            this.userServices = new UserServices();
        }

        [Route("[controller]/login")]
        [HttpPost]
        public Object Login([FromBody] LoginData loginData)
        {
            var userIdArray = this.userServices.IsValidUserAndPasswordCombination(loginData.email, loginData.password);
            
            if (userIdArray.Length == 1) {
                var userId = userIdArray[0].id;
                var user = this.userServices.getUser(userId);
                var token = this.userServices.GenerateToken(user.email,user.role);

                this.userServices.UpdateUserToken(userId,token);

                return new {user.email, user.approach, user.name, user.addresses, user.role, token};
            }
            return userIdArray;
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
}
