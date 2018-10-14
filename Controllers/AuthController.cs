using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
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
        public Object Login(string username, string password, string role)
        {
            var userIdArray = this.userServices.IsValidUserAndPasswordCombination(username, password);

            if (userIdArray.Length == 1) {
                var userId = userIdArray[0].id;
                var user = this.userServices.getUser(userId);
                var token = this.userServices.GenerateToken(user.email,user.role);
                return new {user.email, user.approach, user.name, user.addresses, user.role, token};
            }
            return BadRequest();
        }

        [Route("[controller]/register")]
        [HttpPost]
        public Object Register(string username, string email, string gender, string password, string role)
        {
            if (role == null) role = "User"; 
            this.userServices.InsertUser(username,email,gender,password, role);
            return this.Login(email, password, role);

        }

    }
}
