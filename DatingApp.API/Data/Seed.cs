using System.Collections.Generic;
using System.Linq;
using DatingApp.API.models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                // create some roles

                var roles = new List<Role>
                {
                    new Role {Name="Member"},
                    new Role {Name="Admin"},
                    new Role {Name="Moderator"},
                    new Role {Name="VIP"}
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                foreach (var user in users)
                {
                    userManager.CreateAsync(user, "testpwd").Wait();
                    userManager.AddToRoleAsync(user, "Member");
                }

                //create admin user
                var admin = new User
                {
                    UserName = "Admin"
                };

                var result = userManager.CreateAsync(admin, "adminpwd").Result;

                if (result.Succeeded)
                {
                    var appAdmin = userManager.FindByNameAsync("Admin").Result;
                    userManager.AddToRoleAsync(appAdmin, "Admin");
                    userManager.AddToRoleAsync(appAdmin, "Moderator");
                }

            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }
}