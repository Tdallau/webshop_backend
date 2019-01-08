using Contexts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api")]
    [ApiController]
    public class MainController : BasicController
    {
        public MainController(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings) : base(context, settings, urlSettings)
        {
        }
        [HttpGet]
        public string Alive() {
            return "The server is running!!";
        }

        [HttpGet("mail")]
        public ActionResult<string> TestMail(){
            try
            {
                this.mainServcie.SendEmail("Test", "Dit is een test", false, "tim@dallau.com");
                return Ok("succesfully send!!");
            }
            catch (System.Exception e)
            {
                
                return Ok(e);
            }
        }
    }
}