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

namespace Services
{
    public class UserServices
    {
        private readonly MainContext __context;
        private readonly MainServcie __mainService;
        public UserServices(MainContext context, IOptions<EmailSettings> settings)
        {
            this.__context = context;
            this.__mainService = new MainServcie(context, settings);

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

        public bool InsertUser(string username, string email, string approach, string password, string role)
        {

            try
            {
                var salt = GetSalt();
                var newUser = new User() { name = username, email = email, approach = approach, role = role, password = GetHash(password + salt), salt = salt, active = false };

                this.__context.Add(newUser);
                this.__context.SaveChanges();

                var shoppingCart = new ShoppingCard() { UserId = newUser.id };
                this.__context.Add(shoppingCart);
                this.__context.SaveChanges();

                this.SendActivationMail(newUser);
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
            string body = @"<form action='http://localhost:5000/auth/{id}' method='get'>
                                    <h1>Hello {title} {name}</h1>
                                    <p>Click the button below to activate your account.</p>
                                    <button type='submit' style='display: inline-block;
                                                                font-weight: 400;
                                                                text-align: center;
                                                                white-space: nowrap;
                                                                vertical-align: middle;
                                                                -webkit-user-select: none;
                                                                -moz-user-select: none;
                                                                -ms-user-select: none;
                                                                user-select: none;
                                                                border: 1px solid transparent;
                                                                padding: .375rem .75rem;
                                                                font-size: 1rem;
                                                                line-height: 1.5;
                                                                border-radius: .25rem;
                                                                transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color 		.15s ease-in-out,box-shadow .15s ease-in-out;color: #fff;
                                                                background-color: #007bff;
                                                                border-color: #007bff;'
                                    >Activate account</button>
                                </form>";
            body = body.Replace("{id}", user.id.ToString());
            body = body.Replace("{name}", user.name);
            body = body.Replace("{title}", user.approach != "" ? user.approach : "");
            this.__mainService.SendEmail(subject, body, true, user.email);
        }


    }


}