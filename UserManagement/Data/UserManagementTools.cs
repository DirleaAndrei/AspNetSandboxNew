using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Data
{
    public static class UserManagementTools
    {
        public static async Task SeedUsersWithRolesAsync(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var adminRole = await EnsureRoleCreated(serviceScope.ServiceProvider, "Administrator");
                var operatorRole = await EnsureRoleCreated(serviceScope.ServiceProvider, "Operator");
            }
        }

        private static async Task<IdentityRole> EnsureRoleCreated(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var newRole = await roleManager.FindByNameAsync(roleName);
            if (newRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                newRole = await roleManager.FindByNameAsync(roleName);
            }

            return newRole;
        }
    }


}
