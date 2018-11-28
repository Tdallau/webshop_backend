using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using webshop_backend;

namespace Models
{
    public class UserData
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int? Nbf { get; set; }
        public int? Exp { get; set; }
        public int? ShoppingCartId { get; set; }

        public string ToToken()
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, UserId.ToString()),
                new Claim(ClaimTypes.Name, Name),
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Role, Role),
                new Claim(ClaimTypes.SerialNumber, ShoppingCartId.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["SuperSecretKey"])),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static UserData FromToken(string token)
        {
            var jwttoken = new JwtSecurityToken(token);
            int uid = Int32.Parse(jwttoken.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);
            string name = jwttoken.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            string email = jwttoken.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            string role = jwttoken.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault()?.Value;
            string shoppingCartId = jwttoken.Claims.Where(c => c.Type == ClaimTypes.SerialNumber).FirstOrDefault()?.Value;
            string nbf = jwttoken.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Nbf).FirstOrDefault()?.Value;
            string exp = jwttoken.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Exp).FirstOrDefault()?.Value;
            return new UserData { Name = name, UserId = uid, Email = email, Role = role, Nbf = int.Parse(nbf), Exp = int.Parse(exp), ShoppingCartId = int.Parse(shoppingCartId) };
        }

    }
}