using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConFutureNce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ConFutureNce.Extensions
{
    public class AccountRolesManagement
    {
        public static async Task CreateRoles(ConFutureNceContext context)
        {
            
            var userManager = context.GetService<UserManager<ApplicationUser>>();
            var roleManager = context.GetService<RoleManager<IdentityRole>>();
            // Add roles to app
            string[] roleNames =
            {
                "Admin",
                "Author",
                "Organizer",
                "ProgrammeCommitteeMember",
                "Reviewer"
            };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            var myUser = await userManager.FindByEmailAsync("peciak@gmail.com");

            if ( myUser != null && !await userManager.IsInRoleAsync(myUser, "Author"))
            {
                 // Assigne user to role
                await userManager.AddToRoleAsync(myUser, "Author");   
            }
            
        }
    }
}
