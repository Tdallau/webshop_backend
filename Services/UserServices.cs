
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Services
{
    public class UserServices
    {
        private MainContext __context;
        public UserServices()
        {
            this.__context = new MainContext();
        }
        public User[] IsValidUserAndPasswordCombination(string username, string password)
        {

            var query = from user in this.__context.User
                        where user.email == username && user.password == GetHash(password + user.salt)
                        select user;

            return query.ToArray();
        }

        public void InsertUser(string username, string email, string approach, string password, string role) {

            var salt = GetSalt();
            var newUser = new User(){name = username, email = email, approach = approach, role = role, password= GetHash(password + salt), salt = salt};
            this.__context.Add(newUser);
            this.__context.SaveChanges();
        }

        public User getUser(int userId) {

            var query = from user in this.__context.User
                        where user.id == userId
                        select user;

            return query.ToArray()[0];
        }

        public string GenerateToken(string username, string Role)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.Role, Role)
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256")),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        private string GetSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public void UpdateUserToken(int id, string token) {
            var entity = this.__context.User.FirstOrDefault(u => u.id == id);
            if(entity != null) {
                entity.token = token;
                this.__context.Update(entity);
                this.__context.SaveChanges();
            }
        }
    }
    

}
