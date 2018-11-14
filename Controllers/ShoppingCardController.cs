using Contexts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Models.DB;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCardController : BasicController {
        public ShoppingCardController(MainContext context) : base(context) { }

        [HttpGet("{sessionId}")]
        public ActionResult<ShoppingCard> Get (string sessionId) {

            var query = from ShoppingCard in this.__context.ShoppingCard
                        where ShoppingCard.SessionId == sessionId
                        select ShoppingCard; 
            return Ok(new ShoppingCard());
        }
    }
}