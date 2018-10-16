using System;
using Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
namespace webshop_backend.Controllers
{
	public abstract class BasicController : ControllerBase
	{
		public MainContext __context { get; set;}
		 public BasicController (MainContext context){
            this.__context = context;
        }
		protected IActionResult createResponse<T>(T data) where T: new() 
		{	
			string token = HttpContext.Request.Headers["token"];
			return this.OkOrNotFound(this.encapsulate<T>(data,this.GetUserId()));
		}
		protected int? GetUserId()
		{	
			string token = HttpContext.Request.Headers["token"];
			if(token != null && token != "") {
				var query = (from user in __context.User
							where user.token == token
							select user.id).ToList();
				if(query.Count > 0) {
					return query.First();
				}
				return null;
				
				
			}
			return null;
		}
		public IActionResult CreateResponseUsingUserId<T>(T data, int? userId) where T: new()
		{
			return this.OkOrNotFound<T>(this.encapsulate<T>(data,userId));
		}
		private Encapsulated<T> encapsulate<T>(T data, int? userId)
		{
			return new Encapsulated<T>(data,userId);
		}
		private IActionResult OkOrNotFound<T>(Encapsulated<T> data) where T: new()
		{
			if(data==null){
				return NotFound();
			} else {
				return Ok(data);
			}
		}
	}

	public class Encapsulated<T>
	{
		public T data { get;}
		public int? userId { get;}
		public Encapsulated(T data, int? userId)
		{
			this.data = data;
			this.userId = userId;
		}
	}
}