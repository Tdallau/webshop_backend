using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Contexts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.DB;
using webshop_backend;
using Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using webshop_backend.Models;
using Microsoft.AspNetCore.Http;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : BasicController
    {
        public CardsController(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings) {
         }

        // GET api/values/5
        [HttpGet]
        public ActionResult<Response<dynamic>> Get([FromQuery(Name = "page-size")] int page_size, int page)
        {
        

            if (this.__context.ProductList.Count() != 0)
            {

                int totalCards = this.__context.ProductList.Count();
                int totalPages = totalCards % page_size == 0 ? ((int)totalCards / page_size) : (int)(totalCards / page_size + 1);   

                return Ok(new Response<dynamic>(){
                    Data = new {
                        Cards = this.__context.ProductList.Skip(page_size * (page - 1)).Take(page_size),
                        PageSize = page_size,
                        Page = page,
                        TotalPages =  totalPages
                    },
                    Success = true
                });
            }
            return Ok(new Response<dynamic>(){
                    Data = new {
                        Cards = new List<dynamic>(),
                        PageSize = 0,
                        Page = 0,
                        TotalPages =  0
                    },
                    Success = false
                });
        }

        [HttpGet("{id}")]
        public ActionResult<Response<CardResponse>> GetAction(string id)
        {
            var card = this.mainServcie.GetCard(id);

            if (card != null)
            {
                return Ok(new Response<CardResponse>(){
                    Data = card,
                    Success = true
                });
            }

            return StatusCode(404, "Cart not found!!");

        }
    }

    
}
