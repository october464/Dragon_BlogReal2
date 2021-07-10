using Dragon_BlogReal.Enums;
using Dragon_BlogReal.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dragon_BlogReal.Helper
{
    public static class SeedHelper
    {
        public static async Task SeedDataAsync(UserManager<BlogUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedAdmin(userManager);
            await SeedModerator(userManager);
           
        }
        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            

        }

        private static async Task SeedAdmin(UserManager<BlogUser> userManager)
        {
            if (await userManager.FindByEmailAsync("jontwin77@yahoo.com") == null)
            {
                var admin = new BlogUser()
                {
                    Email = "jontwin77@yahoo.com",
                    UserName = "jontwin77@yahoo.com",
                    FirstName = "jon",
                    LastName = "green",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(admin, "312511JMjg!");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }

        private static async Task SeedModerator(UserManager<BlogUser> userManager)
        {
            if (await userManager.FindByEmailAsync("DrewRussell@CoderFoundry.com") == null)
            {
                var moderator = new BlogUser()
                {
                    Email = "DrewRussell@CoderFoundry.com",
                    UserName = "DrewRussell@CoderFoundry.com",
                    FirstName = "Drew",
                    LastName = "Russell",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(moderator, "ABCc123%");
                await userManager.AddToRoleAsync(moderator, Roles.Moderator.ToString());
            }
        }
       
    }
}

