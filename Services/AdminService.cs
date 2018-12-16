using Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.DB;
using Services;
using System.Linq;

namespace webshop_backend.Services
{
    public class AdminService
    {
        public static bool CheckIncome(User user)
        {
            if (user != null && user.email != "" && user.name != "" && user.role != "")
            {
                return true;
            }
            return false;
        }
        public void CreateUser(User user)
        {

            var salt = UserServices.GetSalt();

            var newUser = new User()
            {
                email = user.email,
                approach = user.approach,
                active = true,
                name = user.name,
                password = BCrypt.Net.BCrypt.HashPassword(user.password + salt),
                salt = salt
            };

            using (MainContext context = new MainContext(new DbContextOptionsBuilder<MainContext>().UseMySql(
                ConfigurationManager.AppSetting.GetConnectionString("DefaultConnection")
            ).Options))
            {
                context.Add(newUser);
                context.SaveChanges();
            }
        }
        public bool UpdateUser(int userId, User user)
        {
            using (MainContext context = new MainContext(new DbContextOptionsBuilder<MainContext>().UseMySql(
                ConfigurationManager.AppSetting.GetConnectionString("DefaultConnection")
            ).Options))
            {
                var eUser = (
                        from u in context.User
                        where u.id == userId
                        select u
                    ).FirstOrDefault();

                if (eUser != null)
                {
                    eUser.approach = user.approach != null ? user.approach : "";
                    eUser.email = user.email;
                    eUser.name = user.name;
                    eUser.role = user.role;

                    context.Update(eUser);
                    context.SaveChanges();

                    return true;
                }

                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            using (MainContext context = new MainContext(new DbContextOptionsBuilder<MainContext>().UseMySql(
                ConfigurationManager.AppSetting.GetConnectionString("DefaultConnection")
            ).Options))
            {
                var user = (
                    from u in context.User
                    where u.id == userId
                    select u
                ).FirstOrDefault();

                if(user != null) {

                    context.Remove(user);
                    context.SaveChanges();
                    
                    return true;
                }
                return false;
            }
        }
    }
}