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
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : BasicController
    {
        private static List<ProductList> _cache = new List<ProductList>();
        private static DateTime _cacheDate;
        public static bool NeedUpdate = false;
        public CardsController(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings)
        {
        }

        // GET api/values/5
        [HttpGet]
        public ActionResult<Response<dynamic>> Get([FromQuery(Name = "page-size")] int page_size, int page, string search = "")
        {
            if (_cache.Count == 0 || (DateTime.Now - _cacheDate).TotalHours > 24 || NeedUpdate)
            {
                lock (_cache)
                {
                    _cache = this.__context.ProductList.ToList();
                    _cacheDate = DateTime.Now;
                    NeedUpdate = false;
                }
            }

            var totalCards = _cache.Filter(search).Count();

            int totalPages = totalCards % page_size == 0 ? ((int)totalCards / page_size) : (int)(totalCards / page_size + 1);

            return Ok(new Response<dynamic>()
            {
                Data = new
                {
                    Cards = _cache.Filter(search).Skip(page_size * (page - 1)).Take(page_size),
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
        public static IEnumerable<ProductList> Filter(this IEnumerable<ProductList> productList, string search = "")
        {
            var list = productList;

            if (!string.IsNullOrEmpty(search))
            {
                foreach (var sortItem in search.Split("|"))
                {
                    var typeAndValue = sortItem.Split(":");
                    if (typeAndValue.Length == 1)
                    {
                        list = list.Where(p => p.Name.ToLower().Contains(typeAndValue[0].Trim().ToLower()));
                        Console.WriteLine(typeAndValue[0].ToLower());
                    }
                    else
                    {
                        switch (typeAndValue[0].Trim())
                        {
                            case "oracle":
                                list = list.Where(p => p.Oracle.Contains(typeAndValue[1].Trim()));
                                break;
                            case "set":
                                list = list.Where(p => p.Set == typeAndValue[1].Trim());
                                break;
                            case "flavor":
                                list = list.Where(p => p.Flavor.Contains(typeAndValue[1].Trim()));
                                break;

                        }
                    }
                }
            }

            return list.OrderBy(p => p.Id);
        }
    }
}
