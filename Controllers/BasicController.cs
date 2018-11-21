using System;
using Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using System.IdentityModel.Tokens.Jwt;
using Services;
using Microsoft.Extensions.Options;
using Models;

namespace webshop_backend.Controllers
{
    public abstract class BasicController : ControllerBase
    {
        public MainContext __context { get; set; }
        public MainServcie mainServcie {get; set;}
        public BasicController(MainContext context, IOptions<EmailSettings> settings)
        {
            this.__context = context;
            this.mainServcie = new MainServcie(context, settings);
        }
    }
}