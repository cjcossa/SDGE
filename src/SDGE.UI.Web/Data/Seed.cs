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
        
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }
            string [] roles = { "Membro", "Participante", "Cientifico", "Organizador" };
            IdentityRole [] identityRoles = new IdentityRole[4];
            int i = 0; 
            foreach(var item in roles)
            {
                identityRoles[i] = new IdentityRole();
                identityRoles[i].Name = item;
                identityRoles[i].NormalizedName = item.Trim().ToUpper();
                i++;
            }
            context.AddRange(identityRoles);
            context.SaveChanges();
        }
    }
}
