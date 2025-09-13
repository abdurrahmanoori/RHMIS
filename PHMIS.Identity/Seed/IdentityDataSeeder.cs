using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PHMIS.Domain.Entities.Identity.Entity;
using PHMIS.Infrastructure.Context;

namespace PHMIS.Identity.Seed
{
    public static class IdentityDataSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var pending = await ctx.Database.GetPendingMigrationsAsync();
            if (pending.Any())
            {
                await ctx.Database.MigrateAsync();
            }
            else
            {
                await ctx.Database.EnsureCreatedAsync();
            }

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Roles to ensure
            var roles = new[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role });
                }
            }

            // Default admin user
            var adminEmail = "admin@local";
            var adminUserName = "admin";
            var admin = await userManager.FindByNameAsync(adminUserName);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "System",
                    LastName = "Admin",
                    HospitalId = 1
                };
                // Simple but valid password
                var adminPassword = "Pass@123";
                var createAdmin = await userManager.CreateAsync(admin, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            // Default normal user
            var userEmail = "user@local";
            var userUserName = "user";
            var user = await userManager.FindByNameAsync(userUserName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = userUserName,
                    Email = userEmail,
                    EmailConfirmed = true,
                    FirstName = "Default",
                    LastName = "User",
                    HospitalId = 1
                };
                var userPassword = "Pass@123";
                var createUser = await userManager.CreateAsync(user, userPassword);
                if (createUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}
