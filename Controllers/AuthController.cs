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

        // GET api/values
        // [HttpGet]
        // public ActionResult<Object> Get()
        // {
            
        //     var query = from user in this.__context.User
        //                 where user.Id == 1
        //                 select new {user.Name, user.Gender, user.Email, user.Addresses, user.Id};

        //     var userData = query.GroupBy(x => x.Id).Select(y => y.First()).ToList();
 
        //     return userData;
        // }
        [Route("[controller]/login")]
        [HttpPost]
        public Object Login(string username, string password)
        {
            var userIdArray = this.userServices.IsValidUserAndPasswordCombination(username, password);
            
            if (userIdArray.Length == 1) {
                var userId = userIdArray[0].Id;
                return this.userServices.getUser(userId, this.userServices.GenerateToken(username));
            }
            return BadRequest();
        }

        [Route("[controller]/register")]
        [HttpPost]
        public Object Register(string username, string email, string gender, string password)
        {

            this.userServices.InsertUser(username,email,gender,password);
            return this.Login(email, password);

        }

    }
}
