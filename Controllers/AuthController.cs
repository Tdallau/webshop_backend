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
using webshop_backend.Models.DB;
using webshop_backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webshop_backend.Enum;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    public class AuthController : BasicController
    {
        private UserServices userServices;
        public AuthController(MainContext context, IOptions<EmailSettings> emailSettings, IOptions<Urls> urlSettings) : base(context, emailSettings, urlSettings)
        {
            this.userServices = new UserServices(this.__context, emailSettings, urlSettings);
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

                    var responseUser = new UserData() { Name = user.name, UserId = user.id, Email = user.email, Role = user.role.ToString(), ShoppingCartId = shoppingCartId };
                    var token = responseUser.ToToken();
                    var refreshToken = UserData.GenerateRefreshToken();
                    var userData = UserData.FromToken(token);

                    this.__context.Add(
                        new Tokens()
                        {
                            UserId = userId,
                            Token = refreshToken,
                            Time = DateTime.Now,
                            ExpireDate = DateTime.Now.AddDays(7)
                        }
                    );
                    this.__context.SaveChanges();

                    return Ok(new Response<SucccessFullyLoggedIn>()
                    {
                        Data = new SucccessFullyLoggedIn() { User = userData, Token = token, RefreshToken = refreshToken },
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

        [Route("[controller]/logout")]
        [HttpPost]
        public ActionResult<Response<string>> Logout([FromBody] RefreshTokens tokens) {
            var token = (
                from refresh in this.__context.Tokens
                // where refresh.Token == tokens.RefreshToken
                select refresh
            ).FirstOrDefault();
            this.__context.Remove(token);
            this.__context.SaveChanges();
            return Ok(
                new Response<string>() {
                    Data = "You are logged out",
                    Success = true
                }
            );
        }

        [Route("[controller]/refresh")]
        [HttpPost]
        public ActionResult<Response<RefreshTokens>> Refresh([FromBody] RefreshTokens tokens)
        {

            var jwttoken = new JwtSecurityToken(tokens.JwtToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            var newTokens = this.userServices.CheckRefreshToken(userId, tokens.RefreshToken, tokens.JwtToken);

            if (newTokens != null)
            {
                return Ok(
                new Response<RefreshTokens>()
                {
                    Data = newTokens,
                    Success = true
                }
            );
            }
            return Unauthorized();

        }

        [Route("[controller]/activate/{id}")]
        [HttpGet]
        public ActionResult<Response<string>> Put(int id)
        {

            var query = (from user in this.__context.User
                         where user.id == id
                         select user).FirstOrDefault();

            if (query != null)
            {
                if(!query.active) {
                    query.active = true;
                    this.__context.Update(query);
                    return Redirect(this.urlSettings.FrontendUrl);
                }
                return Redirect(this.urlSettings.FrontendUrl);
            } 

            return Redirect(this.urlSettings.FrontendUrl);


        }
    }

}
