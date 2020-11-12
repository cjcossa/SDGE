using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDGE.UI.Web.Data
{
    public static class Seed
    {
        /*public static void Initialize(ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }
            
            string [] roles = { "Membro", "Participante", "Cientifico", "Organizador", "Admin", "Director" };
            string email = "sisgdec@gmail.com";
            IdentityRole [] identityRoles = new IdentityRole[6];
            int i = 0; 
            foreach(var item in roles)
            {
                identityRoles[i] = new IdentityRole();
                identityRoles[i].Name = item;
                identityRoles[i].NormalizedName = item.Trim().ToUpper();
                i++;
            }
            context.AddRange(identityRoles);
            var user = new IdentityUser { UserName = email, Email = email , PasswordHash = "1"};
            context.Users.Add(user);
            context.SaveChanges();
        }
        public static void SeedUser(UserManager<IdentityUser> userManager)
        {
            string email = "sisgdec@gmail.com";
            var user = new IdentityUser { UserName = email, Email = email };
            userManager.CreateAsync(user, "1");
        }*/

        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            string email = "sisgdec@gmail.com";
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = email;
                user.Email =email;
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync
                (user, "123456").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,"Admin").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Membro", "Participante", "Cientifico", "Organizador", "Admin", "Director" };
            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    IdentityRole identityRole = new IdentityRole();
                    identityRole.Name = role;
                    identityRole.NormalizedName = role.Trim().ToUpper();
                    IdentityResult roleResult = roleManager.
                    CreateAsync(identityRole).Result;
                }
            }
            

        }
    }
}
