using System;
using Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using System.IdentityModel.Tokens.Jwt;
using Services;
using Microsoft.Extensions.Options;
using Models;
using Microsoft.AspNetCore.Cors;

namespace webshop_backend.Controllers
{
    [EnableCors("MyPolicy")]
    public class BasicController : ControllerBase
    {
        protected readonly MainContext __context;
        protected readonly MainServcie mainServcie;
        protected readonly Urls urlSettings;

        public BasicController(MainContext context, IOptions<EmailSettings> emailSettings, IOptions<Urls> urlSettings)
        {
            this.__context = context;
            this.mainServcie = new MainServcie(context, emailSettings, urlSettings);
            this.urlSettings = urlSettings.Value;
        }
    }
}