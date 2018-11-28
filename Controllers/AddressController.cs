using System.Collections.Generic;
using Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using Models;
using Models.DB;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Claims;

namespace webshop_backend.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class AddressController : BasicController
    {
        public AddressController(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings)
        {
        }

        [HttpGet]
        public ActionResult<Response<List<Address>>> GetAddresses()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            List<Address> addresses = (from a in this.__context.Address
                             where a.UserId == userId
                             select a).ToList();

            return Ok(new Response<List<Address>>()
            {
                Data = addresses,
                Success = true
            });
        }

        [HttpPost]
        public ActionResult<Response<string>> AddAddress([FromBody] Address address)
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            var userToken = token.Split(' ')[1];
            var jwttoken = new JwtSecurityToken(userToken);
            var userId = Int32.Parse(jwttoken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);

            if (address.ZipCode != "" && address.City != "" && address.Street != "" && address.Number != 0)
            {
                address.UserId = address.UserId == 0 ? userId : address.UserId;
                this.__context.Add(address);
                this.__context.SaveChanges();
                return Ok(new Response<string>()
                {
                    Data = "addresses is added!",
                    Success = true
                });
            }

            return Ok(new Response<string>()
            {
                Data = "addresses is added!",
                Success = true
            });
        }

        [HttpPut("{userId}")]
        public ActionResult<Response<string>> UpdateAddresse(int userId, [FromBody] Address address)
        {
            var adr = (from a in this.__context.Address
                       where a.UserId == userId && a.Id == address.Id
                       select a).FirstOrDefault();

            adr.ZipCode = address.ZipCode;
            adr.City = address.City;
            adr.Street = address.Street;
            adr.Number = address.Number;

            this.__context.Update(adr);
            this.__context.SaveChanges();

            return Ok(new Response<string>()
            {
                Data = "Addresses is updated!",
                Success = true
            });
        }

    }
}