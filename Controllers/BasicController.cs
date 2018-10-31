using System;
using Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using System.IdentityModel.Tokens.Jwt;

namespace webshop_backend.Controllers
{
    public abstract class BasicController : ControllerBase
    {
        public MainContext __context { get; set; }
        public BasicController(MainContext context)
        {
            this.__context = context;
        }
        protected IActionResult createResponse<T>(T data) where T : new()
        {
            return this.OkOrNotFound(this.encapsulate<T>(data, this.GetUserId()));
        }
        protected int? GetUserId()
        {
            string token = HttpContext.Request.Headers["token"];
            if (token != null)
            {
                try
                {
                    var jwtToken = new JwtSecurityToken(token);
                    var claim = jwtToken.Claims.Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                    var userid = claim.ToArray();
                    if (userid.Length > 0)
                    {
                        var i = 0;
                        return Int32.TryParse(userid.FirstOrDefault().Value, out i) ? i : (int?)null;
                    }
                    return null;
                } catch {
					return null;
				}
            }
            return null;

        }
        public IActionResult CreateResponseUsingUserId<T>(T data, int? userId) where T : new()
        {
            return this.OkOrNotFound<T>(this.encapsulate<T>(data, userId));
        }
        private Encapsulated<T> encapsulate<T>(T data, int? userId)
        {
            return new Encapsulated<T>(data, userId);
        }
        private IActionResult OkOrNotFound<T>(Encapsulated<T> data) where T : new()
        {
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
    }

    public class Encapsulated<T>
    {
        public T data { get; }
        public int? userId { get; }
        public Encapsulated(T data, int? userId)
        {
            this.data = data;
            this.userId = userId;
        }
    }
}