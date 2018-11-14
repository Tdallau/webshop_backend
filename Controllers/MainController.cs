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

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : BasicController
    {
        public MainController(MainContext context) : base(context) { }
        // GET api/values

        [HttpPost]
        public IActionResult Post([FromBody] string token)
        {
            return this.createResponse<Test>(new Test("test")); ;
        }

        // GET api/values/5
        [HttpGet("{page_size}/{page_index}")]
        public IActionResult Get(int page_size, int page_index)
        {


            // var query = from Print in this.__context.Print
            //             join CardFaces in this.__context.CardFaces on Print.Card.Id equals CardFaces.card.Id
            //             join PrintFace in this.__context.PrintFace on Print.Id equals PrintFace.PrintId
            //             join ImagesUrl in this.__context.ImagesUrl on PrintFace.id equals ImagesUrl.printFace.id
            //             where Print.price != null && Print.isLatest
            //             select new { Print.Id, CardFaces.name, Print.price, Image = ImagesUrl.normal };

            

            // return Ok(this.__context.ProductList.ToList());
            return Ok(this.__context.ProductList.Skip(page_size * (page_index - 1)).Take(page_size));
            // return Ok(query.Skip(page_size * (page_index - 1)).Take(page_size));
        }

        [HttpGet("{id}")]
        public IActionResult GetAction(string id)
        {


            var card = (from Print in this.__context.Print
                        join CardFaces in this.__context.CardFaces on Print.Card.Id equals CardFaces.card.Id
                        join PrintFace in this.__context.PrintFace on Print.Id equals PrintFace.PrintId
                        join ImagesUrl in this.__context.ImagesUrl on PrintFace.id equals ImagesUrl.printFace.id
                        where Print.Id == id
                        select new
                        {
                            Printid = Print.Id,
                            CardFaces.name,
                            CardFaces.loyalty,
                            CardFaces.toughness,
                            CardFaces.power,
                            Print.price,
                            CardFaces.oracleText,
                            PrintFace.flavorText,
                            Image = ImagesUrl.large,
                            CardFaceId =  CardFaces.id
                        }).FirstOrDefault();


            
            if (card != null)
            {
                var printfaceid = (int)card?.CardFaceId;
                var typeLine = (from TypesInLine in this.__context.TypesInLine
                                join Types in this.__context.Types on TypesInLine.type.id equals Types.id
                                join TypeLine in this.__context.TypeLine on TypesInLine.line.id equals TypeLine.id
                                join CardFaces in this.__context.CardFaces on TypeLine.id equals CardFaces.typeLine.id
                                where CardFaces.id == printfaceid
                                select new { TypeName = Types.typeName }).ToList();

                var tl = "";
                for (int i = 0; i < typeLine.Count; i++)
                {
                    if( i != typeLine.Count - 1) {
                        tl += typeLine[i].TypeName + " ";
                    } else {
                        tl += typeLine[i].TypeName;
                    }
                }
                
                return Ok(new {Id = card.Printid, card.name, card.Image, card.flavorText, card.oracleText, card.loyalty, card.power, card.toughness, card.price, TypeLine = tl});
            }

            return UnprocessableEntity();

        }
    }
    public class Test
    {
        public string Data { get; }
        public Test() { }
        public Test(string data)
        {
            this.Data = data;
        }
    }
}
