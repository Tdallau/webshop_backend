using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using webshop_backend;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Options;
using webshop_backend.Services;
using Hangfire;
using Models.DB;
using webshop_backend.Models;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [Authorize(Roles="Admin")]
    [ApiController]
    public class AdminController : BasicController
    {
        public readonly AdminService adminService;
        public AdminController(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings)
        {
            this.adminService = new AdminService();
        }

        // GET api/admin/updateStock
        [HttpPost("stock")]
        public ActionResult<Response<string>> InsertRandomStock()
        {
            BackgroundJob.Enqueue(() => InsertStock());
            return Ok(new Response<string>()
            {
                Data = "Stock Is filled with random values!!",
                Success = true
            });
        }

        [HttpGet("cards")]
        public ActionResult<Response<string>> InsertCartPrice()
        {
            BackgroundJob.Enqueue(() => InsertPrice());
            return Ok(new Response<string>()
            {
                Data = "Price is updating",
                Success = true
            });
        }

        // GET api/values/5
        [HttpGet("stock/cards/{id}/{stock}")]
        public ActionResult<Response<string>> UpdateStock(string id, int stock)
        {
            StockService.UpdateStockById(id, stock);
            return Ok(new Response<string>()
            {
                Data = "Stock Is Updated!",
                Success = true
            });
        }

        [HttpGet("users")]
        public ActionResult<Response<List<AdminUsersList>>> GetUsers()
        {
            var users = (
                from u in this.__context.User
                select new AdminUsersList{
                    Id = u.id,
                    Name = u.name,
                    Approach = u.approach,
                    Email = u.email,
                    Role = u.role,
                    Active = u.active
                }
            ).ToList();

            return Ok(
                new Response<List<AdminUsersList>>() {
                    Data = users,
                    Success = true
                }
            );
        }

        [HttpPost("users")]
        public ActionResult<Response<string>> AddUser([FromBody] User user)
        {
            if (AdminService.CheckIncome(user))
            {

                this.adminService.CreateUser(user);

                return Ok(
                    new Response<string>()
                    {
                        Data = "User is created correctly",
                        Success = true
                    }
                );
            }
            return StatusCode(
                400,
                new Response<string>()
                {
                    Data = "some fields were not filed corectly",
                    Success = false
                }
            );
        }

        [HttpPut("users/{userId}")]
        public ActionResult<Response<string>> UpdateUser(int userId, [FromBody] User user)
        {

            if (AdminService.CheckIncome(user))
            {

                if (adminService.UpdateUser(userId, user))
                {
                    return Ok(
                        new Response<string>()
                        {
                            Data = "User is updated!",
                            Success = true
                        }
                    );
                }
                return StatusCode(
                    404,
                    new Response<string>()
                    {
                        Data = "User not found.",
                        Success = false
                    }
                );
            }

            return StatusCode(
                400,
                new Response<string>()
                {
                    Data = "some fields were not filed corectly",
                    Success = false
                }
            );
        }

        [HttpDelete("users/{userId}")]
        public ActionResult<Response<string>> DeleteUser(int userId)
        {


            if (adminService.DeleteUser(userId))
            {
                return Ok(
                    new Response<string>()
                    {
                        Data = "User is Deleted!",
                        Success = true
                    }
                );
            }
            return StatusCode(
                404,
                new Response<string>()
                {
                    Data = "User not found.",
                    Success = false
                }
            );

        }

        public void InsertStock()
        {
            StockService.SetRandomStock();
        }

        public void InsertPrice()
        {
            var admin = new PriceService();
            admin.PriceInsert();
        }

    }
}
