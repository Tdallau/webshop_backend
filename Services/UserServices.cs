using System;
using BCrypt.Net;
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
using webshop_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using webshop_backend.Enum;

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
            this.__mainService = new MainServcie(settings, urlSettings);
            this.__urlSettings = urlSettings;

        }
        public User IsValidUserAndPasswordCombination(string email, string password)
        {

            var query = (from user in this.__context.User
                        where user.email == email
                        select user).FirstOrDefault();
            try
            {
                var curUser = query;
                if (curUser != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password + curUser.salt, curUser.password))
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

        public RefreshTokens CheckRefreshToken(int userId, string refreshToken, string jwtToken)
        {

            var tokens = (
                from t in this.__context.Tokens
                where t.UserId == userId
                select t
            ).ToList();

            if (tokens.Count != 0)
            {

                for (int i = 0; i < tokens.Count; i++)
                {
                    if (tokens[i].ExpireDate != new DateTime() && tokens[i].Token == refreshToken)
                    {

                        var userData = UserData.FromToken(jwtToken);
                        var user = new UserData()
                        {
                            UserId = userData.UserId,
                            Name = userData.Name,
                            Email = userData.Email,
                            Role = userData.Role,
                            ShoppingCartId = userData.ShoppingCartId
                        };

                        var newJwtToken = user.ToToken();
                        var newRefreshToken = UserData.GenerateRefreshToken();

                        tokens[i].ExpireDate = DateTime.Now.AddDays(7);
                        tokens[i].Time = DateTime.Now;
                        tokens[i].Token = newRefreshToken;

                        this.__context.Update(tokens[i]);
                        this.__context.SaveChanges();

                        return new RefreshTokens()
                        {
                            JwtToken = newJwtToken,
                            RefreshToken = newRefreshToken
                        };
                    }
                }

            }

            return null;
        }

        public bool InsertUser(LoginData loginData)
        {
            try
            {
                var salt = UserServices.GetSalt();


                var newUser = new User()
                {
                    email = loginData.Email,
                    approach = loginData.Approach,
                    active = false,
                    name = loginData.Username,
                    password = BCrypt.Net.BCrypt.HashPassword(loginData.Password + salt),
                    salt = salt,
                    role = Roles.User
                };

                using (MainContext context = new MainContext(new DbContextOptionsBuilder<MainContext>().UseMySql(
                    ConfigurationManager.AppSetting.GetConnectionString("DefaultConnection")
                ).Options))
                {
                    context.Add(newUser);
                    context.SaveChanges();

                    var shoppingCart = new ShoppingCard() { UserId = newUser.id };
                    this.__context.Add(shoppingCart);
                    this.__context.SaveChanges();
                }
                if (!newUser.active) { this.SendActivationMail(newUser); };
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public static string GetSalt()
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