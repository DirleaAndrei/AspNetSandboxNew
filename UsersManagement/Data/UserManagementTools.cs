using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersManagement.Data
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

        private static async Task EnsureUsersCreated(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var adminUser = await EnsureUserCreated(userManager, "borys.admin@principal33.com", "TzfOh22_FCbjxXQt6U");
            var operatorUser = await EnsureUserCreated(userManager, "borys.operator@principal33.com", "cmy7baQLWdY5qlOMYZFF");
            var commonUser = await EnsureUserCreated(userManager, "borys.user@principal33.com", "jTzPDpuIRyfhGJkjY848");
            var superUser = await EnsureUserCreated(userManager, "borys.super@principal33.com", "4ewurntQ90gEj4orU6D8");

            var adminRole = await EnsureRoleCreated(serviceProvider, "Administrator");
            var operatorRole = await EnsureRoleCreated(serviceProvider, "Operator");
            var userRole = await EnsureRoleCreated(serviceProvider, "User");

            await userManager.AddToRoleAsync(adminUser, adminRole.Name);
            await userManager.AddToRoleAsync(operatorUser, operatorRole.Name);
            await userManager.AddToRoleAsync(commonUser, userRole.Name);
            await userManager.AddToRoleAsync(superUser, adminRole.Name);
            await userManager.AddToRoleAsync(superUser, operatorRole.Name);
            await userManager.AddToRoleAsync(superUser, userRole.Name);

            var users = await userManager.Users.ToListAsync();
            Console.WriteLine($"There are {users.Count} users now.");
        }

        private static async Task<IdentityUser> EnsureUserCreated(UserManager<IdentityUser> userManager, string name, string password)
        {
            var newUser = await userManager.FindByNameAsync(name);
            if (newUser == null)
            {
                await userManager.CreateAsync(new IdentityUser(name));
                newUser = await userManager.FindByNameAsync(name);
                var tokenChangePassword = await userManager.GeneratePasswordResetTokenAsync(newUser);

                var result = await userManager.ResetPasswordAsync(newUser, tokenChangePassword, password);

                if (!newUser.EmailConfirmed)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    await userManager.ConfirmEmailAsync(newUser, token);
                }
            }

            return newUser;
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
