using System;
using Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using System.IdentityModel.Tokens.Jwt;

namespace webshop_backend.Controllers
{
    public abstract class BasicController : ControllerBase
    {
        public MainContext __context { get; set; }
        public BasicController(MainContext context)
        {
            this.__context = context;
        }
    }
}