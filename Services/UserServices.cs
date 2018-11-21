using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.DB;
using Contexts;
using System.Collections.Generic;
using System.Security.Cryptography;
using webshop_backend;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using Models;
using webshop_backend.html.activation;

namespace Services
{
    public class UserServices
    {
        private readonly MainContext __context;
        private readonly MainServcie __mainService;
        private readonly IOptions<Urls> __urlSettings;
        public UserServices(MainContext context, IOptions<EmailSettings> settings, IOptions<Urls> urlSettings)
        {
            this.__context = context;
            this.__mainService = new MainServcie(context, settings, urlSettings);
            this.__urlSettings = urlSettings;

        }
        public User IsValidUserAndPasswordCombination(string email, string password)
        {

            var query = from user in this.__context.User
                        where user.email == email
                        select user;
            try
            {
                Console.WriteLine(email);
                var curUser = query.First();
                if (curUser != null)
                {
                    if (curUser.password == GetHash(password + curUser.salt))
                    {
                        return curUser;
                    }
                }
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }

            return null;
        }

        public bool InsertUser(LoginData loginData)
        {

            try
            {
                var salt = GetSalt();
                var newUser = new User() { name = loginData.Username, email = loginData.Email, approach = loginData.Approach, role = loginData.Role, password = GetHash(loginData.Password + salt), salt = salt, active = false };

                this.__context.Add(newUser);
                this.__context.SaveChanges();

                var shoppingCart = new ShoppingCard() { UserId = newUser.id };
                this.__context.Add(shoppingCart);
                this.__context.SaveChanges();

                if (!newUser.active) { this.SendActivationMail(newUser); };
                return true;
            }
            catch
            {
                return false;
            }

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

        private void SendActivationMail(User user)
        {
            string subject = "Activate your acount.";
            string body = ActivationToCSharp.Activation(user, this.__urlSettings);
            this.__mainService.SendEmail(subject, body, true, user.email);
        }


    }


}