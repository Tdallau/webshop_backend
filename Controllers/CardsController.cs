﻿using System;
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
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : BasicController
    {
        public CardsController(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings)
        {
        }

        // GET api/values/5
        [HttpGet]
        public ActionResult<Response<dynamic>> Get([FromQuery(Name = "page-size")] int page_size, int page)
        {
            var totalCards = (
                from p in this.__context.Print
                where p.price != null && p.price != 0
                select p.Id
            ).AsGatedNoTracking().Count();
            
            int totalPages = totalCards % page_size == 0 ? ((int)totalCards / page_size) : (int)(totalCards / page_size + 1);

            return Ok(new Response<dynamic>()
            {
                Data = new
                {
                    Cards = this.__context.ProductList.Filter().AsGatedNoTracking().Skip(page_size * (page - 1)).Take(page_size),
                    PageSize = page_size,
                    Page = page,
                    TotalPages = totalPages
                },
                Success = true
            });

        }

        [HttpGet("{id}")]
        public ActionResult<Response<CardResponse>> GetAction(string id)
        {
            var card = this.mainServcie.GetCard(id);

            if (card != null)
            {
                return Ok(new Response<CardResponse>()
                {
                    Data = card,
                    Success = true
                });
            }

            return StatusCode(404, "Cart not found!!");

        }
    }

    public static class QueryableExtensions
    {
        public static IQueryable<T> AsGatedNoTracking<T>(this IQueryable<T> source) where T : class
        {
            if (source.Provider is EntityQueryProvider)
                return source.AsNoTracking<T>();
            return source;
        }
        public static IQueryable<ProductList> Filter(this IQueryable<ProductList> productList)
        {

            var list = productList.AsGatedNoTracking()
            .Where(p => p.Name.Contains("Abbey"));

            return list;
        }
    }
}